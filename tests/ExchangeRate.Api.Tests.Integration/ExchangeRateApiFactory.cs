using ExchangeRate.Api.Features.CnbIntegration.Repositories;
using ExchangeRate.Api.Features.CnbIntegration.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace ExchangeRate.Api.Tests.Integration;

public class ExchangeRateApiFactory : WebApplicationFactory<IApiMarker>, IAsyncLifetime
{
    private readonly CnbApiServer _cnbApiServer = new();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
            services.AddHttpClient<ICnbIntegrationRepository, CnbIntegrationRepository>(
                httpClient =>
                {
                    httpClient.BaseAddress = new Uri(_cnbApiServer.Url);
                }
            )
        );
    }

    public Task InitializeAsync()
    {
        _cnbApiServer.Start();
        _cnbApiServer.SetupExchangeRates();

        return Task.CompletedTask;
    }

    public new Task DisposeAsync()
    {
        _cnbApiServer.Dispose();

        return Task.CompletedTask;
    }
}
