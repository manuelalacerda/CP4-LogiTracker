using LogiTracker.Domain.Common;

namespace LogiTracker.Application.Services;

public interface IRepository<T> where T : BaseEntity
{
    Task<IEnumerable<T>> GetAllAsync();

    Task<T?> GetByIdAsync(Guid id);

    Task AddAsync(T entity);

    Task DeleteAsync(Guid id);

    Task<bool> ExistsByIdAsync(Guid id);

    Task SaveChangesAsync();
}