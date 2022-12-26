using FluentValidation;
using Mari.Application.Common.Interfaces.Persistence;
using Mari.Domain.Users;
using Mari.Domain.Users.ValueObjects;

namespace Mari.Application.Common.DomainValidators.Users;

public class UserValidator : AbstractValidator<User>
{
}
