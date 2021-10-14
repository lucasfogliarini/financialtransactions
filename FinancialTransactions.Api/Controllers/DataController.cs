using FinancialTransactions.Entities.Abstractions;
using FinancialTransactions.Databases.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Formatter;

namespace FinancialTransactions.Api.Controllers
{
    public abstract class DataController<TEntity> : ODataController where TEntity: class, IEntity
    {
        protected readonly IFinancialTransactionsDatabase _orderDatabase;

        protected DataController(IFinancialTransactionsDatabase orderDatabase)
        {
            _orderDatabase = orderDatabase;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            var entityQuery = _orderDatabase.Query<TEntity>();
            return Ok(entityQuery);
        }

        public IActionResult GetFromOData([FromODataUri] int key)
        {
            var entityQuery = _orderDatabase.Query<TEntity>().FirstOrDefault(e => e.Id == key);
            return Ok(entityQuery);
        }
        public string AuthenticatedUserEmail
        {
            get
            {
                if (!this.User.Identity.IsAuthenticated)
                {
                    throw new ValidationException("Usuário não autenticado.");
                }
                var emailClaim = this.User.FindFirst(ClaimTypes.Email);
                return emailClaim.Value;
            }
        }
    }
}
