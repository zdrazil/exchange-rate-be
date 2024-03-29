using ExchangeRate.Api.Features.CnbIntegration.Models;

namespace ExchangeRate.Api.Features.CnbIntegration.Services;

public interface ICnbIntegrationService
{
    Task<IEnumerable<CnbExchangeRateDto>> GetDailyExchangeRatesAsync(
        GetCnbDailyExchangeRatesOptions options,
        CancellationToken cancellationToken = default
    );
}

public class CnbIntegrationService : ICnbIntegrationService
{
    private readonly HttpClient _httpClient;

    public CnbIntegrationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<CnbExchangeRateDto>> GetDailyExchangeRatesAsync(
        GetCnbDailyExchangeRatesOptions options,
        CancellationToken cancellationToken = default
    )
    {
        var date = options.Date.ToString("dd.MM.yyyy");
        var response = await _httpClient.GetAsync($"/daily.txt?date={date}", cancellationToken);

        var content = await response.Content.ReadAsStringAsync(cancellationToken);

        if (response is null || !response.IsSuccessStatusCode || content is null)
        {
            throw new Exception("Failed to get exchange rates from CNB");
        }

        var exchangeRates = content
            .Split('\n')
            .Skip(2)
            .Select(line =>
            {
                var parts = line.Split('|');

                var country = parts[0];
                var currency = parts[1];
                var quantity = decimal.Parse(parts[2]);
                var code = parts[3];
                // Replace ',' with '.' to ensure correct parsing for decimal values
                var rate = decimal.Parse(
                    parts[4].Replace(',', '.'),
                    System.Globalization.CultureInfo.InvariantCulture
                );

                return new CnbExchangeRateDto
                {
                    Country = country,
                    Currency = currency,
                    Quantity = quantity,
                    Code = code,
                    Rate = rate
                };
            });

        return exchangeRates;
    }
}
