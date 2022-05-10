using Klapper.Classes;

namespace Klapper.Data;

public class PrinterStatusService
{
    public PrinterInfo? PrinterInfo { get; set; }
    public SystemInfo? SystemInfo { get; set; }
    public PrinterStatus? PrinterStatus { get; set; }

    public bool KlipperIsRunning => SystemInfo?.service_state.klipper.active_state == "active";
    public bool KlippyIsReady => PrinterStatus?.status.webhooks.state == "ready";

    private readonly MoonrakerApiService _api;

    public PrinterStatusService(MoonrakerApiService api)
    {
        _api = api;

        SetKlipperStatusTimer();
    }
    
    private void SetKlipperStatusTimer()
    {
        var timer = new System.Timers.Timer(1000);
        timer.Elapsed += async (sender, args) =>
        {
            await GetKlipperSystemInfo();
            await GetKlipperPrinterInfo();
            await GetKlipperPrinterStatus();
        };
        timer.AutoReset = true;
        timer.Enabled = true;
    }

    public async Task ForceRefreshAll()
    {
        await GetKlipperPrinterInfo();
        await GetKlipperSystemInfo();
        await GetKlipperPrinterStatus();
    }

    private async Task GetKlipperSystemInfo()
    {
        SystemInfo = await _api.GetSystemInfo();
    }
    
    private async Task GetKlipperPrinterInfo()
    {
        PrinterInfo = (await _api.GetPrinterInfo()).result;
        //SystemInfo.
    }
    private async Task GetKlipperPrinterStatus()
    {
        PrinterStatus = (await _api.GetPrinterStatus()).result;
        //SystemInfo.
    }
}