using Biblioteca.Domain.Interfaces;
using Biblioteca.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Biblioteca.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly BibliotecaContext _ctx;
    public Repository(BibliotecaContext ctx) => _ctx = ctx;

    public async Task<IEnumerable<T>> GetAllAsync() => await _ctx.Set<T>().ToListAsync();
    public async Task<T?> GetByIdAsync(int id) => await _ctx.Set<T>().FindAsync(id);

    public async Task<T> CreateAsync(T entity)
    {
        _ctx.Set<T>().Add(entity);
        await _ctx.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _ctx.Set<T>().Update(entity);
        await _ctx.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _ctx.Set<T>().Remove(entity);
            await _ctx.SaveChangesAsync();
        }
    }
}
