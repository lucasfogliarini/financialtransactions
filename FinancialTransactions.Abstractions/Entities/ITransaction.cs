namespace FinancialTransactions.Entities.Abstractions
{
    public interface ITransaction : IEntity
    {
        public TransactionStatus Status { get; set; }
    }

    public enum TransactionStatus
    {
        Requested,
        Transferred,
    }
}
