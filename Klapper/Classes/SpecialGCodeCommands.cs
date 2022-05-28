namespace Klapper.Classes;

public static class SpecialGCodeCommands
{
    public static string ClearSDCard => "SDCARD_RESET_FILE";
    public static string Restart => "RESTART";
    public static string FirmwareRestart => "FIRMWARE_RESTART";
    public static string Home => "G28";
}

public class GcodeStore
{
    public string message { get; set; }
    public double time { get; set; }
    public string type { get; set; }
}

public class GCodeStoreResult
{
    public List<GcodeStore> gcode_store { get; set; }
}

public class GCodeStoreRoot
{
    public GCodeStoreResult result { get; set; }
}