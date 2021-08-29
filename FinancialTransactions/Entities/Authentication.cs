using FinancialTransactions.Entities.Abstractions;
using System;

namespace FinancialTransactions.Entities
{
    public class Authentication : IEntity
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
        public int AccountId { get; set; }
        public Account Account { get; set; }        
        public string JwToken { get; set; }
        public string Provider { get; set; }
    }
}
