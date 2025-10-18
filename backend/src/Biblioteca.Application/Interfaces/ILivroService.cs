using Biblioteca.Application.Common;
using Biblioteca.Application.DTOs;

namespace Biblioteca.Application.Interfaces;

public interface ILivroService
{
    Task<PagedResult<LivroDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
    Task<LivroDto?> GetByIdAsync(int id);
    Task<LivroDto> CreateAsync(LivroDto dto);
    Task<LivroDto> UpdateAsync(int id, LivroDto dto);
    Task DeleteAsync(int id);
}
