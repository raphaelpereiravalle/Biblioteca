using Biblioteca.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stimulsoft.Report;
using System.Reflection.Metadata;
using System.Xml.Linq;

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
            // A atribuição da licença deve ser feita em um ponto de entrada da aplicação, como Program.cs, e não no construtor do controlador.
            // Para versões de teste, a atribuição vazia pode ser necessária, mas idealmente deve ser configurada globalmente.
            // StiLicense.Key = "";
        }


        [HttpGet("relatorio")]
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

            if (!dados.Any())
                return NotFound("Nenhum livro encontrado.");

            using var stream = new MemoryStream();
            var document = new Document(PageSize.A4);
            var writer = PdfWriter.GetInstance(document, stream);
            document.Open();

            // 🔹 Cabeçalho
            var titulo = new Paragraph("Relatório de Livros", new Font(Font.FontFamily.HELVETICA, 16, Font.BOLD));
            titulo.Alignment = Element.ALIGN_CENTER;
            document.Add(titulo);
            document.Add(new Paragraph("\n"));

            // 🔹 Tabela
            var tabela = new PdfPTable(6); // 6 colunas
            tabela.WidthPercentage = 100;
            tabela.SetWidths(new float[] { 20, 25, 20, 15, 10, 10 });

            // Cabeçalhos
            string[] cabecalhos = { "Autor", "Título", "Assunto", "Editora", "Tipo Venda", "Valor" };
            foreach (var cab in cabecalhos)
            {
                var cell = new PdfPCell(new Phrase(cab, new Font(Font.FontFamily.HELVETICA, 10, Font.BOLD)));
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                tabela.AddCell(cell);
            }

            // Linhas
            foreach (var item in dados)
            {
                tabela.AddCell(new Phrase(item.Autor ?? ""));
                tabela.AddCell(new Phrase(item.Titulo ?? ""));
                tabela.AddCell(new Phrase(item.Assunto ?? ""));
                tabela.AddCell(new Phrase(item.Editora ?? ""));
                tabela.AddCell(new Phrase(item.TipoVenda ?? ""));
                tabela.AddCell(new Phrase(item.Valor.ToString("C2")));
            }

            document.Add(tabela);
            document.Close();
            writer.Close();

            return File(stream.ToArray(), "application/pdf", "relatorio-livros.pdf");
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
