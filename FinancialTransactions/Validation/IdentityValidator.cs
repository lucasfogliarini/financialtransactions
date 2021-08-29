using FinancialTransactions.Entities.Abstractions;
using FluentValidation;
using System.Linq;

namespace FinancialTransactions.Validation
{
    public class IdentityValidator<T> : AbstractValidator<T> where T: IIdentity
    {
        public IdentityValidator()
        {
            RuleFor(x => x.Identity).Length(1, 30);
            RuleFor(x => x.Identity).NotEmpty();

            When(i => i.Identity != null, () =>
            {
                RuleFor(x => x.Identity).Must(e => !e.Contains(' ')).WithMessage("'Identity' não deve ter espaço em branco.");
                RuleFor(x => x.Identity).Must(e => !e.Any(char.IsUpper)).WithMessage("'Identity' não deve ter caracter UpperCase.");
                RuleFor(x => x.Identity).Matches("^[a-zA-Z0-9\\-_]").WithMessage("'Identity' deve ter apenas caracters de (A a Z) e (0 a 9).");
            });
        }
    }
}
