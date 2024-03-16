using ExchangeRate.Api.Features.CnbIntegration.Services;

namespace ExchangeRate.Api.Features.ExchangeRates.Application.ServiceCollections;

public static class CnbIntegrationServiceCollectionExtensions
{
    public static IServiceCollection AddCnbIntegration(this IServiceCollection services)
    {
        services.AddSingleton<ICnbIntegrationService, CnbIntegrationService>();

        services.AddHttpClient<ICnbIntegrationService, CnbIntegrationService>(client =>
        {
            // TODO: Uncomment when we know everything else is working.
            //   client.BaseAddress = new Uri(
            //     "https://www.cnb.cz/en/financial_markets/foreign_exchange_market/exchange_rate_fixing"
            // );
            client.BaseAddress = new Uri("https://www.example.com");
        });

        return services;
    }
}
