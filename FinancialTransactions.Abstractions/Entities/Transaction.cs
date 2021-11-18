using System;

namespace FinancialTransactions.Entities.Abstractions
{
    public class Transaction : IEntity
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public int FromId { get; set; }
        public Account From { get; set; }
        public int ToId { get; set; }
        public Account To { get; set; }
        public TransactionStatus Status { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime? PromisedTime { get; set; }
        public DateTime? TransferTime { get; set; }
    }

    public enum TransactionStatus
    {
        Requested,
        Promised,
        Transferred,
    }
}
