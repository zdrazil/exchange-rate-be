namespace ExchangeRate.Api.Features.ExchangeRateApi;

public static partial class ExchangeRateEndpoints
{
    private const string Base = $"{ApiEndpoints.ApiBase}/exchange-rates";
    public const string Get = $"{Base}/{{code}}";
    public const string GetAll = Base;
}
