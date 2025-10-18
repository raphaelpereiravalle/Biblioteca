using Biblioteca.Application.DTOs;

namespace Biblioteca.Application.Interfaces;

public interface IAssuntoService
{
    Task<IEnumerable<AssuntoDto>> GetAllAsync();
    Task<AssuntoDto?> GetByIdAsync(int id);
    Task<AssuntoDto> CreateAsync(AssuntoDto dto);
    Task<AssuntoDto> UpdateAsync(int id, AssuntoDto dto);
    Task DeleteAsync(int id);
}
