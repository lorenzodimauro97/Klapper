using System.Globalization;

namespace Klapper.Classes;

public class PID
{
    public PID(string message)
    {
        var array = message.Split(" ");
        Kp = float.Parse(array[3].Split("=")[1], CultureInfo.InvariantCulture);
        Ki = float.Parse(array[4].Split("=")[1], CultureInfo.InvariantCulture);
        Kd = float.Parse(array[5].Split("=")[1].Replace("\n//", ""), CultureInfo.InvariantCulture);
    }

    public PID()
    {
        
    }

    public float Kp { get; set; }
    public float Ki { get; set; }
    public float Kd { get; set; }
}