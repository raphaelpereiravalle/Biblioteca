using Biblioteca.Application.Common;
using Biblioteca.Application.DTOs;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.Services;

public class AutorService : IAutorService
{
    private readonly IRepository<Autor> _repo;

    public AutorService(IRepository<Autor> repo)
    {
        _repo = repo;
    }

    public async Task<ApiResponse<PagedResult<AutorDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber <= 0) pageNumber = 1;
        if (pageSize <= 0) pageSize = 10;

        var listaAutores = await _repo.GetAllAsync();
        var total = listaAutores.Count();

        var items = listaAutores
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(a => new AutorDto
            {
                IdAutor = a.IdAutor,
                Nome = a.Nome
            })
            .ToList();

        var pagedResult = new PagedResult<AutorDto>
        {
            Items = items,
            TotalCount = total,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        return ApiResponse<PagedResult<AutorDto>>.Ok(
            pagedResult,
            total > 0
                ? $"Foram encontrados {total} autores."
                : "Nenhum autor encontrado."
        );
    }

    public async Task<ApiResponse<AutorDto?>> GetByIdAsync(int id)
    {
        var autor = await _repo.GetByIdAsync(id);

        if (autor == null)
            return ApiResponse<AutorDto?>.NotFound($"Autor com ID {id} não encontrado.");

        var dto = new AutorDto
        {
            IdAutor = autor.IdAutor,
            Nome = autor.Nome
        };

        return ApiResponse<AutorDto?>.Ok(dto, "Autor encontrado com sucesso.");
    }

    public async Task<ApiResponse<AutorDto>> CreateAsync(AutorDto dto)
    {
        var entity = new Autor { Nome = dto.Nome };
        var created = await _repo.CreateAsync(entity);
        dto.IdAutor = created.IdAutor;

        return ApiResponse<AutorDto>.Created(dto, "Autor criado com sucesso.");
    }

    public async Task<ApiResponse<AutorDto>> UpdateAsync(int id, AutorDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<AutorDto>.NotFound($"Autor com ID {id} não encontrado.");

        entity.Nome = dto.Nome;
        await _repo.UpdateAsync(entity);
        dto.IdAutor = entity.IdAutor;

        return ApiResponse<AutorDto>.Ok(dto, "Autor atualizado com sucesso.");
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        var autor = await _repo.GetByIdAsync(id);
        if (autor == null)
            return ApiResponse<bool>.NotFound($"Autor com ID {id} não encontrado.");

        await _repo.DeleteAsync(id);
        return ApiResponse<bool>.Ok(true, "Autor excluído com sucesso.");
    }
}
