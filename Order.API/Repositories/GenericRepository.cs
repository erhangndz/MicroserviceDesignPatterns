using Microsoft.EntityFrameworkCore;

namespace Order.API.Repositories
{
    public class GenericRepository<TEntity>(AppDbContext context) : IRepository<TEntity> where TEntity : class
    {
        public async Task<TEntity> GetByIdAsync(int id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
           return await context.Set<TEntity>().AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
           await context.AddAsync(entity);
           await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TEntity entity)
        {
            context.Update(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            context.Remove(entity);
            await context.SaveChangesAsync();
        }
    }
}
