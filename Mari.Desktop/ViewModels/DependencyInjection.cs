using System.Reflection;
using Mari.Desktop.ViewModels.Common.Attributes;
using Mari.Desktop.ViewModels.Common.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace Mari.Desktop.ViewModels;

public static class DependencyInjection
{  
    public static IServiceCollection AddViewModels(this IServiceCollection services)
    {
        typeof(DependencyInjection).Assembly.GetTypes()
            .Where(t => t.IsClass && !t.IsAbstract && 
                        t.Name.EndsWith("ViewModel") && 
                        t.IsSubclassOf(typeof(ViewModelBase)) &&
                        t.GetCustomAttribute<UnregisteredViewModelAttribute>() is null)
            .ToList()
            .ForEach(t => services.AddTransient(t));

        return services;
    } 
}
