using Biblioteca.Application.DTOs;

namespace Biblioteca.Application.Interfaces;

public interface ILivroValorService
{
    Task<IEnumerable<LivroValorDto>> GetAllAsync();
    Task<LivroValorDto?> GetByIdAsync(int id);
    Task<LivroValorDto> CreateAsync(LivroValorDto dto);
    Task<LivroValorDto> UpdateAsync(int id, LivroValorDto dto);
    Task DeleteAsync(int id);
}
