using FinancialTransactions.Databases.Abstractions;
using FinancialTransactions.Entities.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancialTransactions.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class AuthenticationsController : DataController<Authentication>
    {
        public AuthenticationsController(IFinancialTransactionsDatabase database) : base(database)
        {
        }
    }
}
