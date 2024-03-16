namespace ExchangeRate.Api.Features.ExchangeRateModels;

public class ExchangeRateDto
{
    public required string Currency { get; set; }

    public required string Country { get; set; }

    public required decimal Quantity { get; set; }

    public required string Code { get; set; }

    public required decimal Rate { get; set; }
}
