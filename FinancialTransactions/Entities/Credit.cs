using FinancialTransactions.Entities.Abstractions;
using System;

namespace FinancialTransactions.Entities
{
    public class Credit : IEntity
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
