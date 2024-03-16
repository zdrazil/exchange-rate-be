using ExchangeRate.Api.Features.Paging;

namespace ExchangeRate.Api.Features.ExchangeRates.Api.Contracts;

public class GetAllExchangeRatesRequest : PagedRequest
{
    public string? SortBy { get; init; }
}
