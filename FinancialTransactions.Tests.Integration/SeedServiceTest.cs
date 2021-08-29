using FinancialTransactions.Services.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace FinancialTransactions.Tests.Integration
{
    public class SeedServiceTest
    {
        readonly ISeedService _seedService;
        public SeedServiceTest()
        {
            var serviceProvider = new ServiceCollection()
                                        .AddTestServices()
                                        .BuildServiceProvider();
            _seedService = serviceProvider.GetService<ISeedService>();
        }

        [Fact]
        public void Seed()
        {
            _seedService.Seed();
        }
    }
}
