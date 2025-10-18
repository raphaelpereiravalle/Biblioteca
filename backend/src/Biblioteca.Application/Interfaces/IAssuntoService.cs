using Biblioteca.Application.Common;
using Biblioteca.Application.DTOs;

namespace Biblioteca.Application.Interfaces;

public interface IAssuntoService
{
    Task<ApiResponse<PagedResult<AssuntoDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
    Task<ApiResponse<AssuntoDto?>> GetByIdAsync(int id);
    Task<ApiResponse<AssuntoDto>> CreateAsync(AssuntoDto dto);
    Task<ApiResponse<AssuntoDto>> UpdateAsync(int id, AssuntoDto dto);
    Task<ApiResponse<bool>> DeleteAsync(int id);
}
