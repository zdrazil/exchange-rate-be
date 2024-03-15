using ExchangeRate.Api.Features.ExchangeRateApplication.Repository;
using ExchangeRate.Api.Features.ExchangeRateApplication.Service;

namespace ExchangeRate.Api.Features.ExchangeRates.Application.ServiceCollections;

public static class ExchangeRatesServiceCollectionExtensions
{
    public static IServiceCollection AddExchangeRates(this IServiceCollection services)
    {
        services.AddSingleton<IExchangeRateService, ExchangeRateService>();
        services.AddSingleton<IExchangeRateRepository, ExchangeRateRepository>();

        return services;
    }
}
