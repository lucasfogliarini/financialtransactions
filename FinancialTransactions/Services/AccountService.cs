using FinancialTransactions.Entities;
using FinancialTransactions.Entities.Abstractions;
using FinancialTransactions.Inputs.Abstractions;
using FinancialTransactions.Databases.Abstractions;
using FinancialTransactions.Services.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;
using FinancialTransactions.Validation;
using Microsoft.EntityFrameworkCore;

namespace FinancialTransactions.Services
{
    internal class AccountService : IAccountService
    {
        readonly IFinancialTransactionsDatabase _financialTransactionsDatabase;
        public AccountService(IFinancialTransactionsDatabase database)
        {
            _financialTransactionsDatabase = database;
        }
        public async Task<ILegalPerson> CreateAsync(string email, string name = null)
        {
            var account = new Account
            {
                Name = name,
                Email = email,
                CreationTime = DateTime.Now
            };
            _financialTransactionsDatabase.Add(account);
            await _financialTransactionsDatabase.CommitAsync();
            return account;
        }

        public async Task<ILegalPerson> GetOrCreateAsync(string email)
        {
            var account = Get(email);
            if (account != null)
                return account;

            account = await CreateAsync(email);
            return account;
        }

        public ILegalPerson Get(string email)
        {
            var account = _financialTransactionsDatabase.Query<Account>().FirstOrDefault(e => e.Email == email);
            return account;
        }

        public async Task SignInAsync(int accountId, string jwToken, AuthenticationInput authenticationInput)
        {
            var authentication = new Authentication
            {
                AccountId = accountId,
                Provider = authenticationInput.Provider,
                JwToken = jwToken,
                CreationTime = DateTime.Now
            };
            _financialTransactionsDatabase.Add(authentication);
            await _financialTransactionsDatabase.CommitAsync();
        }

        public async Task CreditAsync(int accountId, decimal value)
        {
            Credit(accountId, value);
            await _financialTransactionsDatabase.CommitAsync();
        }

        public void Debit(int accountId, decimal value)
        {
            BankingOperation(accountId, -value);
        }

        public void Credit(int accountId, decimal value)
        {
            if (value <= 0)
            {
                var message = $"Not allowed to credit less or equal to 0.";
                throw new FluentValidation.ValidationException(message);
            }
            BankingOperation(accountId, value);
        }

        private void BankingOperation(int accountId, decimal value)
        {
            var account = _financialTransactionsDatabase.Query<Account>().Include(e => e.Credits).FirstOrDefault(e => e.Id == accountId);
            Validator.ValidateNotNullable(account);
            if (value < 0 && account.Balance < value)
            {
                var message = $"Insufficient balance to transfer, the balance should be grather than or equal to {-value}.";
                throw new FluentValidation.ValidationException(message);
            }
            account.Balance += value;
            account.Credits.Add(new Credit
            {
                Value = value,
                CreationTime = DateTime.Now
            });
            _financialTransactionsDatabase.Update(account);
        }
    }
}
