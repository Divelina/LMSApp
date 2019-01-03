
namespace LMSApp.Data.Common
{
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<TEntity>
        where TEntity : class
    {
        IQueryable<TEntity> All();

        Task<TEntity> FindbyId(string id);

        Task AddAsync(TEntity entity);

        void Delete(TEntity entity);

        Task<int> SaveChangesAsync();

    }
}
