using ExchangeRate.Api.Features.CnbIntegration.Models;
using ExchangeRate.Api.Features.ExchangeRates.Api.ContractMapping;
using ExchangeRate.Api.Features.ExchangeRates.Api.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRate.Api.Features.ExchangeRateApi;

public partial class ExchangeRateController
{
    [HttpGet(ExchangeRateEndpoints.GetAll)]
    [ProducesResponseType(typeof(ExchangeRateResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAll(
        [FromQuery] GetAllExchangeRatesRequest request,
        CancellationToken cancellationToken
    )
    {
        var options = request.MapToOptions();

        var res = await _cnbService.GetDailyExchangeRatesAsync(
            new GetCnbDailyExchangeRatesOptions { Date = DateTime.Now },
            cancellationToken
        );

        var exchangeRates = await _exchangeRateService.GetExchangeRatesAsync(
            options,
            cancellationToken
        );

        var exchangeRatesCount = await _exchangeRateService.GetCountAsync(cancellationToken);

        var response = exchangeRates.MapToResponse(
            request.Page,
            request.PageSize,
            exchangeRatesCount
        );

        return Ok(response);
    }
}
