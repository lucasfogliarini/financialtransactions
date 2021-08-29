using FinancialTransactions.Entities;
using Microsoft.EntityFrameworkCore;
using FinancialTransactions.Databases.Abstractions;
using FinancialTransactions.Services.Abstractions;
using System;
using System.Linq;
using FinancialTransactions.Inputs.Abstractions;
using FinancialTransactions.Validation;
using System.Threading.Tasks;
using FinancialTransactions.Services;
using FinancialTransactions.Entities.Abstractions;

namespace FinancialTransactions
{
    internal class TransactionService : ITransactionService
    {
        readonly IFinancialTransactionsDatabase _financialTransactionsDatabase;
        readonly IAccountService _accountService;

        public TransactionService(IFinancialTransactionsDatabase financialTransactionsDatabase,
            IAccountService accountService)
        {
            _financialTransactionsDatabase = financialTransactionsDatabase;
            _accountService = accountService;
        }

        public ITransaction Create(TransactionInput transactionInput)
        {
            if (transactionInput.Value <= 0)
            {
                var message = $"Not allowed to transfer less or equal to 0.";
                throw new FluentValidation.ValidationException(message);
            }
            var transaction = new Transaction
            {
                Status = TransactionStatus.Transfering,
                Value = transactionInput.Value,
                FromId = transactionInput.FromId,
                ToId = transactionInput.ToId,
                CreationTime = DateTime.Now
            };
            _financialTransactionsDatabase.Add(transaction);
            _financialTransactionsDatabase.Commit();
            return transaction;
        }

        public async Task TransferAsync(TransactionInput transactionInput)
        {
            var transaction = Create(transactionInput);
            _accountService.Debit(transactionInput.FromId, transactionInput.Value);
            _accountService.Credit(transactionInput.ToId, transactionInput.Value);

            transaction.Status = TransactionStatus.Transfered;

            await _financialTransactionsDatabase.CommitAsync();
        }
    }
}
