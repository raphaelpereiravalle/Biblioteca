namespace Biblioteca.Domain.Entities;

public class Livro
{
    public Livro() { }

    public Livro(string titulo)
    {
        Titulo = titulo;

    }

    public int IdLivro { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string? Editora { get; set; }
    public int? Edicao { get; set; }
    public string? AnoPublicacao { get; set; }
    public ICollection<LivroAutor> LivroAutores { get; set; } = new List<LivroAutor>();
    public ICollection<LivroAssunto> LivroAssuntos { get; set; } = new List<LivroAssunto>();
    public ICollection<LivroValor> LivroValores { get; set; } = new List<LivroValor>();
}
