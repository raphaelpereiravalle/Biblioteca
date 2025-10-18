using Biblioteca.Application.Common;
using Biblioteca.Application.DTOs;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.Services;

public class AutorService : IAutorService
{
    private readonly IRepository<Autor> _repo;

    public AutorService(IRepository<Autor> repo) => _repo = repo;

    public async Task<PagedResult<AutorDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
    {
        var ListaAutor = await _repo.GetAllAsync();
        var total = ListaAutor.Count();

        var items = ListaAutor
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(a => new AutorDto
            {
                IdAutor = a.IdAutor,
                Nome = a.Nome
            })
            .ToList();

        return new PagedResult<AutorDto>
        {
            Items = items,
            TotalCount = total,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<AutorDto?> GetByIdAsync(int id)
    {
        var a = await _repo.GetByIdAsync(id);
        return a == null ? null : new AutorDto { IdAutor = a.IdAutor, Nome = a.Nome };
    }

    public async Task<AutorDto> CreateAsync(AutorDto dto)
    {
        var entity = new Autor { Nome = dto.Nome };
        var created = await _repo.CreateAsync(entity);
        dto.IdAutor = created.IdAutor;
        return dto;
    }

    public async Task<AutorDto> UpdateAsync(int id, AutorDto dto)
    {
        var entity = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Autor não encontrado.");
        entity.Nome = dto.Nome;
        await _repo.UpdateAsync(entity);
        dto.IdAutor = entity.IdAutor;
        return dto;
    }

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
}
