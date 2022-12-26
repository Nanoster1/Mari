using FluentValidation;
using Mari.Application.Common.DomainValidators.Shared;
using Mari.Domain.Comments.ValueObjects;

namespace Mari.Application.Common.DomainValidators.Comments.ValueObjects;

public class CommentContentValidator : StringAbstractValidator<CommentContent>
{
}
