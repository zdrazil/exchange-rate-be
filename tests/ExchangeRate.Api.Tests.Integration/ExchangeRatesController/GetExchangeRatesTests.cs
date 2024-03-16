using System.Net;
using System.Net.Http.Json;
using System.Web;
using ExchangeRate.Api.Features.ExchangeRateApi;
using ExchangeRate.Api.Features.ExchangeRates.Api.Contracts;
using FluentAssertions;

namespace ExchangeRate.Api.Tests.Integration.ExchangeRatesController;

public class GetExchangeRatesTests : IClassFixture<ExchangeRateApiFactory>
{
    private readonly ExchangeRateApiFactory _apiFactory;

    private readonly HttpClient _client;

    public GetExchangeRatesTests(ExchangeRateApiFactory apiFactory)
    {
        _apiFactory = apiFactory;
        _client = _apiFactory.CreateClient();
        _client.DefaultRequestHeaders.Add("Accept", "application/json;api-version=1.0");
    }

    [Fact]
    public async Task GetExchangeRates_ReturnsExchangeRates()
    {
        // Arrange
        GetAllExchangeRatesRequest request = new() { Page = 1, PageSize = 10 };

        var builder = new UriBuilder { Path = ExchangeRateEndpoints.GetAll };
        var query = HttpUtility.ParseQueryString(builder.Query);
        // query["page"] = "1";
        // query["pageSize"] = "10";
        builder.Query = query.ToString();
        string url = builder.ToString();

        // Act
        var response = await _client.GetAsync(url);

        // Assert
        var exchangeRatesResponse =
            await response.Content.ReadFromJsonAsync<ExchangeRatesResponse>();

        var expectedExchangeRate = new ExchangeRateResponse
        {
            Currency = "CZK",
            Country = "Czech Republic",
            Quantity = 1,
            Code = "CZK",
            Rate = 1,
        };

        exchangeRatesResponse.Should().BeEquivalentTo(expectedExchangeRate);

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        response.Headers.Location!.ToString().Should().Be($"http://localhost/api/exchange-rates");
    }
}
