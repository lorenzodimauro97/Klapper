using System.Globalization;

namespace Klapper.Classes;

public class GCodeFile
{
    public string path { get; set; }
    public double modified { get; set; }
    public int size { get; set; }
    public string permissions { get; set; }
}

public class GCodeFileRoot
{
    public List<GCodeFile> result { get; set; }
}

public class GCodeFileDetails
{
    public Extruder Extruder { get; set; }
    
    public int size { get; set; }
    public double modified { get; set; }
    public string slicer { get; set; }
    public string slicer_version { get; set; }
    public int gcode_start_byte { get; set; }
    public int gcode_end_byte { get; set; }
    public int layer_count { get; set; }
    public double object_height { get; set; }
    public double estimated_time { get; set; }
    public double layer_height { get; set; }
    public double first_layer_height { get; set; }
    public double first_layer_extr_temp { get; set; }
    public double first_layer_bed_temp { get; set; }
    public double filament_total { get; set; }
    public List<Thumbnail>? thumbnails { get; set; }

    public double? print_start_time { get; set; }
    public string? job_id { get; set; }
    public string filename { get; set; }

    public string Base64Image { get; set; }

    public string GetEstimatedTime()
    {
        var timespan = TimeSpan.FromSeconds(estimated_time);
        var estimatedTime = string.Empty;
        if (timespan.Days > 0) estimatedTime += $" {timespan.Days} Days,";
        if (timespan.Hours > 0) estimatedTime += $" {timespan.Hours} Hours,";

        estimatedTime += $" {timespan.Minutes} Minutes and {timespan.Seconds} Seconds";
        return estimatedTime;
    }

    public double GetLayer(double z)
    {
        return Math.Ceiling((z - first_layer_height) / layer_height + 1);
    }

    public double GetFlow(double speed) //SEVERE WARNING: I understand none of this shit, this is ripped of the internet. If you know a better solution, make a goddamn pull request!
    {
        if (Extruder == null) return 0;
        return speed * Extruder.nozzle_diameter * layer_height;
    }
}

public class GCodeFileDetailsRoot
{
    public GCodeFileDetails result { get; set; }
}

public class Thumbnail
{
    public int width { get; set; }
    public int height { get; set; }
    public int size { get; set; }
    public string relative_path { get; set; }
}