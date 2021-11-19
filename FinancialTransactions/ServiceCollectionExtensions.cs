using FinancialTransactions.Databases.Abstractions;
using FinancialTransactions.Services;
using FinancialTransactions.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FinancialTransactions.EntityFrameworkCore;

namespace FinancialTransactions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Add all type of services: logical services, database services, DbContext, gateway services ...
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static IServiceCollection AddAllServices(this IServiceCollection serviceCollection, string divagandoDbConnectionString)
        {
            serviceCollection.AddLogicalServices();
            serviceCollection.AddDatabases();
            serviceCollection.AddHttpClients();
            serviceCollection.AddDbContext<FinancialTransactionsDbContext>(options => options.UseSqlServer(divagandoDbConnectionString));
            return serviceCollection;
        }
        public static IServiceCollection AddTestServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddLogicalServices();
            serviceCollection.AddDatabases();
            serviceCollection.AddHttpClients();
            serviceCollection.AddDbContext<FinancialTransactionsDbContext>(options => options.UseInMemoryDatabase("divagandoDb"));
            return serviceCollection;
        }
        /// <summary>
        /// Add logical services.
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddLogicalServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<IAccountService, AccountService>();
            serviceCollection.AddTransient<IAccountService, AccountService>();
            serviceCollection.AddTransient<ITransactionService, TransactionService>();
            serviceCollection.AddTransient<ISeedService, SeedService>();
        }
        /// <summary>
        /// Add databases services.
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddDatabases(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IFinancialTransactionsDatabase, FinancialTransactionsDatabase>();
        }
        /// <summary>
        /// Add httpClient services.
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddHttpClients(this IServiceCollection serviceCollection)
        {
        }
    }
}
