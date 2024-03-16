using ExchangeRate.Api.Features.CnbIntegration.Models;
using ExchangeRate.Api.Features.CnbIntegration.Services;
using ExchangeRate.Api.Features.ExchangeRateApi.Mapping;
using ExchangeRate.Api.Features.ExchangeRateApplication.Service;
using ExchangeRate.Api.Features.ExchangeRateContracts;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRate.Api.Features.ExchangeRateApi;

[ApiController]
public class ExchangeRateController : ControllerBase
{
    private readonly IExchangeRateService _exchangeRateService;
    private readonly ICnbService _cnbService;

    public ExchangeRateController(IExchangeRateService exchangeRateService, ICnbService cnbService)
    {
        _exchangeRateService = exchangeRateService;
        _cnbService = cnbService;
    }

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
