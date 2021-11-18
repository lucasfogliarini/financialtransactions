using FinancialTransactions.Entities.Abstractions;
using FinancialTransactions.Inputs.Abstractions;
using System.Threading.Tasks;

namespace FinancialTransactions.Services.Abstractions
{
    public interface ITransactionService
    {
        Task<Transaction> RequestAsync(TransactionInput transactionInput);
        Task<Transaction> PromiseAsync(int transactionId);
        Task<Transaction> TransferAsync(int transactionId);
        Task<Transaction> TransferAsync(TransactionInput transactionInput);
    }
}
