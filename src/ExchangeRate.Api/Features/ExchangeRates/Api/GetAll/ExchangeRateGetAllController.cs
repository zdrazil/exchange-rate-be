using ExchangeRate.Api.Features.CnbIntegration.Models;
using ExchangeRate.Api.Features.CnbIntegration.Services;
using ExchangeRate.Api.Features.ExchangeRateApplication.Service;
using ExchangeRate.Api.Features.ExchangeRates.Api.ContractMapping;
using ExchangeRate.Api.Features.ExchangeRates.Api.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRate.Api.Features.ExchangeRateApi;

[ApiController]
public class ExchangeRateGetAllController : ControllerBase
{
    public const string Path = ExchangeRateEndpoints.Base;

    private readonly IExchangeRateService _exchangeRateService;
    private readonly ICnbIntegrationService _cnbService;

    public ExchangeRateGetAllController(
        IExchangeRateService exchangeRateService,
        ICnbIntegrationService cnbService
    )
    {
        _exchangeRateService = exchangeRateService;
        _cnbService = cnbService;
    }

    [HttpGet(Path)]
    [ProducesResponseType(typeof(ExchangeRateResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAll(
        [FromQuery] GetAllExchangeRatesRequest request,
        CancellationToken cancellationToken
    )
    {
        var options = request.MapToOptions();

        var res = await _cnbService.GetDailyExchangeRatesAsync(
            new GetCnbDailyExchangeRatesOptions { Date = DateTime.UtcNow.Date },
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
