using FluentValidation;
using Mari.Domain.Users.ValueObjects;

namespace Mari.Application.Authentication.Commands.Registration;

internal class RegistrationCommandValidator : AbstractValidator<RegistrationCommand>
{
    public RegistrationCommandValidator(
        IValidator<UserId> userIdValidator,
        IValidator<Username> usernameValidator)
    {
        RuleFor(x => x.UserId)
            .SetValidator(userIdValidator);

        RuleFor(x => x.Username)
            .SetValidator(usernameValidator);
    }
}
