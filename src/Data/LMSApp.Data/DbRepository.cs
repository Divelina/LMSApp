
namespace LMSApp.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using LMSApp.Data.Common;
    using Microsoft.EntityFrameworkCore;

    public class DbRepository<TEntity> : IRepository<TEntity>, IDisposable
         where TEntity : class
    {
        private readonly LMSAppContext context;
        private DbSet<TEntity> dbSet;

        public DbRepository(LMSAppContext context)
        {
            this.context = context;
            this.dbSet = this.context.Set<TEntity>();
        }

        public Task AddAsync(TEntity entity)
        {
            return this.dbSet.AddAsync(entity);
        }

        public IQueryable<TEntity> All()
        {
            return this.dbSet;
        }

        public void Edit(TEntity entity)
        {
            dbSet.Attach(entity);
            context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(TEntity entity)
        {
            this.dbSet.Remove(entity);
        }

        public async Task<TEntity> FindbyId(string id)
        {
            return await this.dbSet.FindAsync(id);
        }

        public void Dispose()
        {
            this.context.Dispose();
        }

        public Task<int> SaveChangesAsync()
        {
            return this.context.SaveChangesAsync();
        }


    }
}
