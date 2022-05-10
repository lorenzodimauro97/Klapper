namespace Klapper.Classes;

public class PrinterStatus
{
    public Status status { get; set; }
    public double eventtime { get; set; }
}

public class PrinterStatusRoot
{
    public PrinterStatus result { get; set; }
}

public class Status
{
    public VirtualSdcard virtual_sdcard { get; set; }
    public Webhooks webhooks { get; set; }
    public PrintStats print_stats { get; set; }
}