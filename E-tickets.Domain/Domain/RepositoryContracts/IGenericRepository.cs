using eTickets.Models;

namespace eTickets.Services;

public interface IGenericRepository<T>
{
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(int id);
    Task<T> GetByIdAsync(int id);
    IQueryable<T> GetAll { get; }
}

