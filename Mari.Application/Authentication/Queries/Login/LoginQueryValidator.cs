using FluentValidation;
using Mari.Domain.Users.ValueObjects;

namespace Mari.Application.Authentication.Queries.Login;

internal class LoginQueryValidator : AbstractValidator<LoginQuery>
{
    public LoginQueryValidator(
        IValidator<UserId> userIdValidator)
    {
        RuleFor(x => x.UserId)
            .SetValidator(userIdValidator);
    }
}
