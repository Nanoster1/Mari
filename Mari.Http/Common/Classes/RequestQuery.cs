using System.Web;
using Mari.Http.Common.Interfaces;

namespace Mari.Http.Common.Classes;

public abstract record RequestQuery : IRequestQuery
{
    public virtual string GetQueryString()
    {
        var query = HttpUtility.ParseQueryString(string.Empty);
        var properties = GetType().GetProperties();
        foreach (var property in properties)
        {
            var value = property.GetValue(this);
            var stringValue = value?.ToString();
            if (string.IsNullOrWhiteSpace(stringValue)) continue;
            query[property.Name] = stringValue;
        }
        return query.ToString() ?? string.Empty;
    }
}
