namespace Biblioteca.Domain.Entities;

public class Autor
{
    public Autor() { }
    public Autor(int idAutor, string nome)
    {
        IdAutor = idAutor;
        Nome = nome;
    }

    public int IdAutor { get; set; }
    public string Nome { get; set; } = string.Empty;
    public ICollection<LivroAutor> LivroAutores { get; set; } = new List<LivroAutor>();
}
