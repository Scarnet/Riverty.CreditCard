using System.Reflection;

namespace Riverty.CreditCard.Modules;

/// <summary>
/// Helper class to help with module registerations
/// </summary>
public static class ServiceCollectionExtensions
{
    public static void AddModules(this IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
    {
        var modules = new List<IModule>();

        foreach (var assembly in assemblies)
        {
            modules.AddRange(
                assembly.ExportedTypes
                    .Where(type => typeof(IModule).IsAssignableFrom(type) && !type.IsAbstract && !type.IsInterface)
                    .Select(Activator.CreateInstance).Cast<IModule>());
        }

        foreach (var module in modules)
        {
            module.DefineServices(services, configuration);
        }

        services.AddSingleton(modules as IReadOnlyCollection<IModule>);
    }

    public static void UseModules(this WebApplication app)
    {
        var modules = app.Services.GetRequiredService<IReadOnlyCollection<IModule>>();

        foreach (var module in modules)
        {
            module.DefineEndpoints(app);
        }
    }
}
