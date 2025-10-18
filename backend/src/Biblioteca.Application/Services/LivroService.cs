using Biblioteca.Application.Common;
using Biblioteca.Application.DTOs;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.Services;

public class LivroService : ILivroService
{
    private readonly IRepository<Livro> _repo;

    public LivroService(IRepository<Livro> repo) => _repo = repo;

    public async Task<PagedResult<LivroDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber <= 0) pageNumber = 1;
        if (pageSize <= 0) pageSize = 10;

        var allLivros = await _repo.GetAllAsync();
        var total = allLivros.Count();

        var livros = allLivros
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(l => new LivroDto
            {
                IdLivro = l.IdLivro,
                Titulo = l.Titulo,
                Editora = l.Editora,
                Edicao = l.Edicao,
                AnoPublicacao = l.AnoPublicacao
            })
            .ToList();

        return new PagedResult<LivroDto>
        {
            Items = livros,
            TotalCount = total,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<LivroDto?> GetByIdAsync(int id)
    {
        var l = await _repo.GetByIdAsync(id);
        return l == null ? null : new LivroDto
        {
            IdLivro = l.IdLivro,
            Titulo = l.Titulo,
            Editora = l.Editora,
            Edicao = l.Edicao,
            AnoPublicacao = l.AnoPublicacao
        };
    }

    public async Task<LivroDto> CreateAsync(LivroDto dto)
    {
        var entity = new Livro
        {
            Titulo = dto.Titulo,
            Editora = dto.Editora,
            Edicao = dto.Edicao,
            AnoPublicacao = dto.AnoPublicacao
        };
        var created = await _repo.CreateAsync(entity);
        dto.IdLivro = created.IdLivro;
        return dto;
    }

    public async Task<LivroDto> UpdateAsync(int id, LivroDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException("Livro não encontrado.");

        entity.Titulo = dto.Titulo;
        entity.Editora = dto.Editora;
        entity.Edicao = dto.Edicao;
        entity.AnoPublicacao = dto.AnoPublicacao;

        await _repo.UpdateAsync(entity);
        return dto;
    }

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
}
