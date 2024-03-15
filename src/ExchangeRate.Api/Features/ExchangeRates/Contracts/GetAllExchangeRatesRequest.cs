using ExchangeRate.Api.Features.Paging;

namespace ExchangeRate.Api.Features.ExchangeRateContracts;

public class GetAllExchangeRatesRequest : PagedRequest
{
    public string? SortBy { get; init; }
}
