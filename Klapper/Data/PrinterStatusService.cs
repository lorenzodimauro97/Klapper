using Klapper.Classes;
using Timer = System.Timers.Timer;

namespace Klapper.Data;

public class PrinterStatusService
{
    private readonly MoonrakerApiService _api;

    public PrinterStatusService(MoonrakerApiService api)
    {
        _api = api;

        SetKlipperStatusTimer();
    }

    public PrinterInfo? PrinterInfo { get; set; }
    public SystemInfo? SystemInfo { get; set; }
    public PrinterStatus? PrinterStatus { get; set; }
    
    public GCodeFileDetails? PrintFileDetails { get; set; }

    public bool KlipperIsRunning => SystemInfo?.service_state.klipper.active_state == "active";
    public bool KlippyIsReady => PrinterStatus?.status.webhooks.state == "ready";

    private void SetKlipperStatusTimer()
    {
        var timer = new Timer(1000);
        timer.Elapsed += async (sender, args) =>
        {
            await GetKlipperSystemInfo();
            await GetKlipperPrinterInfo();
            await GetKlipperPrinterStatus();
            await GetPrintFileDetails();
        };
        timer.AutoReset = true;
        timer.Enabled = true;
    }

    public async Task ForceRefreshAll()
    {
        await GetKlipperPrinterInfo();
        await GetKlipperSystemInfo();
        await GetKlipperPrinterStatus();
        await GetPrintFileDetails();
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

    private async Task GetPrintFileDetails()
    {
        if (PrinterStatus?.status.print_stats.filename == null) return;

        if (!string.IsNullOrEmpty(PrinterStatus?.status.print_stats.filename) &&
            PrinterStatus?.status.print_stats.filename != PrintFileDetails?.filename)
        {
            PrintFileDetails = (await _api.GetFileDetails(PrinterStatus.status.print_stats.filename))
                .result;
            
            if (PrintFileDetails.thumbnails?.Count > 0)
                PrintFileDetails.Base64Image = Convert.ToBase64String(await _api.GetImage(PrintFileDetails.thumbnails.Last().relative_path));
        }
    }
}