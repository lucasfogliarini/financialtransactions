using FinancialTransactions.Entities.Abstractions;
using System;

namespace FinancialTransactions.Entities
{
    public class Account : IAccount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
