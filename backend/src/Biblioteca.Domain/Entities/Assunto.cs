namespace Biblioteca.Domain.Entities;

public class Assunto
{
    public Assunto() { }

    public Assunto(int id, string descricao)
    {
        IdAssunto = id;
        Descricao = descricao;
    }

    public int IdAssunto { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public ICollection<LivroAssunto> LivroAssuntos { get; set; } = new List<LivroAssunto>();
}
