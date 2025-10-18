using Biblioteca.Application.Services;
using Biblioteca.Application.DTOs;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Moq;
using Xunit;
using System.Threading.Tasks;

public class AutorServiceTests
{
    private readonly Mock<IRepository<Autor>> _repo = new();
    private readonly AutorService _svc;

    public AutorServiceTests()
    {
        _svc = new AutorService(_repo.Object);
    }

    [Fact]
    public async Task CriaAutor()
    {
        var input = new AutorDto { Nome = "Machado de Assis" };
        _repo.Setup(r => r.CreateAsync(It.IsAny<Autor>())).ReturnsAsync(new Autor(1, input.Nome));

        var result = await _svc.CreateAsync(input);
        Assert.Equal(1, result.Data.IdAutor);
        Assert.Equal("Machado de Assis", result.Data.Nome);
    }
}
