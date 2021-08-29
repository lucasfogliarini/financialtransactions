using System;

namespace FinancialTransactions.Entities.Abstractions
{
    public interface IEntity
    {
        public int Id { get; set; }
        public DateTime CreationTime { get; set; }
    }
}