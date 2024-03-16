using ExchangeRate.Api.Features.CnbIntegration.Models;
using ExchangeRate.Api.Features.CnbIntegration.Repositories;

namespace ExchangeRate.Api.Features.CnbIntegration.Services;

public class CnbService : ICnbService
{
    private readonly ICnbIntegrationRepository _cnbIntegrationRepository;

    public CnbService(ICnbIntegrationRepository cnbIntegrationRepository)
    {
        _cnbIntegrationRepository = cnbIntegrationRepository;
    }

    public Task<IEnumerable<CnbExchangeRateDto>> GetDailyExchangeRatesAsync(
        GetCnbDailyExchangeRatesOptions options,
        CancellationToken cancellationToken = default
    ) => _cnbIntegrationRepository.GetDailyExchangeRatesAsync(options, cancellationToken);
}
