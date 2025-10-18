using Biblioteca.Application.Common;
using Biblioteca.Application.DTOs;

namespace Biblioteca.Application.Interfaces;

public interface ILivroValorService
{
    Task<ApiResponse<PagedResult<LivroValorDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
    Task<ApiResponse<LivroValorDto?>> GetByIdAsync(int id);
    Task<ApiResponse<LivroValorDto>> CreateAsync(LivroValorDto dto);
    Task<ApiResponse<LivroValorDto>> UpdateAsync(int id, LivroValorDto dto);
    Task<ApiResponse<bool>> DeleteAsync(int id);
}
