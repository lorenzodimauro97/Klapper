namespace Klapper.Classes;

public class WebcamOptions
{
    public int Width { get; set; } = 320;
    public string VideoID { get; set; }
    public string CanvasID { get; set; }
    public string Filter { get; set; } = null;
}