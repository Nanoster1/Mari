using FluentValidation;

namespace Mari.Application.Common.Shared.Paging;

public class PageRequestValidator<T> : AbstractValidator<T>
    where T : PageRequest
{
    public PageRequestValidator()
    {
        RuleFor(x => x.Page)
            .GreaterThan(0);

        RuleFor(x => x.PageSize)
            .GreaterThan(0);
    }
}
