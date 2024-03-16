using ExchangeRate.Api.Features.ExchangeRateContracts;
using ExchangeRate.Api.Features.ExchangeRateModels;

namespace ExchangeRate.Api.Features.ExchangeRateApi.Mapping;

public static class ExchangeRateContractMapping
{
    public static ExchangeRateResponse MapToResponse(this ExchangeRateDto exchangeRate) =>
        new()
        {
            Currency = exchangeRate.Currency,
            Country = exchangeRate.Country,
            Quantity = exchangeRate.Quantity,
            Code = exchangeRate.Code,
            Rate = exchangeRate.Rate,
        };

    public static ExchangeRatesResponse MapToResponse(
        this IEnumerable<ExchangeRateDto> exchangeRates,
        int page,
        int pageSize,
        int totalCount
    ) =>
        new()
        {
            Items = exchangeRates.Select(MapToResponse),
            Page = page,
            PageSize = pageSize,
            Total = totalCount,
        };

    public static GetAllExchangeRatesOptions MapToOptions(
        this GetAllExchangeRatesRequest request
    ) => new() { };
}
