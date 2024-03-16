using Movies.Application.Database;

namespace ExchangeRate.Api.Features.ExchangeRates.Application.ServiceCollections;

public static class DatabaseCollectionExtensions
{
    public static IServiceCollection AddDatabase(
        this IServiceCollection services,
        string connectionString
    )
    {
        services.AddSingleton<IDbConnectionFactory>(_ => new NpgsqlConnectionFactory(
            connectionString
        ));

        services.AddSingleton<DbInitializer>();

        return services;
    }
}
