using Xunit;
using Microsoft.Extensions.DependencyInjection;
using System;
using FinancialTransactions.Services.Abstractions;
using FinancialTransactions.Inputs.Abstractions;
using System.Threading.Tasks;

namespace FinancialTransactions.Tests.Unit
{
    public class TransactionServiceTest : BaseTest
    {
        readonly ITransactionService _transactionService;
        public TransactionServiceTest()
        {
            _transactionService = ServiceProvider.GetService<ITransactionService>();
        }

        [Fact]
        public async Task TransferAsync_ShouldTransferAsync()
        {
            //Given
            var transactionInput = new TransactionInput
            {
                FromId = 1,
                ToId = 1,
                Value = 500,
            };

            //When
            await _transactionService.TransferAsync(transactionInput);

            //Then
            Assert.True(true);
        }
    }
}
