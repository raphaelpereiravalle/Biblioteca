using Biblioteca.Application.Common;
using Biblioteca.Application.DTOs;

namespace Biblioteca.Application.Interfaces;

public interface ILivroService
{
    Task<ApiResponse<PagedResult<LivroDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
    Task<ApiResponse<LivroDto?>> GetByIdAsync(int id);
    Task<ApiResponse<LivroDto>> CreateAsync(LivroDto dto);
    Task<ApiResponse<LivroDto>> UpdateAsync(int id, LivroDto dto);
    Task<ApiResponse<bool>> DeleteAsync(int id);
}
