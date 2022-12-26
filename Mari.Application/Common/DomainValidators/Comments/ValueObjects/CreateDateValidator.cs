using FluentValidation;
using Mari.Application.Common.Interfaces.CommonServices;
using Mari.Domain.Comments.ValueObjects;

namespace Mari.Application.Common.DomainValidators.Comments.ValueObjects;

public class CreateDateValidator : AbstractValidator<CommentCreateDate>
{
}
