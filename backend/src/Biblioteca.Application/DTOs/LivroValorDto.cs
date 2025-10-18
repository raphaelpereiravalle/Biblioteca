namespace Biblioteca.Application.DTOs;

public class LivroValorDto
{
    public int IdLivroValor { get; set; }
    public int IdLivro { get; set; }
    public string TipoVenda { get; set; } = string.Empty;
    public decimal Valor { get; set; }
}
