namespace ExchangeRate.Api.Features.ExchangeRateApi;

public static class ExchangeRateEndpoints
{
    public const string Base = $"{ApiEndpoints.ApiBase}/exchange-rates";
    public const string Get = $"{Base}/{{code}}";
}
