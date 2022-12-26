using Mari.Http.Models;

namespace Mari.Client.Common.Http.ProblemsHandling;

public class ProblemHandlerOptions
{
    public ProblemHandlerOptions(
        Func<ProblemDetails, Task>? defaultProblemEvent = null,
        Func<ProblemDetails, Task>? validationProblemEvent = null,
        Func<ProblemDetails, Task>? unauthorizedProblemEvent = null,
        Func<ProblemDetails, Task>? notFoundProblemEvent = null,
        Func<ProblemDetails, Task>? errorProblemEvent = null)
    {
        DefaultProblemEvent = defaultProblemEvent;
        ValidationProblemEvent = validationProblemEvent;
        UnauthorizedProblemEvent = unauthorizedProblemEvent;
        NotFoundProblemEvent = notFoundProblemEvent;
        ErrorProblemEvent = errorProblemEvent;
    }

    public Func<ProblemDetails, Task>? DefaultProblemEvent { get; set; }
    public Func<ProblemDetails, Task>? ValidationProblemEvent { get; set; }
    public Func<ProblemDetails, Task>? UnauthorizedProblemEvent { get; set; }
    public Func<ProblemDetails, Task>? NotFoundProblemEvent { get; set; }
    public Func<ProblemDetails, Task>? ErrorProblemEvent { get; set; }
}
