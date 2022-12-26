using FluentValidation;
using Mari.Application.Common.Interfaces.Persistence;
using Mari.Domain.Releases;
using Mari.Domain.Releases.Entities;
using Mari.Domain.Releases.ValueObjects;

namespace Mari.Application.Common.DomainValidators.Releases;

public class ReleaseValidator : AbstractValidator<Release>
{
}
