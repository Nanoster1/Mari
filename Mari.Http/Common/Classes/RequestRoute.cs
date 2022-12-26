using System.Text;
using Mari.Http.Common.Interfaces;

namespace Mari.Http.Common.Classes;

public abstract record RequestRoute : IRequestRoute
{
    public virtual string GetRouteString(string routeTemplate)
    {
        var route = new StringBuilder(routeTemplate);
        var properties = GetType().GetProperties();
        foreach (var property in properties)
        {
            var value = property.GetValue(this)?.ToString();
            if (value is not null)
            {
                route = route.Replace($"{{{property.Name}}}", value);
            }
        }
        return route.ToString();
    }
}
