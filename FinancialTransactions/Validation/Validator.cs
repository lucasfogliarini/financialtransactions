using FinancialTransactions.Entities.Abstractions;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FinancialTransactions.Validation
{
    public static class Validator
    {
        public static void ValidateNotNullable<TEntity>(TEntity entity) where TEntity : IEntity
        {
            if (entity == null)
            {
                var entityName = typeof(TEntity).Name;
                var message = $"The {entityName} can't be null.";
                throw new ValidationException(message);
            }
        }

        public static void ValidateNotExists<TEntity>(TEntity entity, int entityId) where TEntity : IEntity
        {
            if (entity == null)
            {
                entity = Activator.CreateInstance<TEntity>();
                entity.Id = entityId;
                var entityName = entity.GetType().Name;
                var message = $"There is no {entityName} with the id '{entity.Id}'";
                throw new ValidationException(message);
            }
        }

        public static void ValidateNotExists<TEntity>(TEntity entity, string entityValue) where TEntity : IEntity
        {
            if (entity == null)
            {
                var entityName = typeof(TEntity).Name;
                var message = $"There is no {entityName} with the value '{entityValue}'";
                throw new ValidationException(message);
            }
        }

        public static void ValidateAlreadyExists<TIdentity>(TIdentity entity) where TIdentity : IIdentity
        {
            var entityName = entity.GetType().Name;
            var message = $"There is already a {entityName} with the identity '{entity.Identity}'";
            throw new ValidationException(message);
        }

        public static void ValidateAlreadyExists<TEntity>() where TEntity : IEntity
        {
            var entityName = typeof(TEntity).Name;
            var message = $"There is already a {entityName}";
            throw new ValidationException(message);
        }
    }
}
