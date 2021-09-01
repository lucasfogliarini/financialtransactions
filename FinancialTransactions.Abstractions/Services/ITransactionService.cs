using FinancialTransactions.Entities.Abstractions;
using FinancialTransactions.Inputs.Abstractions;
using System.Threading.Tasks;

namespace FinancialTransactions.Services.Abstractions
{
    public interface ITransactionService
    {
        Task<ITransaction> RequestAsync(TransactionInput transactionInput);
        Task<ITransaction> PromiseAsync(int transactionId);
        Task<ITransaction> TransferAsync(int transactionId);
        Task<ITransaction> TransferAsync(TransactionInput transactionInput);
    }
}
