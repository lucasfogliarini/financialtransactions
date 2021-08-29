namespace FinancialTransactions.Entities.Abstractions
{
    public interface ILegalPerson : IEntity
    {
        public string Email { get; set; }
        public string Name { get; set; }
    }
}
