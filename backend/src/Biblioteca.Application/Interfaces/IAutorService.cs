using Biblioteca.Application.DTOs;

namespace Biblioteca.Application.Interfaces;

public interface IAutorService
{
    Task<IEnumerable<AutorDto>> GetAllAsync();
    Task<AutorDto?> GetByIdAsync(int id);
    Task<AutorDto> CreateAsync(AutorDto dto);
    Task<AutorDto> UpdateAsync(int id, AutorDto dto);
    Task DeleteAsync(int id);
}
