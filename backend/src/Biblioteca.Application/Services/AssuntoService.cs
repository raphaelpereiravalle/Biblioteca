using Biblioteca.Application.Common;
using Biblioteca.Application.DTOs;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.Services;

public class AssuntoService : IAssuntoService
{
    private readonly IRepository<Assunto> _repo;

    public AssuntoService(IRepository<Assunto> repo)
    {
        _repo = repo;
    }

    public async Task<ApiResponse<PagedResult<AssuntoDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber <= 0) pageNumber = 1;
        if (pageSize <= 0) pageSize = 10;

        var lista = await _repo.GetAllAsync();
        var total = lista.Count();

        var items = lista
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(a => new AssuntoDto
            {
                IdAssunto = a.IdAssunto,
                Descricao = a.Descricao
            })
            .ToList();

        var paged = new PagedResult<AssuntoDto>
        {
            Items = items,
            TotalCount = total,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        return ApiResponse<PagedResult<AssuntoDto>>.Ok(
            paged,
            total > 0
                ? $"Foram encontrados {total} assuntos."
                : "Nenhum assunto encontrado."
        );
    }

    public async Task<ApiResponse<AssuntoDto?>> GetByIdAsync(int id)
    {
        var assunto = await _repo.GetByIdAsync(id);

        if (assunto == null)
            return ApiResponse<AssuntoDto?>.NotFound($"Assunto com ID {id} não encontrado.");

        var dto = new AssuntoDto
        {
            IdAssunto = assunto.IdAssunto,
            Descricao = assunto.Descricao
        };

        return ApiResponse<AssuntoDto?>.Ok(dto, "Assunto encontrado com sucesso.");
    }

    public async Task<ApiResponse<AssuntoDto>> CreateAsync(AssuntoDto dto)
    {
        var entity = new Assunto { Descricao = dto.Descricao };
        var created = await _repo.CreateAsync(entity);
        dto.IdAssunto = created.IdAssunto;

        return ApiResponse<AssuntoDto>.Created(dto, "Assunto criado com sucesso.");
    }

    public async Task<ApiResponse<AssuntoDto>> UpdateAsync(int id, AssuntoDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<AssuntoDto>.NotFound($"Assunto com ID {id} não encontrado.");

        entity.Descricao = dto.Descricao;
        await _repo.UpdateAsync(entity);
        dto.IdAssunto = entity.IdAssunto;

        return ApiResponse<AssuntoDto>.Ok(dto, "Assunto atualizado com sucesso.");
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<bool>.NotFound($"Assunto com ID {id} não encontrado.");

        await _repo.DeleteAsync(id);
        return ApiResponse<bool>.Ok(true, "Assunto excluído com sucesso.");
    }
}
