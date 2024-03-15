using ExchangeRate.Api.Features.ExchangeRateApi.Mapping;
using ExchangeRate.Api.Features.ExchangeRateApplication.Service;
using ExchangeRate.Api.Features.ExchangeRateContracts;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRate.Api.Features.ExchangeRateApi;

[ApiController]
public class ExchangeRateController : ControllerBase
{
    private readonly IExchangeRateService _exchangeRateService;

    public ExchangeRateController(IExchangeRateService exchangeRateService)
    {
        _exchangeRateService = exchangeRateService;
    }

    [HttpGet(ExchangeRateEndpoints.GetAll)]
    [ProducesResponseType(typeof(ExchangeRateResponse), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetAll(
        [FromQuery] GetAllExchangeRatesRequest request,
        CancellationToken cancellationToken
    )
    {
        var options = request.MapToOptions();

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
