using FluentValidation;
using Mari.Domain.Common.Interfaces;
using Mari.Domain.Common.Models;

namespace Mari.Application.Common.DomainValidators.Shared;

public abstract class StringAbstractValidator<T> : AbstractValidator<T>
    where T : ValueObjectWrapper<string, T>, IStringWrapper, new()
{
    protected StringAbstractValidator()
    {
        RuleFor(s => s.Value)
            .NotEmpty();

        if (T.MaxLength != null)
            RuleFor(x => x.Value)
                .MaximumLength((int)T.MaxLength.Value);

        if (T.MinLength != null)
            RuleFor(x => x.Value)
                .MinimumLength((int)T.MinLength.Value);

        if (!string.IsNullOrWhiteSpace(T.Pattern))
            RuleFor(x => x.Value)
                .Matches(T.Pattern);
    }
}
