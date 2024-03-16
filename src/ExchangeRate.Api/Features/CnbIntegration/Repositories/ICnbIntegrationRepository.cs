using ExchangeRate.Api.Features.CnbIntegration.Models;

namespace ExchangeRate.Api.Features.CnbIntegration.Repositories;

public interface ICnbIntegrationRepository
{
    Task<IEnumerable<CnbExchangeRateDto>> GetDailyExchangeRatesAsync(
        GetCnbDailyExchangeRatesOptions options,
        CancellationToken cancellationToken = default
    );
}
