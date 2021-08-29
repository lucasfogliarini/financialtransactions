namespace FinancialTransactions.Inputs.Abstractions
{
    public class TransactionInput
    {
        public int FromId { get; set; }
        public int ToId { get; set; }
        public decimal Value { get; set; }
    }
}
