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
            .Given(
                Request
                    .Create()
                    .WithPath(
                        "/cs/financni-trhy/devizovy-trh/kurzy-devizoveho-trhu/kurzy-devizoveho-trhu/denni_kurz.txt"
                    )
                    .UsingGet()
            )
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
země|měna|množství|kód|kurz
Austrálie|dolar|1|AUD|15,175
Brazílie|real|1|BRL|4,624
Bulharsko|lev|1|BGN|12,861
Čína|žen-min-pi|1|CNY|3,209
Dánsko|koruna|1|DKK|3,373
EMU|euro|1|EUR|25,155
Filipíny|peso|100|PHP|41,579
Hongkong|dolar|1|HKD|2,952
Indie|rupie|100|INR|27,866
Indonesie|rupie|1000|IDR|1,481
Island|koruna|100|ISK|16,871
Izrael|nový šekel|1|ILS|6,322
Japonsko|jen|100|JPY|15,520
Jižní Afrika|rand|1|ZAR|1,236
Kanada|dolar|1|CAD|17,079
Korejská republika|won|100|KRW|1,737
Maďarsko|forint|100|HUF|6,398
Malajsie|ringgit|1|MYR|4,909
Mexiko|peso|1|MXN|1,383
MMF|ZPČ|1|XDR|30,819
Norsko|koruna|1|NOK|2,184
Nový Zéland|dolar|1|NZD|14,089
Polsko|zlotý|1|PLN|5,857
Rumunsko|leu|1|RON|5,060
Singapur|dolar|1|SGD|17,275
Švédsko|koruna|1|SEK|2,233
Švýcarsko|frank|1|CHF|26,173
Thajsko|baht|100|THB|64,412
Turecko|lira|100|TRY|71,676
USA|dolar|1|USD|23,093
Velká Británie|libra|1|GBP|29,454";

    public void Dispose()
    {
        _server.Stop();
        _server.Dispose();
    }
}
