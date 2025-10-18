using Biblioteca.Application.DTOs;
using Biblioteca.Application.Services;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;
using Moq;
using System.Threading.Tasks;
using Xunit;

public class LivroServiceTests
{
    [Fact]
    public async Task DeveCriarLivroComSucesso()
    {
        var repo = new Mock<IRepository<Livro>>();
        repo.Setup(r => r.CreateAsync(It.IsAny<Livro>()))
            .ReturnsAsync((Livro l) => { l.IdLivro = 1; return l; });

        var service = new LivroService(repo.Object);
        var dto = new LivroDto { Titulo = "Teste Jurídico" };
        var result = await service.CreateAsync(dto);

        Assert.Equal(1, result.Data.IdLivro);
        Assert.Equal("Teste Jurídico", result.Data.Titulo);
    }
}
