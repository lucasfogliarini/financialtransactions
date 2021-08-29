using FinancialTransactions.Entities.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancialTransactions.EntityConfigurations
{
    public static class EntityConfigurationExtensions
    {
        public static PropertyBuilder<TProperty> PricePrecision<TProperty>(this PropertyBuilder<TProperty> propertyBuilder)
        {
            return propertyBuilder.HasPrecision(22, 4);
        }

        public static PropertyBuilder<TProperty> FeePrecision<TProperty>(this PropertyBuilder<TProperty> propertyBuilder)
        {
            return propertyBuilder.HasPrecision(5, 4);
        }
        public static void ConfigureKeys<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IIdentity
        {
            builder.Property(e => e.Identity).HasMaxLength(50);
            builder.HasAlternateKey(e => e.Identity);
        }

        public static void ConfigureEntity<TEntity>(this EntityTypeBuilder<TEntity> builder) where TEntity : class, IEntity
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.CreationTime).IsRequired();
        }
    }
}
