namespace Klapper.Classes;

public class MoonrakerResult
{
    public string state_message { get; set; }
    public string klipper_path { get; set; }
    public string config_file { get; set; }
    public string software_version { get; set; }
    public string hostname { get; set; }
    public string cpu_info { get; set; }
    public string state { get; set; }
    public string python_path { get; set; }
    public string log_file { get; set; }
}

public class MoonrakerRoot
{
    public MoonrakerResult Result { get; set; }
}