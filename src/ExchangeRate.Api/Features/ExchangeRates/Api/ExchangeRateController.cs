using ExchangeRate.Api.Features.CnbIntegration.Services;
using ExchangeRate.Api.Features.ExchangeRateApplication.Service;
using Microsoft.AspNetCore.Mvc;

namespace ExchangeRate.Api.Features.ExchangeRateApi;

[ApiController]
public partial class ExchangeRateController : ControllerBase
{
    private readonly IExchangeRateService _exchangeRateService;
    private readonly ICnbIntegrationService _cnbService;

    public ExchangeRateController(
        IExchangeRateService exchangeRateService,
        ICnbIntegrationService cnbService
    )
    {
        _exchangeRateService = exchangeRateService;
        _cnbService = cnbService;
    }
}
