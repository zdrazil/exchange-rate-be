using ExchangeRate.Api.Features.ExchangeRateModels;

namespace ExchangeRate.Api.Features.ExchangeRateApplication.Repository;

public class ExchangeRateRepository : IExchangeRateRepository
{
    public Task<int> GetCountAsync(CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public Task<IEnumerable<ExchangeRateDto>> GetExchangeRatesAsync(
        GetAllExchangeRatesOptions options,
        CancellationToken cancellationToken = default
    ) => throw new NotImplementedException();
}
