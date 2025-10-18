using Biblioteca.Application.Common;
using Biblioteca.Application.DTOs;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.Services;

public class LivroValorService : ILivroValorService
{
    private readonly IRepository<LivroValor> _repo;
    public LivroValorService(IRepository<LivroValor> repo) => _repo = repo;

    public async Task<PagedResult<LivroValorDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
    {
        var all = await _repo.GetAllAsync();
        var total = all.Count();

        var items = all
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(v => new LivroValorDto
            {
                IdLivroValor = v.IdLivroValor,
                IdLivro = v.IdLivro,
                TipoVenda = v.TipoVenda,
                Valor = v.Valor
            })
            .ToList();

        return new PagedResult<LivroValorDto>
        {
            Items = items,
            TotalCount = total,
            PageNumber = pageNumber,
            PageSize = pageSize
        };
    }

    public async Task<LivroValorDto?> GetByIdAsync(int id)
    {
        var v = await _repo.GetByIdAsync(id);
        return v == null ? null : new LivroValorDto { IdLivroValor = v.IdLivroValor, IdLivro = v.IdLivro, TipoVenda = v.TipoVenda, Valor = v.Valor };
    }

    public async Task<LivroValorDto> CreateAsync(LivroValorDto dto)
    {
        var entity = new LivroValor { IdLivro = dto.IdLivro, TipoVenda = dto.TipoVenda, Valor = dto.Valor };
        var created = await _repo.CreateAsync(entity);
        dto.IdLivroValor = created.IdLivroValor;
        return dto;
    }

    public async Task<LivroValorDto> UpdateAsync(int id, LivroValorDto dto)
    {
        var entity = await _repo.GetByIdAsync(id) ?? throw new KeyNotFoundException("LivroValor não encontrado.");
        entity.IdLivro = dto.IdLivro;
        entity.TipoVenda = dto.TipoVenda;
        entity.Valor = dto.Valor;
        await _repo.UpdateAsync(entity);
        dto.IdLivroValor = entity.IdLivroValor;
        return dto;
    }

    public async Task DeleteAsync(int id) => await _repo.DeleteAsync(id);
}
