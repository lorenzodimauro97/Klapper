namespace Klapper.Classes;

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
public class Error
{
    public int code { get; set; }
    public string message { get; set; }
    public string traceback { get; set; }
}

public class ErrorRoot
{
    public Error error { get; set; }
}

public class ErrorMessage
{
    public string message { get; set; }
    public string error { get; set; }
}