using ExchangeRate.Api.Features.CnbDataSync;

namespace ExchangeRate.Api.Features.ExchangeRates.Application.ServiceCollections;

public static class CnbDataSyncCollectionExtensions
{
    public static IServiceCollection AddCnbDataSync(this IServiceCollection services)
    {
        services.AddSingleton<ICnbDataSyncService, CnbDataSyncService>();
        services.AddHostedService<CnbDataSyncBackgroundService>();
        return services;
    }
}
