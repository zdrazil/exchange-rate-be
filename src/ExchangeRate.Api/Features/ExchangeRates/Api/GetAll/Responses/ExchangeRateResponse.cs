namespace ExchangeRate.Api.Features.ExchangeRates.Api.Contracts;

public class ExchangeRateResponse
{
    public required string Currency { get; set; }

    public required string Country { get; set; }

    public required decimal Amount { get; set; }

    public required string CurrencyCode { get; set; }

    public required decimal Rate { get; set; }
}
