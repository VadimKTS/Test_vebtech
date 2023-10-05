using Microsoft.EntityFrameworkCore;
using TestApplicationForVebtech.DataAccess.DbPatterns.Interfaces;

namespace TestApplicationForVebtech.DataAccess.DbPatterns
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly MyDbContext _dbContext;
        public GenericRepository(MyDbContext context) 
        {
            _dbContext = context;
        }

        public async Task<T> CreateAsync(T t)
        {
            _dbContext.Set<T>().Add(t);
            await _dbContext.SaveChangesAsync();
            return t;
        }

        public async Task<T> ReadAsync(Guid id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task UpdateAsync(T t)
        {
            _dbContext.Set<T>().Update(t);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T t)
        {
            _dbContext.Set<T>().Remove(t);
            await _dbContext.SaveChangesAsync();
        }
    }
}
