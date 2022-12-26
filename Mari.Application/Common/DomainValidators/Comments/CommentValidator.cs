using System.Data;
using FluentValidation;
using Mari.Application.Common.Interfaces.Persistence;
using Mari.Domain.Comments;
using Mari.Domain.Comments.ValueObjects;
using Mari.Domain.Users.ValueObjects;

namespace Mari.Application.Common.DomainValidators.Comments;

public class CommentValidator : AbstractValidator<Comment>
{
}
