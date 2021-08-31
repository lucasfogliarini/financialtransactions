namespace FinancialTransactions.Entities.Abstractions
{
    public interface ITransaction
    {
        public TransactionStatus Status { get; set; }
    }

    public enum TransactionStatus
    {
        Requested,
        Transfered,
    }
}
