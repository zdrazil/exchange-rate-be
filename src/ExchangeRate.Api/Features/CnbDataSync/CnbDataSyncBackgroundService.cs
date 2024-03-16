namespace ExchangeRate.Api.Features.CnbDataSync;

public class CnbDataSyncBackgroundService : BackgroundService
{
    private readonly PeriodicTimer _periodicTimer = new(TimeSpan.FromHours(1));
    private readonly ICnbDataSyncService _cnbDataSyncService;

    public CnbDataSyncBackgroundService(ICnbDataSyncService cnbDataSyncService)
    {
        _cnbDataSyncService = cnbDataSyncService;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (
            await _periodicTimer.WaitForNextTickAsync(stoppingToken)
            && !stoppingToken.IsCancellationRequested
        )
        {
            await _cnbDataSyncService.SyncDataAsync(stoppingToken);
        }
    }
}
