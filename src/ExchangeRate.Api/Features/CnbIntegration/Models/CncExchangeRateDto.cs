namespace ExchangeRate.Api.Features.CnbIntegration.Models;

public class CnbExchangeRateDto
{
    public required string Currency { get; set; }

    public required string Country { get; set; }

    public required decimal Quantity { get; set; }

    public required string Code { get; set; }

    public required decimal Rate { get; set; }
}
