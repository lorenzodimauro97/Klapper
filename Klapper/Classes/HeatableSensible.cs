namespace Klapper.Shared.Components;

public class HeatableSensible
{
    public double pressure_advance { get; set; }
    public double target { get; set; }
    public double power { get; set; }
    public bool can_extrude { get; set; }
    public double smooth_time { get; set; }
    public double? temperature { get; set; }
    public double measured_min_temp { get; set; }
    public double measured_max_temp { get; set; }

    public bool IsHeater { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;

    public double GetPowerPercent()
    {
        return power * 100;
    }
}

public class Temperature
{
    public Temperature(HeatableSensible heatableSensible)
    {
        temperature = heatableSensible.temperature ?? 0;
        Date = heatableSensible.Date;
    }

    public Temperature()
    {
        temperature = 0;
        Date = DateTime.Now;
    }

    public double temperature { get; set; }
    public DateTime Date { get; set; }
}