using ExchangeRate.Api.Features.ExchangeRateModels;
using ExchangeRate.Api.Features.ExchangeRates.Api.Contracts;

namespace ExchangeRate.Api.Features.ExchangeRates.Api.ContractMapping;

public static class GetAllRequestMapping
{
    public static GetAllExchangeRatesOptions MapToOptions(
        this GetAllExchangeRatesRequest request
    ) => new() { };
}
