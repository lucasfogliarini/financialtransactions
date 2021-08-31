using FinancialTransactions.Inputs.Abstractions;
using System.Threading.Tasks;

namespace FinancialTransactions.Services.Abstractions
{
    public interface ITransactionService
    {
        Task RequestAsync(TransactionInput transactionInput);
        Task TransferAsync(int transactionId);
        Task TransferAsync(TransactionInput transactionInput);
    }
}
