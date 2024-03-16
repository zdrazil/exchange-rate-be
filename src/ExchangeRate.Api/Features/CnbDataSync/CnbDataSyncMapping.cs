using ExchangeRate.Api.Features.CnbIntegration.Models;
using ExchangeRate.Api.Features.ExchangeRateModels;

namespace ExchangeRate.Api.Features.CnbDataSync;

public static class CnbDataSyncMapping
{
    public static ExchangeRateDto MapToExchangeRateDto(this CnbExchangeRateDto exchangeRate) =>
        new()
        {
            Country = exchangeRate.Country,
            Currency = exchangeRate.Currency,
            Quantity = exchangeRate.Quantity,
            Code = exchangeRate.Code,
            Rate = exchangeRate.Rate
        };

    public static IEnumerable<ExchangeRateDto> MapToExchangeRateDto(
        this IEnumerable<CnbExchangeRateDto> exchangeRates
    ) => exchangeRates.Select(MapToExchangeRateDto);
}
