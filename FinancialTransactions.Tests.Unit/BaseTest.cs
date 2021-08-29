using FinancialTransactions.Databases.Abstractions;
using FinancialTransactions.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;

namespace FinancialTransactions.Tests.Unit
{
    [Collection("Unit")]
    public abstract class BaseTest
    {

        public IServiceProvider ServiceProvider { get; set; }

        protected readonly IFinancialTransactionsDatabase _healthCareDatabase;

        protected BaseTest()
        {
            ServiceProvider = new ServiceCollection()
                                        .AddTestServices()
                                        .BuildServiceProvider();

            _healthCareDatabase = ServiceProvider.GetService<IFinancialTransactionsDatabase>();
            var seedService = ServiceProvider.GetService<ISeedService>();
            seedService.Seed();
        }
    }
}
