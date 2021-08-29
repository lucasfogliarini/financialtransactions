using FinancialTransactions.Gateways.PaymentGateway;
using FinancialTransactions.Databases;
using FinancialTransactions.Databases.Abstractions;
using FinancialTransactions.Services;
using FinancialTransactions.Services.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http.Headers;

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
            serviceCollection.AddGateways();
            serviceCollection.AddDbContext<FinancialTransactionsDbContext>(options => options.UseSqlServer(divagandoDbConnectionString));
            return serviceCollection;
        }
        public static IServiceCollection AddTestServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddLogicalServices();
            serviceCollection.AddDatabases();
            serviceCollection.AddGateways();
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
        /// Add gateway services.
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void AddGateways(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddHttpClient<IPaymentGateway, MercadoPagoGateway>((httpClient) =>
            {
                httpClient.BaseAddress = new Uri("https://api.mercadopago.com/");
                var token = "TEST-8412785768643690-100418-045e0c16d549cf40c6939842e1c4c56f-61441717";
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return new MercadoPagoGateway(httpClient);
            });
        }
    }
}
