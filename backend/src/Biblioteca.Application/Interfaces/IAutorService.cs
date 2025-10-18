using Biblioteca.Application.Common;
using Biblioteca.Application.DTOs;

namespace Biblioteca.Application.Interfaces;

public interface IAutorService
{
    Task<PagedResult<AutorDto>> GetAllAsync(int pageNumber = 1, int pageSize = 10);
    Task<AutorDto?> GetByIdAsync(int id);
    Task<AutorDto> CreateAsync(AutorDto dto);
    Task<AutorDto> UpdateAsync(int id, AutorDto dto);
    Task DeleteAsync(int id);
}
