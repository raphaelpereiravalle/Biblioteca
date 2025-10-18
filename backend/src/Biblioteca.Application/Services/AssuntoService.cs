using Biblioteca.Application.DTOs;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.Services;

public class AssuntoService : IAssuntoService
{
    private readonly IRepository<Assunto> _repo;
    public AssuntoService(IRepository<Assunto> repo) => _repo = repo;

    public async Task<IEnumerable<AssuntoDto>> GetAllAsync() =>
        (await _repo.GetAllAsync()).Select(a => new AssuntoDto { IdAssunto = a.IdAssunto, Descricao = a.Descricao });

    public async Task<AssuntoDto?> GetByIdAsync(int id)
    {
        var a = await _repo.GetByIdAsync(id);
        return a == null ? null : new AssuntoDto { IdAssunto = a.IdAssunto, Descricao = a.Descricao };
    }

    public async Task<AssuntoDto> CreateAsync(AssuntoDto dto)
    {
        var entity = new Assunto { Descricao = dto.Descricao };
        var created = await _repo.CreateAsync(entity);
        dto.IdAssunto = created.IdAssunto;
        return dto;
    }

    public async Task<AssuntoDto> UpdateAsync(int id, AssuntoDto dto)
    {
        var entity = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("Assunto não encontrado.");
        entity.Descricao = dto.Descricao;
        await _repo.UpdateAsync(entity);
        dto.IdAssunto = entity.IdAssunto;
        return dto;
    }

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
}
