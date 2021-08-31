using FinancialTransactions.Entities;
using FinancialTransactions.Inputs.Abstractions;
using FinancialTransactions.Databases.Abstractions;
using FinancialTransactions.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FinancialTransactions.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TransactionsController : DataController<Transaction>
    {
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;

        public TransactionsController(IFinancialTransactionsDatabase database,
            IAccountService accountService,
            ITransactionService transactionService) : base(database)
        {
            _accountService = accountService;
            _transactionService = transactionService;
        }

        [HttpPost]
        public async Task<IActionResult> TransferAsync(decimal value, int toId)
        {
            var from = await _accountService.GetOrCreateAsync(AuthenticatedUserEmail);
            var transactionInput = new TransactionInput
            {
                Value = value,
                FromId = from.Id,
                ToId = toId
            };

            await _transactionService.TransferAsync(transactionInput);
            return Ok();
        }

        [HttpPost("request")]
        public async Task<IActionResult> RequestAsync(TransactionInput transactionInput)
        {
            await _transactionService.RequestAsync(transactionInput);
            return Ok(transactionInput);
        }
    }
}
