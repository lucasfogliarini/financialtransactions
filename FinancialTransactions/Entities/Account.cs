using FinancialTransactions.Entities.Abstractions;
using System;
using System.Collections.Generic;

namespace FinancialTransactions.Entities
{
    public class Account : ILegalPerson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
        public List<Credit> Credits { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
