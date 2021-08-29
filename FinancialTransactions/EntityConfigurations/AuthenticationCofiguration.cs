using FinancialTransactions.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialTransactions.EntityConfigurations
{
    internal sealed class AuthenticationCofiguration : IEntityTypeConfiguration<Authentication>
    {
        public void Configure(EntityTypeBuilder<Authentication> builder)
        {
            builder.ConfigureEntity();
            builder.Property(e => e.JwToken).IsRequired();
            builder.Property(e => e.AccountId).IsRequired();
        }
    }
}
