using Biblioteca.Application.Common;
using Biblioteca.Application.DTOs;

namespace Biblioteca.Application.Interfaces;

public interface ILivroValorService
{
    Task<PagedResult<LivroValorDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
    Task<LivroValorDto?> GetByIdAsync(int id);
    Task<LivroValorDto> CreateAsync(LivroValorDto dto);
    Task<LivroValorDto> UpdateAsync(int id, LivroValorDto dto);
    Task DeleteAsync(int id);
}
