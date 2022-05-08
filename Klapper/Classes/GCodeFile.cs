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