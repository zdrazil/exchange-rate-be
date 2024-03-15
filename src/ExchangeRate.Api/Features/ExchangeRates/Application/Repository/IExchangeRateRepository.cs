using ExchangeRate.Api.Features.ExchangeRateModels;

namespace ExchangeRate.Api.Features.ExchangeRateApplication.Repository;

public interface IExchangeRateRepository
{
    Task<IEnumerable<ExchangeRateDto>> GetExchangeRatesAsync(
        GetAllExchangeRatesOptions options,
        CancellationToken cancellationToken = default
    );

    Task<int> GetCountAsync(CancellationToken cancellationToken = default);
}
