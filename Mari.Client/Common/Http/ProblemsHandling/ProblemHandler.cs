using Mari.Http.Models;
using Microsoft.AspNetCore.Http;
using Throw;

namespace Mari.Client.Common.Http.ProblemsHandling;

public class ProblemHandler
{
    public event Func<ProblemDetails, Task> DefaultProblemEvent;
    public event Func<ProblemDetails, Task> ValidationProblemEvent;
    public event Func<ProblemDetails, Task>UnauthorizedProblemEvent;
    public event Func<ProblemDetails, Task> NotFoundProblemEvent;
    public event Func<ProblemDetails, Task> ErrorProblemEvent;

    public ProblemHandler(ProblemHandlerOptions options)
    {
        DefaultProblemEvent = options.DefaultProblemEvent ?? (_ => Task.CompletedTask);
        ValidationProblemEvent = options.ValidationProblemEvent ?? (_ => Task.CompletedTask);
        UnauthorizedProblemEvent = options.UnauthorizedProblemEvent ?? (_ => Task.CompletedTask);
        NotFoundProblemEvent = options.NotFoundProblemEvent ?? (_ => Task.CompletedTask);
        ErrorProblemEvent = options.ErrorProblemEvent ?? (_ => Task.CompletedTask);
    }

    public void HandleProblem(ProblemDetails problem)
    {
        problem.ThrowIfNull();
        ErrorProblemEvent(problem);
        switch (problem.Status)
        {
            case StatusCodes.Status400BadRequest
            when problem.Errors is not null && problem.Errors.Count > 0:
                ValidationProblemEvent(problem);
                break;
            case StatusCodes.Status401Unauthorized:
                UnauthorizedProblemEvent(problem);
                break;
            case StatusCodes.Status404NotFound:
                NotFoundProblemEvent(problem);
                break;
            default:
                DefaultProblemEvent(problem);
                break;
        }
    }
}
