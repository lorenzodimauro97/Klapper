using Klapper.Shared.Components;

namespace Klapper.Classes;

public class Python
{
    public List<object> version { get; set; }
    public string version_string { get; set; }
}

public class CpuInfo
{
    public int cpu_count { get; set; }
    public string bits { get; set; }
    public string processor { get; set; }
    public string cpu_desc { get; set; }
    public string serial_number { get; set; }
    public string hardware_desc { get; set; }
    public string model { get; set; }
    public int total_memory { get; set; }
    public string memory_units { get; set; }
}

public class SdInfo
{
}

public class VersionParts
{
    public string major { get; set; }
    public string minor { get; set; }
    public string build_number { get; set; }
}

public class ReleaseInfo
{
}

public class Distribution
{
    public string name { get; set; }
    public string id { get; set; }
    public string version { get; set; }
    public VersionParts version_parts { get; set; }
    public string like { get; set; }
    public string codename { get; set; }
    public ReleaseInfo release_info { get; set; }
}

public class Virtualization
{
    public string virt_type { get; set; }
    public string virt_identifier { get; set; }
}

public class IpAddress
{
    public string family { get; set; }
    public string address { get; set; }
    public bool is_link_local { get; set; }
}

public class Wlp3s0b1
{
    public string mac_address { get; set; }
    public List<IpAddress> ip_addresses { get; set; }
}

public class Network
{
    public Wlp3s0b1 wlp3s0b1 { get; set; }
}

public class Klipper : ISystemInfoStatus
{
    public string active_state { get; set; }
    public string sub_state { get; set; }
}

public class Webcamd : ISystemInfoStatus
{
    public string active_state { get; set; }
    public string sub_state { get; set; }
}

public class Moonraker : ISystemInfoStatus
{
    public string active_state { get; set; }
    public string sub_state { get; set; }
}

public class ServiceState
{
    public Klipper klipper { get; set; }
    public Webcamd webcamd { get; set; }
    public Moonraker moonraker { get; set; }
}

public class SystemInfo
{
    public Python python { get; set; }
    public CpuInfo cpu_info { get; set; }
    public SdInfo sd_info { get; set; }
    public Distribution distribution { get; set; }
    public Virtualization virtualization { get; set; }
    public Network network { get; set; }
    public List<string> available_services { get; set; }
    public ServiceState service_state { get; set; }
}