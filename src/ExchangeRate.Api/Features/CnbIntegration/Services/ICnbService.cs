using ExchangeRate.Api.Features.CnbIntegration.Models;

namespace ExchangeRate.Api.Features.CnbIntegration.Services;

public interface ICnbService
{
    Task<IEnumerable<CnbExchangeRateDto>> GetDailyExchangeRatesAsync(
        GetCnbDailyExchangeRatesOptions options,
        CancellationToken cancellationToken = default
    );
}
