using FinancialTransactions.Inputs.Abstractions;
using System.Threading.Tasks;

namespace FinancialTransactions.Services.Abstractions
{
    public interface ITransactionService
    {
        Task TransferAsync(TransactionInput transactionInput);
    }
}
