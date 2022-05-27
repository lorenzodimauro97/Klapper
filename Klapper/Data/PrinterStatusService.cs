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

    public PrinterInfo? PrinterInfo { get; private set; }
    public SystemInfo? SystemInfo { get; private set; }
    public PrinterStatus? PrinterStatus { get; private set; }
    public GCodeFileDetails? PrintFileDetails { get; private set; }
    public GcodeMove? GcodeMove { get; set; }
    public Toolhead? Toolhead { get; set; }
    
    public bool KlipperIsRunning => SystemInfo?.service_state.klipper.active_state == "active";
    public bool PrinterIsPrinting => PrinterStatus?.status.print_stats.state == "printing";
    public bool KlippyIsReady => PrinterStatus?.status.webhooks.state == "ready";
    public bool PrinterIsAdjustingScrews { get; set; }
    public int PrinterScrewAdjustIndex { get; set; }

    private void SetKlipperStatusTimer()
    {
        var timer = new Timer(1000);
        timer.Elapsed += async (_, _) =>
        {
            await ForceRefreshAll();
            timer.Start();
        };
        timer.AutoReset = false;
        timer.Enabled = true;
    }

    public async Task ForceRefreshAll()
    {
        await GetKlipperPrinterInfo();
        await GetKlipperSystemInfo();
        await GetKlipperPrinterStatus();
        await GetPrintFileDetails();
        await GetGcodeMoveToolHead();
    }

    private async Task GetKlipperSystemInfo()
    {
        SystemInfo = await _api.GetSystemInfo();
    }

    private async Task GetKlipperPrinterInfo()
    {
        PrinterInfo = (await _api.GetPrinterInfo())?.result;
    }

    private async Task GetKlipperPrinterStatus()
    {
        PrinterStatus = (await _api.GetPrinterStatus())?.result;
    }

    private async Task GetPrintFileDetails()
    {
        if (!string.IsNullOrEmpty(PrinterStatus?.status.print_stats.filename) &&
            PrinterStatus?.status.print_stats.filename != PrintFileDetails?.filename)
        {
            PrintFileDetails = (await _api.GetFileDetails(PrinterStatus.status.print_stats.filename))
                .result;

            if (PrintFileDetails.thumbnails?.Count > 0)
                PrintFileDetails.Base64Image =
                    Convert.ToBase64String(await _api.GetFile(PrintFileDetails.thumbnails.Last().relative_path,
                        "gcodes"));
        }
    }

    private async Task GetGcodeMoveToolHead()
    {
        if(!KlippyIsReady) return;
        var objects = await _api.GetObject<MoonrakerQueryResultObject>("gcode_move&toolhead", false);
        if (objects == null) return;
        GcodeMove = objects.result.status.gcode_move;
        Toolhead = objects.result.status.toolhead;
    }
}