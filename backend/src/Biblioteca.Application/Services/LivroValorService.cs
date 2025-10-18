using Biblioteca.Application.Common;
using Biblioteca.Application.DTOs;
using Biblioteca.Application.Interfaces;
using Biblioteca.Domain.Entities;
using Biblioteca.Domain.Interfaces;

namespace Biblioteca.Application.Services;

public class LivroValorService : ILivroValorService
{
    private readonly IRepository<LivroValor> _repo;

    public LivroValorService(IRepository<LivroValor> repo)
    {
        _repo = repo;
    }

    public async Task<ApiResponse<PagedResult<LivroValorDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10)
    {
        if (pageNumber <= 0) pageNumber = 1;
        if (pageSize <= 0) pageSize = 10;

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

        var paged = new PagedResult<LivroValorDto>
        {
            Items = items,
            TotalCount = total,
            PageNumber = pageNumber,
            PageSize = pageSize
        };

        return ApiResponse<PagedResult<LivroValorDto>>.Ok(
            paged,
            total > 0
                ? $"Foram encontrados {total} registros de valores de livros."
                : "Nenhum valor de livro encontrado."
        );
    }

    public async Task<ApiResponse<LivroValorDto?>> GetByIdAsync(int id)
    {
        var v = await _repo.GetByIdAsync(id);

        if (v == null)
            return ApiResponse<LivroValorDto?>.NotFound($"LivroValor com ID {id} não encontrado.");

        var dto = new LivroValorDto
        {
            IdLivroValor = v.IdLivroValor,
            IdLivro = v.IdLivro,
            TipoVenda = v.TipoVenda,
            Valor = v.Valor
        };

        return ApiResponse<LivroValorDto?>.Ok(dto, "Valor do livro encontrado com sucesso.");
    }

    public async Task<ApiResponse<LivroValorDto>> CreateAsync(LivroValorDto dto)
    {
        var entity = new LivroValor
        {
            IdLivro = dto.IdLivro,
            TipoVenda = dto.TipoVenda,
            Valor = dto.Valor
        };

        var created = await _repo.CreateAsync(entity);
        dto.IdLivroValor = created.IdLivroValor;

        return ApiResponse<LivroValorDto>.Created(dto, "Valor de livro criado com sucesso.");
    }

    public async Task<ApiResponse<LivroValorDto>> UpdateAsync(int id, LivroValorDto dto)
    {
        var entity = await _repo.GetByIdAsync(id);
        if (entity == null)
            return ApiResponse<LivroValorDto>.NotFound($"LivroValor com ID {id} não encontrado.");

        entity.IdLivro = dto.IdLivro;
        entity.TipoVenda = dto.TipoVenda;
        entity.Valor = dto.Valor;

        await _repo.UpdateAsync(entity);
        dto.IdLivroValor = entity.IdLivroValor;

        return ApiResponse<LivroValorDto>.Ok(dto, "Valor de livro atualizado com sucesso.");
    }

    public async Task<ApiResponse<bool>> DeleteAsync(int id)
    {
        var livroValor = await _repo.GetByIdAsync(id);
        if (livroValor == null)
            return ApiResponse<bool>.NotFound($"LivroValor com ID {id} não encontrado.");

        await _repo.DeleteAsync(id);
        return ApiResponse<bool>.Ok(true, "Valor de livro excluído com sucesso.");
    }
}
