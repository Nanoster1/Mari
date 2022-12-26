using FluentValidation;
using Mari.Application.Common.DomainValidators.Shared;
using Mari.Domain.Releases.ValueObjects;

namespace Mari.Application.Common.DomainValidators.Releases.ValueObjects;

public class IssueValidator : StringAbstractValidator<Issue>
{
}
