namespace ExchangeRate.Api.Features.ExchangeRateModels;

public class GetAllExchangeRatesOptions
{
    public DateTime Date { get; set; }
}

public enum SortOrder
{
    Unsorted,
    Ascending,
    Descending
}
