using Biblioteca.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.NETCore;

namespace Biblioteca.Api.Controllers;

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

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var dados = await _context.Livros
            .Include(l => l.LivroAutores).ThenInclude(la => la.Autor)
            .Include(l => l.LivroAssuntos).ThenInclude(la => la.Assunto)
            .Include(l => l.LivroValores)
            .Select(l => new RelatorioLivrosView
            {
                Autor = string.Join(", ", l.LivroAutores.Select(a => a.Autor.Nome)),
                Titulo = l.Titulo,
                Assunto = string.Join(", ", l.LivroAssuntos.Select(s => s.Assunto.Descricao)),
                Editora = l.Editora,
                TipoVenda = string.Join(", ", l.LivroValores.Select(v => v.TipoVenda)),
                Valor = l.LivroValores.Sum(v => (decimal?)v.Valor)
            })
            .ToListAsync();
        return Ok(dados);
    }

    [HttpGet("livros-valores")]
    public async Task<IActionResult> Exportar([FromQuery] string formato = "PDF")
    {
        var dados = await _context.LivroValores
            .Include(v => v.Livro)
            .Select(v => new { v.IdLivroValor, v.Livro.Titulo, v.TipoVenda, v.Valor })
            .ToListAsync();

        var caminho = Path.Combine(_env.ContentRootPath, "Reports", "RelatorioLivroValor.rdlc");
        using var rel = new LocalReport();
        using var fs = System.IO.File.OpenRead(caminho);
        rel.LoadReportDefinition(fs);
        rel.DataSources.Clear();
        rel.DataSources.Add(new ReportDataSource("DataSetLivroValor", dados));

        string mime, ext;
        var bytes = rel.Render(formato.ToUpper(), null, out mime, out _, out ext, out _, out _);
        var nome = $"Relatorio_LivroValor.{ext}";
        return File(bytes, mime, nome);
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
