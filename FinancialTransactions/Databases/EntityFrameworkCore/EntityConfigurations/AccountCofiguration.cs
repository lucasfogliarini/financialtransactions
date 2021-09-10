using FinancialTransactions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialTransactions.EntityFrameworkCore
{
    internal sealed class AccountCofiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ConfigureEntity();
            builder.Property(e => e.Email).IsRequired();
            builder.Property(e => e.Name).IsRequired();
        }
    }
}
