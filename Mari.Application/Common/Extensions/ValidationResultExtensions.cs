using ErrorOr;
using FluentValidation.Results;

namespace Mari.Application.Common.Extensions;

public static class ValidationResultExtensions
{
    public static List<Error> ToDomainErrors(this List<ValidationFailure> validationErrors)
    {
        return validationErrors
            .Select(x => Error.Validation(x.PropertyName, x.ErrorMessage))
            .ToList();
    }
}
