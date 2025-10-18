using Biblioteca.Application.DTOs;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.Services;

public class AutorService : IAutorService
{
    private readonly IRepository<Autor> _repo;
    public AutorService(IRepository<Autor> repo) => _repo = repo;

    public async Task<IEnumerable<AutorDto>> GetAllAsync() =>
        (await _repo.GetAllAsync()).Select(a => new AutorDto { IdAutor = a.IdAutor, Nome = a.Nome });

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
