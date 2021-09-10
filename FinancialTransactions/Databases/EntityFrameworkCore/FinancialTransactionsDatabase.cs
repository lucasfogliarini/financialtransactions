using FinancialTransactions.Entities.Abstractions;
using FinancialTransactions.Databases.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace FinancialTransactions.EntityFrameworkCore
{
    internal class FinancialTransactionsDatabase : IFinancialTransactionsDatabase
    {
        readonly DbContext _dbContext;
        public FinancialTransactionsDatabase(FinancialTransactionsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<TEntity> Query<TEntity>() where TEntity : class, IEntity
        {
            return _dbContext.Set<TEntity>().AsNoTracking();
        }
        public void Add<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            _dbContext.Add(entity);
        }
        public void AddRange<TEntity>(IEnumerable<TEntity> entities) where TEntity : class, IEntity
        {
            _dbContext.AddRange(entities);
        }

        public int Commit()
        {
            try
            {
                var changes = _dbContext.SaveChanges();
                return changes;
            }
            catch (DbUpdateException ex)
            {
                throw new ValidationException(ex.GetBaseException().Message);
            }
        }
        public async Task<int> CommitAsync()
        {
            try
            {
                var changes = _dbContext.SaveChangesAsync();
                return await changes;
            }
            catch (DbUpdateException ex)
            {
                throw new ValidationException(ex.GetBaseException().Message);
            }
        }
        public void Update<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            _dbContext.Update(entity);
        }
        public void Remove<TEntity>(TEntity entity) where TEntity : class, IEntity
        {
            _dbContext.Remove(entity);
        }
    }
}
