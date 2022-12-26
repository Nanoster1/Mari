using ErrorOr;
using Mari.Server.Filters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Mari.Server.Controllers.Common;

[ApiController]
public class ApiController : ControllerBase
{
    protected ActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0) return Problem();
        if (errors.All(e => e.Type == ErrorType.Validation)) return ValidationProblem(errors);
        return Problem(errors[0]);
    }

    private ActionResult ValidationProblem(IList<Error> errors)
    {
        foreach (var error in errors)
        {
            ModelState.AddModelError(error.Code, error.Description);
        }
        return ValidationProblem(ModelState);
    }

    protected ActionResult Problem(Error error)
    {
        if (ErrorType.Validation == error.Type) return ValidationProblem(new[] { error });

        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _ => 500
        };

        return Problem(title: error.Description, statusCode: statusCode);
    }
}
