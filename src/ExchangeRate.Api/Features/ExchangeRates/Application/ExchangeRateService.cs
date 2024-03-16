using ExchangeRate.Api.Features.ExchangeRateApplication.Repository;
using ExchangeRate.Api.Features.ExchangeRateModels;

namespace ExchangeRate.Api.Features.ExchangeRateApplication.Service;

public interface IExchangeRateService
{
    Task<bool> CreateAsync(ExchangeRateDto exchangeRate, CancellationToken token = default);

    Task<IEnumerable<ExchangeRateDto>> GetExchangeRatesAsync(
        GetAllExchangeRatesOptions options,
        CancellationToken cancellationToken = default
    );

    Task<int> GetCountAsync(CancellationToken cancellationToken = default);
}

public class ExchangeRateService : IExchangeRateService
{
    private readonly IExchangeRateRepository _exchangeRateRepository;

    public ExchangeRateService(IExchangeRateRepository exchangeRateRepository)
    {
        _exchangeRateRepository = exchangeRateRepository;
    }

    public async Task<bool> CreateAsync(
        ExchangeRateDto exchangeRate,
        CancellationToken token = default
    ) => await _exchangeRateRepository.CreateAsync(exchangeRate, token);

    public Task<int> GetCountAsync(CancellationToken cancellationToken = default) =>
        throw new NotImplementedException();

    public Task<IEnumerable<ExchangeRateDto>> GetExchangeRatesAsync(
        GetAllExchangeRatesOptions options,
        CancellationToken cancellationToken = default
    )
    {
        return _exchangeRateRepository.GetAllAsync(options, cancellationToken);
    }
}
