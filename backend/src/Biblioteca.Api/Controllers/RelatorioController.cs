using Biblioteca.Infrastructure.Data;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stimulsoft.Report;
using System.Reflection.Metadata;

namespace Biblioteca.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RelatorioController : ControllerBase
    {
        private readonly BibliotecaContext _context;
        private readonly IWebHostEnvironment _env;

        public RelatorioController(BibliotecaContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }


        [HttpGet("relatorio-livros")]
        public async Task<IActionResult> ObterRelatorioLivros()
        {
            var dados = await _context.Livros
                .Include(l => l.LivroAutores).ThenInclude(la => la.Autor)
                .Include(l => l.LivroAssuntos).ThenInclude(la => la.Assunto)
                .Include(l => l.LivroValores)
                .Select(l => new
                {
                    Autor = string.Join(", ", l.LivroAutores.Select(a => a.Autor.Nome)),
                    Titulo = l.Titulo,
                    Assunto = string.Join(", ", l.LivroAssuntos.Select(s => s.Assunto.Descricao)),
                    Editora = l.Editora,
                    TipoVenda = string.Join(", ", l.LivroValores.Select(v => v.TipoVenda)),
                    Valor = l.LivroValores.Sum(v => (decimal?)v.Valor) ?? 0
                })
                .AsSplitQuery()
                .ToListAsync();

            if (dados == null || !dados.Any())
                return NotFound("Nenhum livro encontrado.");

            using var stream = new MemoryStream();

            // 🔹 Configuração básica do documento
            using (var document = new iTextSharp.text.Document(PageSize.A4, 36, 36, 36, 36)) // margens: left, right, top, bottom
            {
                var writer = PdfWriter.GetInstance(document, stream);
                document.Open();

                // 🔹 Cabeçalho
                var fontTitulo = new Font(Font.HELVETICA, 16, Font.BOLD);
                var titulo = new Paragraph("Relatório de Livros", fontTitulo)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 10f
                };
                document.Add(titulo);

                // 🔹 Fonte padrão para texto
                var fontPadrao = new Font(Font.HELVETICA, 10, Font.NORMAL);

                // 🔹 Tabela de dados
                var tabela = new PdfPTable(6) { WidthPercentage = 100 };
                tabela.SetWidths(new float[] { 20, 25, 20, 15, 10, 10 });

                // Cabeçalhos
                string[] cabecalhos = { "Autor", "Título", "Assunto", "Editora", "Tipo Venda", "Valor" };
                foreach (var cab in cabecalhos)
                {
                    var cellHeader = new PdfPCell(new Phrase(cab, new Font(Font.HELVETICA, 10, Font.BOLD, BaseColor.White)))
                    {
                        BackgroundColor = new BaseColor(0, 70, 140),
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        Padding = 5
                    };
                    tabela.AddCell(cellHeader);
                }

                // Linhas
                foreach (var item in dados)
                {
                    tabela.AddCell(new PdfPCell(new Phrase(item.Autor ?? "", fontPadrao)) { Padding = 4 });
                    tabela.AddCell(new PdfPCell(new Phrase(item.Titulo ?? "", fontPadrao)) { Padding = 4 });
                    tabela.AddCell(new PdfPCell(new Phrase(item.Assunto ?? "", fontPadrao)) { Padding = 4 });
                    tabela.AddCell(new PdfPCell(new Phrase(item.Editora ?? "", fontPadrao)) { Padding = 4 });
                    tabela.AddCell(new PdfPCell(new Phrase(item.TipoVenda ?? "", fontPadrao)) { Padding = 4 });
                    tabela.AddCell(new PdfPCell(new Phrase(item.Valor.ToString("C2"), fontPadrao)) { Padding = 4, HorizontalAlignment = Element.ALIGN_RIGHT });
                }

                document.Add(tabela);
                document.Close();
                writer.Close();
            }

            stream.Position = 0;
            return File(stream.ToArray(), "application/pdf", "relatorio-livros.pdf");
        }


        [HttpGet("relatorio-livro-valores")]
        public async Task<IActionResult> ObterRelatorioLivroValores()
        {
            var dados = await _context.LivroValores
                .Include(v => v.Livro)
                .Select(v => new
                {
                    Livro = v.Livro.Titulo,
                    TipoVenda = v.TipoVenda,
                    Valor = v.Valor
                })
                .AsSplitQuery()
                .ToListAsync();

            if (dados == null || !dados.Any())
                return NotFound("Nenhum registro de valor encontrado.");

            using var stream = new MemoryStream();

            using (var document = new iTextSharp.text.Document(PageSize.A4, 36, 36, 36, 36))
            {
                var writer = PdfWriter.GetInstance(document, stream);
                document.Open();

                // 🔹 Cabeçalho
                var fontTitulo = new Font(Font.HELVETICA, 16, Font.BOLD);
                var titulo = new Paragraph("Relatório de Valores dos Livros", fontTitulo)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 10f
                };
                document.Add(titulo);

                // 🔹 Fonte padrão
                var fontPadrao = new Font(Font.HELVETICA, 10, Font.NORMAL);

                // 🔹 Tabela
                var tabela = new PdfPTable(4) { WidthPercentage = 100 };
                tabela.SetWidths(new float[] { 40, 25, 15, 20 }); // Ajuste proporcional das colunas

                // Cabeçalhos
                string[] cabecalhos = { "Livro", "Tipo de Venda", "Valor", "Data Cadastro" };
                foreach (var cab in cabecalhos)
                {
                    var cellHeader = new PdfPCell(new Phrase(cab, new Font(Font.HELVETICA, 10, Font.BOLD, BaseColor.White)))
                    {
                        BackgroundColor = new BaseColor(0, 70, 140),
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        Padding = 5
                    };
                    tabela.AddCell(cellHeader);
                }

                // Linhas
                foreach (var item in dados)
                {
                    tabela.AddCell(new PdfPCell(new Phrase(item.Livro ?? "", fontPadrao)) { Padding = 4 });
                    tabela.AddCell(new PdfPCell(new Phrase(item.TipoVenda ?? "", fontPadrao)) { Padding = 4 });
                    tabela.AddCell(new PdfPCell(new Phrase(item.Valor.ToString("C2"), fontPadrao))
                    {
                        Padding = 4,
                        HorizontalAlignment = Element.ALIGN_RIGHT
                    });
                }

                document.Add(tabela);

                // 🔹 Rodapé opcional
                document.Add(new Paragraph($"\nGerado em: {DateTime.Now:dd/MM/yyyy HH:mm}", new Font(Font.HELVETICA, 8, Font.ITALIC))
                {
                    Alignment = Element.ALIGN_RIGHT
                });

                document.Close();
                writer.Close();
            }

            stream.Position = 0;
            return File(stream.ToArray(), "application/pdf", "relatorio-livro-valores.pdf");
        }

        [HttpGet("relatorio-autores")]
        public async Task<IActionResult> ObterRelatorioAutores()
        {
            var dados = await _context.Autores
                .Include(a => a.LivroAutores)
                    .ThenInclude(la => la.Livro)
                .Select(a => new
                {
                    Nome = a.Nome,
                    QuantidadeLivros = a.LivroAutores.Count,
                    Livros = string.Join(", ", a.LivroAutores.Select(l => l.Livro.Titulo))
                })
                .AsSplitQuery()
                .OrderBy(a => a.Nome)
                .ToListAsync();

            if (dados == null || !dados.Any())
                return NotFound("Nenhum autor encontrado.");

            using var stream = new MemoryStream();

            using (var document = new iTextSharp.text.Document(PageSize.A4, 36, 36, 36, 36))
            {
                var writer = PdfWriter.GetInstance(document, stream);
                document.Open();

                // 🔹 Cabeçalho
                var fontTitulo = new Font(Font.HELVETICA, 16, Font.BOLD);
                var titulo = new Paragraph("Relatório de Autores", fontTitulo)
                {
                    Alignment = Element.ALIGN_CENTER,
                    SpacingAfter = 10f
                };
                document.Add(titulo);

                // 🔹 Fonte padrão
                var fontPadrao = new Font(Font.HELVETICA, 10, Font.NORMAL);

                // 🔹 Tabela
                var tabela = new PdfPTable(3) { WidthPercentage = 100 };
                tabela.SetWidths(new float[] { 25, 10, 65 }); // Nome / Qtd / Livros

                // Cabeçalhos
                string[] cabecalhos = { "Autor", "Qtd. Livros", "Títulos" };
                foreach (var cab in cabecalhos)
                {
                    var cellHeader = new PdfPCell(new Phrase(cab, new Font(Font.HELVETICA, 10, Font.BOLD, BaseColor.White)))
                    {
                        BackgroundColor = new BaseColor(0, 70, 140),
                        HorizontalAlignment = Element.ALIGN_CENTER,
                        Padding = 5
                    };
                    tabela.AddCell(cellHeader);
                }

                // Linhas
                foreach (var item in dados)
                {
                    tabela.AddCell(new PdfPCell(new Phrase(item.Nome ?? "", fontPadrao)) { Padding = 4 });
                    tabela.AddCell(new PdfPCell(new Phrase(item.QuantidadeLivros.ToString(), fontPadrao))
                    {
                        Padding = 4,
                        HorizontalAlignment = Element.ALIGN_CENTER
                    });
                    tabela.AddCell(new PdfPCell(new Phrase(item.Livros ?? "-", fontPadrao)) { Padding = 4 });
                }

                document.Add(tabela);

                // 🔹 Rodapé
                document.Add(new Paragraph($"\nGerado em: {DateTime.Now:dd/MM/yyyy HH:mm}", new Font(Font.HELVETICA, 8, Font.ITALIC))
                {
                    Alignment = Element.ALIGN_RIGHT
                });

                document.Close();
                writer.Close();
            }

            stream.Position = 0;
            return File(stream.ToArray(), "application/pdf", "relatorio-autores.pdf");
        }

        [HttpGet("exportar")]
        public async Task<IActionResult> Exportar([FromQuery] string formato = "PDF")
        {
            // 1. Obter e projetar os dados da forma mais eficiente possível.
            // Uso de DTO (Data Transfer Object) para evitar passar a entidade completa.
            // Certifique-se de que a query retorna exatamente os dados necessários.
            var dados = await _context.LivroValores
                .Include(v => v.Livro)
                .Select(v => new
                {
                    v.IdLivroValor,
                    v.Livro.Titulo,
                    v.TipoVenda,
                    v.Valor
                })
                .ToListAsync();

            // 2. Validar o caminho e carregar o relatório.
            var caminhoRelatorio = Path.Combine(_env.ContentRootPath, "Reports", "RelatorioLivroValor.mrt");
            if (!System.IO.File.Exists(caminhoRelatorio))
            {
                return NotFound($"O arquivo de relatório não foi encontrado no caminho: {caminhoRelatorio}");
            }

            var report = new StiReport();
            report.Load(caminhoRelatorio);

            // 3. Registrar dados.
            // Use o método `RegData` para registrar a lista de objetos anônimos.
            report.RegData("DataSetLivroValor", dados);

            // 4. Compilar e renderizar.
            report.Compile();
            report.Render(false);

            // 5. Verificar a renderização antes de continuar.
            if (report.Pages.Count == 0)
            {
                return BadRequest("O relatório não conseguiu gerar nenhuma página. Verifique a fonte de dados e o design do relatório.");
            }

            // 6. Exportar e retornar.
            var formatoUpper = formato.ToUpperInvariant();
            var (exportFormat, mimeType) = GetExportDetails(formatoUpper);

            using (var stream = new MemoryStream())
            {
                report.ExportDocument(exportFormat, stream);
                stream.Position = 0;

                string nomeArquivo = $"RelatorioLivroValor_{DateTime.Now:yyyyMMddHHmmss}.{formato.ToLowerInvariant()}";
                return File(stream, mimeType, nomeArquivo);
            }
        }

        private (StiExportFormat format, string mime) GetExportDetails(string formatoUpper)
        {
            return formatoUpper switch
            {
                "EXCEL" => (StiExportFormat.Excel2007, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"),
                "WORD" => (StiExportFormat.Word2007, "application/vnd.openxmlformats-officedocument.wordprocessingml.document"),
                _ => (StiExportFormat.Pdf, "application/pdf"),
            };
        }
    }
}

public class RelatorioLivrosView
{
    public string Autor { get; set; } = string.Empty;
    public string Titulo { get; set; } = string.Empty;
    public string? Assunto { get; set; }
    public string? Editora { get; set; }
    public string? TipoVenda { get; set; }
    public decimal? Valor { get; set; }
}
