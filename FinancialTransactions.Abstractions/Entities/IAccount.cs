namespace FinancialTransactions.Entities.Abstractions
{
    public interface IAccount : IEntity
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
