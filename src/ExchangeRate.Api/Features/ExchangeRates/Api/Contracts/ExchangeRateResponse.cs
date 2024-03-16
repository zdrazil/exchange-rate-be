namespace ExchangeRate.Api.Features.ExchangeRates.Api.Contracts;

public class ExchangeRateResponse
{
    public required string Currency { get; set; }

    public required string Country { get; set; }

    public required decimal Quantity { get; set; }

    public required string Code { get; set; }

    public required decimal Rate { get; set; }
}
