using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Domain.Entities;

public class LivroAutor
{
    public int IdLivro { get; set; }
    public Livro Livro { get; set; } = null!;
    public int IdAutor { get; set; }
    public Autor Autor { get; set; } = null!;
}

public class LivroAssunto
{
    public int IdLivro { get; set; }
    public Livro Livro { get; set; } = null!;
    public int IdAssunto { get; set; }
    public Assunto Assunto { get; set; } = null!;
}

public class LivroValor
{
    [Key]
    public int IdLivroValor { get; set; }
    public int IdLivro { get; set; }
    public Livro Livro { get; set; } = null!;
    public string TipoVenda { get; set; } = string.Empty;
    public decimal Valor { get; set; }
}
