using FinancialTransactions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialTransactions.EntityConfigurations
{
    internal sealed class TransactionCofiguration : IEntityTypeConfiguration<Transaction>
    {
        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.ConfigureEntity();
            builder.Property(e => e.FromId).IsRequired();
            builder.Property(e => e.ToId).IsRequired();
            builder.Property(e => e.Value).IsRequired();
            builder.Property(e => e.CreationTime).IsRequired();
        }
    }
}
