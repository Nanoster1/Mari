using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mari.Server.Filters;

public class MariAuthorizeFilter : ActionFilterAttribute, IAsyncAuthorizationFilter
{
    public AuthorizationPolicy Policy { get; }

    public MariAuthorizeFilter()
    {
        Policy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
    }

    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }

        if (context.Filters.Any(item => item is IAllowAnonymousFilter))
            return;

        var policyEvaluator = context.HttpContext.RequestServices.GetRequiredService<IPolicyEvaluator>();
        var authenticateResult = await policyEvaluator.AuthenticateAsync(Policy, context.HttpContext);
        var authorizeResult = await policyEvaluator.AuthorizeAsync(Policy, authenticateResult, context.HttpContext, context);

        if (authorizeResult.Challenged)
        {
            context.Result = new JsonResult(new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Detail = "You are not authorized to access this resource."
            })
            {
                StatusCode = StatusCodes.Status401Unauthorized
            };
        }

        if (authorizeResult.Forbidden)
        {
            context.Result = new JsonResult(new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "Forbidden",
                Detail = "You are not allowed to access this resource."
            })
            {
                StatusCode = StatusCodes.Status403Forbidden
            };
        }
    }
}
