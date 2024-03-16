using ExchangeRate.Api.Features.CnbIntegration.Repositories;
using ExchangeRate.Api.Features.CnbIntegration.Services;

namespace ExchangeRate.Api.Features.ExchangeRates.Application.ServiceCollections;

public static class CnbIntegrationServiceCollectionExtensions
{
    public static IServiceCollection AddCnbIntegration(this IServiceCollection services)
    {
        services.AddHttpClient<ICnbIntegrationRepository, CnbIntegrationRepository>(client =>
        {
            client.BaseAddress = new Uri(
                "https://www.cnb.cz/en/financial_markets/foreign_exchange_market/exchange_rate_fixing"
            );
        });

        return services;
    }
}
