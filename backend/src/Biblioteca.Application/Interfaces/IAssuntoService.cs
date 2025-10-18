using Biblioteca.Application.Common;
using Biblioteca.Application.DTOs;

namespace Biblioteca.Application.Interfaces;

public interface IAssuntoService
{
    Task<PagedResult<AssuntoDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
    Task<AssuntoDto?> GetByIdAsync(int id);
    Task<AssuntoDto> CreateAsync(AssuntoDto dto);
    Task<AssuntoDto> UpdateAsync(int id, AssuntoDto dto);
    Task DeleteAsync(int id);
}
