using FinancialTransactions.Entities.Abstractions;
using FinancialTransactions.Inputs.Abstractions;
using System.Threading.Tasks;

namespace FinancialTransactions.Services.Abstractions
{
    public interface IAccountService
    {
        Task SignInAsync(int accountId, string jwToken, AuthenticationInput authenticationInput);
        Task<Account> CreateAsync(string email, string name);
        Task<Account> GetOrCreateAsync(string email, string name = null);
        Account Get(string email);
        Task CreditAsync(int accountId, decimal value);
        void Debit(int accountId, decimal value);
        void Credit(int accountId, decimal value);
    }
}