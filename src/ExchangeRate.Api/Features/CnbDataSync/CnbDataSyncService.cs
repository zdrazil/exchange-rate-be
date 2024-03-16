using ExchangeRate.Api.Features.CnbIntegration.Models;
using ExchangeRate.Api.Features.CnbIntegration.Services;
using ExchangeRate.Api.Features.ExchangeRateApplication.Service;

namespace ExchangeRate.Api.Features.CnbDataSync;

public interface ICnbDataSyncService
{
    Task SyncDataAsync(CancellationToken cancellationToken = default);
}

public class CnbDataSyncService : ICnbDataSyncService
{
    private readonly ICnbIntegrationService _cnbIntegrationService;
    private readonly IExchangeRateService _exchangeRateService;

    public CnbDataSyncService(
        ICnbIntegrationService cnbIntegrationService,
        IExchangeRateService exchangeRateService
    )
    {
        _cnbIntegrationService = cnbIntegrationService;
        _exchangeRateService = exchangeRateService;
    }

    public async Task SyncDataAsync(CancellationToken cancellationToken = default)
    {
        GetCnbDailyExchangeRatesOptions options = new() { Date = DateTime.Now };

        var exchangeRates = await _cnbIntegrationService.GetDailyExchangeRatesAsync(
            options,
            cancellationToken
        );

        foreach (var exchangeRate in exchangeRates.MapToExchangeRateDto())
        {
            await _exchangeRateService.CreateAsync(exchangeRate, cancellationToken);
        }
    }
}
