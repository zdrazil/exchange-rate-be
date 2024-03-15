using ExchangeRate.Api.Features.ExchangeRateModels;

namespace ExchangeRate.Api.Features.ExchangeRateApplication.Service;

public interface IExchangeRateService
{
    Task<IEnumerable<ExchangeRateDto>> GetExchangeRatesAsync(
        GetAllExchangeRatesOptions options,
        CancellationToken cancellationToken = default
    );

    Task<int> GetCountAsync(CancellationToken cancellationToken = default);
}
