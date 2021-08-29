using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FinancialTransactions.Databases
{
    public class FinancialTransactionsDbContext : DbContext
    {
        public FinancialTransactionsDbContext(DbContextOptions<FinancialTransactionsDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            var thisAssembly = Assembly.GetExecutingAssembly();
            builder.ApplyConfigurationsFromAssembly(thisAssembly);
        }
    }
}
