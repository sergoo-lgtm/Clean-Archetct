using E_tickets.Infrastructure.Data;
using eTickets.Models;

namespace eTickets.Services;

public class GenericRepository<T>:IGenericRepository<T> where T : class
{
    
    private readonly AppDbContext _dbContext;
    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
   public async Task AddAsync(T entity)
    {
        await _dbContext .Set<T>().AddAsync(entity);
    }

    public async Task UpdateAsync(T entity)
    {
        _dbContext.Set<T>().Update(entity);
    }

    public async Task RemoveAsync(int id)
    {
        var entity = await _dbContext.Set<T>().FindAsync(id);
        if (entity != null)
            _dbContext.Set<T>().Remove(entity);
    }

    public async Task<T> GetByIdAsync(int id)
    {
        return await _dbContext.Set<T>().FindAsync(id);
    }

    public IQueryable<T> GetAll  => _dbContext.Set<T>().AsQueryable();
}