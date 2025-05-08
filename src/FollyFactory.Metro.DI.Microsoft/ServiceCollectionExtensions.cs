using FollyFactory.Metro.Commands;
using System.Reflection;
using FollyFactory.Metro;
using FollyFactory.Metro.DI.Microsoft;
using FollyFactory.Metro.Queries;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMetro(this IServiceCollection services,
        params Assembly[] assemblies)
    {
        return services
            .AddMetroCommands(assemblies)
            .AddMetroQueries(assemblies);
    }

    public static IServiceCollection AddMetro(this IServiceCollection services, Action<MetroConfiguration> configure)
    {
        var config = new MetroConfiguration();
        configure?.Invoke(config);

        return services
            .AddMetroCommands(config.AssembliesToScan)
            .AddMetroQueries(config.AssembliesToScan);
    }

    public static IServiceCollection AddMetroCommands(this IServiceCollection services, params Assembly[] assemblies)
    {
        if (assemblies.Length > 0)
        {
            services.Scan(scan => scan
                .FromAssemblies(assemblies!)
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            );

            services.Scan(scan => scan
                .FromAssemblies(assemblies!)
                .AddClasses(classes => classes.AssignableTo(typeof(ICommandValidator<>)))
                .AsSelfWithInterfaces()
            .WithSingletonLifetime()
            );
        }

        services.AddTransient<ICommandDispatcher, MicrosoftCommandDispatcher>();

        return services;
    }

    public static IServiceCollection AddMetroQueries(this IServiceCollection services, params Assembly[] assemblies)
    {
        if (assemblies.Length > 0)
        {
            services.Scan(scan => scan
                .FromAssemblies(assemblies!)
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryHandler<,>)))
                .AsImplementedInterfaces()
                .WithTransientLifetime()
            );

            services.Scan(scan => scan
                .FromAssemblies(assemblies!)
                .AddClasses(classes => classes.AssignableTo(typeof(IQueryValidator<>)))
                .AsSelfWithInterfaces()
                .WithSingletonLifetime()
            );
        }

        services.AddTransient<IQueryProcessor, MicrosoftQueryProcessor>();
        return services;
    }
}
