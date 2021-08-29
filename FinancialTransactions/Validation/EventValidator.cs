using Divagando.Domain.Entities;
using FluentValidation;
using System;

namespace Divagando.Validation
{
    public class EventValidator : IdentityValidator<Event>
    {
        public EventValidator()
        {
            RuleFor(x => x.Title).NotEmpty();
            RuleFor(x => x.When).GreaterThan(DateTime.Now);
            RuleFor(x => x.Deadline).GreaterThan(DateTime.Now);
            RuleFor(x => x.CreationDate).GreaterThanOrEqualTo(DateTime.Today.AddMinutes(-5));
            RuleFor(x => x.PromotionFee).GreaterThanOrEqualTo(0).LessThan(1);
        }
    }
}
