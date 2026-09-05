using LogiTracker.Application.Services;
using LogiTracker.Domain.Common;
using LogiTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace LogiTracker.Infrastructure;

public class Repository<T> : IRepository<T> where T : BaseEntity
{
    private readonly ApplicationDbContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<T?> GetByIdAsync(Guid id)
    {
        return await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await _dbSet.FindAsync(id);

        if (entity == null)
            throw new KeyNotFoundException("Registro não encontrado.");

        _dbSet.Remove(entity);
    }

    public async Task<bool> ExistsByIdAsync(Guid id)
    {
        return await _dbSet.AnyAsync(x => x.Id == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}