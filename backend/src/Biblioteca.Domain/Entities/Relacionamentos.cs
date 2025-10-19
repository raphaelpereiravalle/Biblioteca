using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Biblioteca.Domain.Entities;

public class LivroAutor
{
    public int IdLivro { get; set; }
    public int IdAutor { get; set; }
    public Livro Livro { get; set; } = null!;
    public Autor Autor { get; set; } = null!;
}

public class LivroAssunto
{
    public int IdLivro { get; set; }
    public int IdAssunto { get; set; }
    public Livro Livro { get; set; } = null!;
    public Assunto Assunto { get; set; } = null!;
}

[Table("LivroValor")]
public class LivroValor
{
    [Key]
    public int IdLivroValor { get; set; }
    public int IdLivro { get; set; }
    public string TipoVenda { get; set; } = string.Empty;
    public decimal Valor { get; set; }

    [ForeignKey(nameof(IdLivro))]
    public Livro Livro { get; set; } = null!;
}
