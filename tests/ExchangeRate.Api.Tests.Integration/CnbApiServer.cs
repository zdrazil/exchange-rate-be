using System.Configuration.Assemblies;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

namespace ExchangeRate.Api.Tests.Integration;

public class CnbApiServer : IDisposable
{
    private WireMockServer _server;
    public string Url => _server.Url;

    public void Start()
    {
        _server = WireMockServer.Start();
    }

    public void SetupExchangeRates()
    {
        _server
            .Given(Request.Create().WithPath("/daily.txt").UsingGet())
            .RespondWith(
                Response
                    .Create()
                    .WithBody(GenerateExchangeRatesBody())
                    .WithHeader("content-type", "text/plain; charset=UTF-8")
                    .WithStatusCode(200)
            );
    }

    private static string GenerateExchangeRatesBody() =>
        @"15.03.2024 #54
Country|Currency|Amount|Code|Rate
Australia|dollar|1|AUD|15.175
Brazil|real|1|BRL|4.624
Bulgaria|lev|1|BGN|12.861
Canada|dollar|1|CAD|17.079
China|renminbi|1|CNY|3.209
Denmark|krone|1|DKK|3.373
EMU|euro|1|EUR|25.155
Hongkong|dollar|1|HKD|2.952
Hungary|forint|100|HUF|6.398
Iceland|krona|100|ISK|16.871
IMF|SDR|1|XDR|30.819
India|rupee|100|INR|27.866
Indonesia|rupiah|1000|IDR|1.481
Israel|new shekel|1|ILS|6.322
Japan|yen|100|JPY|15.520
Malaysia|ringgit|1|MYR|4.909
Mexico|peso|1|MXN|1.383
New Zealand|dollar|1|NZD|14.089
Norway|krone|1|NOK|2.184
Philippines|peso|100|PHP|41.579
Poland|zloty|1|PLN|5.857
Romania|leu|1|RON|5.060
Singapore|dollar|1|SGD|17.275
South Africa|rand|1|ZAR|1.236
South Korea|won|100|KRW|1.737
Sweden|krona|1|SEK|2.233
Switzerland|franc|1|CHF|26.173
Thailand|baht|100|THB|64.412
Turkey|lira|100|TRY|71.676
United Kingdom|pound|1|GBP|29.454
USA|dollar|1|USD|23.093";

    public void Dispose()
    {
        _server.Stop();
        _server.Dispose();
    }
}
