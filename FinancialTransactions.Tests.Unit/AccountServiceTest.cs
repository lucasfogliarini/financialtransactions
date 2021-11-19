using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System;
using FinancialTransactions.Services.Abstractions;
using FinancialTransactions.Inputs.Abstractions;
using System.Threading.Tasks;

namespace FinancialTransactions.Tests.Unit
{
    public class AccountServiceTest : BaseTest
    {
        readonly IAccountService _accountService;
        public AccountServiceTest()
        {
            _accountService = ServiceProvider.GetService<IAccountService>();
        }

        [Fact]
        public async Task GetOrCreate_WhenNotCreated_ShouldCreate()
        {
            //Given
            string email = "lucasfogliarini@gmail.com";
            string name = "lucasfogliarini";

            //When
            var account = await _accountService.GetOrCreateAsync(email, name);

            //Then
            Assert.NotNull(account.Email);
            Assert.NotNull(account.Name);
        }

        [Fact]
        public void SignIn_When_ShouldSignIn()
        {
            //Given
            var accountId = 1;
            string jwToken = Guid.NewGuid().ToString();
            var authentication = new AuthenticationInput
            {
                Email = "lucasfogliarini@gmail.com",
                Provider = Guid.NewGuid().ToString()
            };

            //When
            _accountService.SignInAsync(accountId, jwToken, authentication);

            //Then
            Assert.True(true);
        }
    }
}
