namespace Biblioteca.Application.DTOs;

public class LivroDto
{
    public int IdLivro { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Editora { get; set; }
    public int? Edicao { get; set; }
    public string? AnoPublicacao { get; set; }
}
