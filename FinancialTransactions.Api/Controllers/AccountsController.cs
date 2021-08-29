using FinancialTransactions.Entities;
using FinancialTransactions.Inputs.Abstractions;
using FinancialTransactions.Databases.Abstractions;
using FinancialTransactions.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace FinancialTransactions.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountsController : DataController<Account>
    {
        private readonly IAccountService _accountService;
        private readonly Jwt _jwt;

        public AccountsController(IFinancialTransactionsDatabase database,
            IAccountService accountService,
            IOptions<Jwt> jwt) : base(database)
        {
            _accountService = accountService;
            _jwt = jwt.Value;
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignInAsync(AuthenticationInput authenticationInput)
        {
            var account = await _accountService.GetOrCreateAsync(authenticationInput.Email);
            var jwToken  = _jwt.GenerateToken(account.Id, account.Email);
            await _accountService.SignInAsync(account.Id, jwToken, authenticationInput);
            return Ok(jwToken);
        }

        [HttpPost("Credit")]
        public async Task<IActionResult> CreditAsync(decimal value)
        {
            var account = await _accountService.GetOrCreateAsync(AuthenticatedUserEmail);
            await _accountService.CreditAsync(account.Id, value);
            return Ok(value);
        }
    }
}
