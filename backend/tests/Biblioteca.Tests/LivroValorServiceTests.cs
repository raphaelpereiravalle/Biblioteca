using Biblioteca.Application.Services;
using Biblioteca.Application.DTOs;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Moq;
using Xunit;
using System.Threading.Tasks;

public class LivroValorServiceTests
{
    private readonly Mock<IRepository<LivroValor>> _repo = new();
    private readonly LivroValorService _svc;

    public LivroValorServiceTests()
    {
        _svc = new LivroValorService(_repo.Object);
    }

    [Fact]
    public async Task CriaLivroValor()
    {
        var input = new LivroValorDto { IdLivro = 10, TipoVenda = "Digital", Valor = 59.90m };
        _repo.Setup(r => r.CreateAsync(It.IsAny<LivroValor>())).ReturnsAsync(new LivroValor { IdLivroValor = 7, IdLivro = 10, TipoVenda = "Digital", Valor = 59.90m });

        var result = await _svc.CreateAsync(input);
        Assert.Equal(7, result.Data.IdLivroValor);
        Assert.Equal(10, result.Data.IdLivro);
        Assert.Equal("Digital", result.Data.TipoVenda);
        Assert.Equal(59.90m, result.Data.Valor);
    }
}
