using Biblioteca.Application.Common;
using Biblioteca.Application.DTOs;

namespace Biblioteca.Application.Interfaces;

public interface IAutorService
{
    Task<ApiResponse<PagedResult<AutorDto>>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
    Task<ApiResponse<AutorDto?>> GetByIdAsync(int id);
    Task<ApiResponse<AutorDto>> CreateAsync(AutorDto dto);
    Task<ApiResponse<AutorDto>> UpdateAsync(int id, AutorDto dto);
    Task<ApiResponse<bool>> DeleteAsync(int id);
}
