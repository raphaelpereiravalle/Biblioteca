using Biblioteca.Application.Services;
using Biblioteca.Application.DTOs;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Moq;
using Xunit;
using System.Threading.Tasks;

public class AssuntoServiceTests
{
    private readonly Mock<IRepository<Assunto>> _repo = new();
    private readonly AssuntoService _svc;

    public AssuntoServiceTests()
    {
        _svc = new AssuntoService(_repo.Object);
    }

    [Fact]
    public async Task CriaAssunto()
    {
        var input = new AssuntoDto { Descricao = "Processo Civil" };
        _repo.Setup(r => r.CreateAsync(It.IsAny<Assunto>())).ReturnsAsync(new Assunto(1, input.Descricao));

        var result = await _svc.CreateAsync(input);
        Assert.Equal(1, result.IdAssunto);
        Assert.Equal("Processo Civil", result.Descricao);
    }
}
