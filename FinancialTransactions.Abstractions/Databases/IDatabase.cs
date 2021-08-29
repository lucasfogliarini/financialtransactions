using FinancialTransactions.Entities.Abstractions;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinancialTransactions.Databases.Abstractions
{
    public interface IDatabase
    {
        IQueryable<TEntity> Query<TEntity>() where TEntity : class, IEntity;
        void Add<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity;
        void Update<TEntity>(TEntity entity) where TEntity : class, IEntity;
        void Remove<TEntity>(TEntity entity) where TEntity : class, IEntity;
        Task<int> CommitAsync();
        int Commit();
    }
}
