using FinancialTransactions.Databases.Abstractions;
using FinancialTransactions.Entities.Abstractions;
using FinancialTransactions.Inputs.Abstractions;
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

        [HttpPost("request")]
        public async Task<IActionResult> RequestAsync(TransactionInput transactionInput)
        {
            var transaction = await _transactionService.RequestAsync(transactionInput);
            return Ok(transaction);
        }

        [HttpPost("transfer")]
        public async Task<IActionResult> TransferAsync(decimal value, int toId)
        {
            var from = await _accountService.GetOrCreateAsync(AuthenticatedUserEmail);
            var transactionInput = new TransactionInput
            {
                Value = value,
                FromId = from.Id,
                ToId = toId
            };

            var transaction = await _transactionService.TransferAsync(transactionInput);
            return Ok(transaction);
        }

        [HttpPut("{transactionId}/transfer")]
        public async Task<IActionResult> TransferAsync(int transactionId)
        {
            var transaction = await _transactionService.TransferAsync(transactionId);
            return Ok(transaction);
        }

        [HttpPut("{transactionId}/promise")]
        public async Task<IActionResult> PromiseAsync(int transactionId)
        {

            var transaction = await _transactionService.PromiseAsync(transactionId);
            return Ok(transaction);
        }

    }
}
