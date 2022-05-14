using System.Net;
using System.Text.Json;
using System.Text.RegularExpressions;
using Klapper.Classes;
using Klapper.Shared.Components;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace Klapper.Data;

public class MoonrakerApiService
{
    private readonly RestClient _client;
    private readonly IConfiguration _configuration;
    private readonly ILogger<MoonrakerApiService> _logger;

    public MoonrakerApiService(ILogger<MoonrakerApiService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
        Log = new List<(string, string, string)>();

        BaseUrl = _configuration.GetValue<string>("HostSettings:Address");
        try
        {
            _client = new RestClient(new RestClientOptions
            {
                BaseUrl = new Uri(BaseUrl),
                MaxTimeout = 300000
            });
        }
        catch (Exception ex)
        {
            _logger.LogCritical("Failed to initialize RestSharp! Error: {Error}", ex.Message);
            Environment.Exit(1);
        }

        _client.UseSerializer<SpanJsonSerializationAdapter>();
    }

    public string BaseUrl { get; }

    public List<(string, string, string)> Log { get; }

    /*public async Task<MoonrakerObjectListClass> GetFullObjectList()
    {
        var request = new RestRequest("/printer/objects/list");
        var result = await _client.ExecuteAsync<MoonrakerObjectListClass>(request);
        return result.Data;
    }*/

    public async Task<(bool, string)> RunGCode(string query)
    {
        var request = new RestRequest($"/printer/gcode/script?script={query}", Method.Post);
        return await LaunchPostRequest(request, query);
    }

    public async Task<(bool, string)> GetGCodeList()
    {
        var request = new RestRequest("/printer/gcode/help", Method.Post);
        return await LaunchPostRequest(request, "HELP", true);
    }

    public async Task<(bool, string)> PauseCancelResumePrint(int code)
    {
        var requestUrl = code switch
        {
            0 => "/printer/print/pause",
            1 => "/printer/print/cancel",
            2 => "/printer/print/resume",
            _ => string.Empty
        };

        var request = new RestRequest(requestUrl, Method.Post);
        return await LaunchPostRequest(request);
    }

    public async Task<bool> PrintFile(string query)
    {
        var request = new RestRequest($"/printer/print/start?filename={query}", Method.Post);
        var result = await _client.ExecuteAsync(request);
        return result.IsSuccessful;
    }

    public async Task<GCodeFileRoot> GetFiles()
    {
        var request = new RestRequest("/server/files/list");
        return await LaunchGetRequest<GCodeFileRoot>(request, false);
    }

    public async Task<byte[]> GetImage(string query)
    {
        var request = new RestRequest($"/server/files/gcodes/{query}");
        var result = await _client.DownloadDataAsync(request);

        return result;
    }

    public async Task<GCodeFileDetailsRoot> GetFileDetails(string query)
    {
        var request = new RestRequest($"/server/files/metadata?filename={query}");
        return await LaunchGetRequest<GCodeFileDetailsRoot>(request, false);
    }

    public async Task<T> GetObject<T>(string query, bool filter)
    {
        var request = new RestRequest($"/printer/objects/query?{query}");

        return await LaunchGetRequest<T>(request, filter, query);
    }

    public async Task<SystemInfo> GetSystemInfo()
    {
        var request = new RestRequest("/machine/system_info");
        return await LaunchGetRequest<SystemInfo>(request, true, "system_info");
    }

    public async Task<SystemInfoStatus> GetSystemInfoStatus(string query)
    {
        var request = new RestRequest("/machine/system_info");
        return await LaunchGetRequest<SystemInfoStatus>(request, true, query);
    }

    private async Task<T> LaunchGetRequest<T>(RestRequest request, bool filter, string filterQuery = "")
    {
        var executeRequest = await _client.ExecuteAsync(request);
        var requestResult = executeRequest.Content;

        if (!executeRequest.IsSuccessful) return default;

        if (filter)
        {
            var jo = JObject.Parse(requestResult);
            requestResult = Filter(jo, filterQuery).ToString();
        }

        var deserializedClass = JsonSerializer.Deserialize<T>(requestResult);

        return deserializedClass;
    }

    private async Task<(bool, string)> LaunchPostRequest(RestRequest request, string command = "",
        bool logResponse = false)
    {
        Log.Add(("Client", "Information", string.IsNullOrEmpty(command) ? request.Resource : command));

        var result = await _client.ExecuteAsync(request);

        var response = string.Empty;

        if (!result.IsSuccessful)
        {
            if (result.StatusCode == HttpStatusCode.BadRequest)
            {
                var rx = new Regex(@"{(.*?)}");
                var responseClass = JsonSerializer.Deserialize<ErrorRoot>(result.Content);
                response = rx.Match(responseClass.error.traceback).Groups[1].Value;
                Log.Add(("Server", "Error", response));
            }
            else
            {
                Log.Add(("Server", "Error", $"Server Returned Code {result.StatusCode}"));
            }
        }

        if (logResponse) Log.Add(("Server", "Information", result.Content));


        return (result.IsSuccessful, response);
    }

    private static JToken? Filter(JObject jObject, string filter)
    {
        return jObject.Descendants()
            .Where(t => t.Type == JTokenType.Property && ((JProperty)t).Name == filter)
            .Select(p => ((JProperty)p).Value)
            .FirstOrDefault();
    }

    public async Task<PrinterInfoRoot> GetPrinterInfo()
    {
        var request = new RestRequest("/printer/info");
        return await LaunchGetRequest<PrinterInfoRoot>(request, false);
    }

    public async Task<PrinterStatusRoot> GetPrinterStatus()
    {
        var request = new RestRequest("/printer/objects/query?webhooks&virtual_sdcard&print_stats");
        return await LaunchGetRequest<PrinterStatusRoot>(request, false);
    }

    public async Task<(bool, string)> DeleteFile(string file)
    {
        var request = new RestRequest($"/server/files/gcodes/{file}", Method.Delete);
        return await LaunchPostRequest(request, $"DELETE {file}");
    }

    public async Task<(bool, string)> UploadFile(MultipartFormDataContent content, string fileName)
    {
        var request = new RestRequest("/server/files/upload", Method.Post);
        request.AddHeader("Content-Type", "multipart/form-data");
        request.AddFile("file", await content.ReadAsByteArrayAsync(), fileName);
        request.AlwaysMultipartFormData = true;
        return await LaunchPostRequest(request, $"UPLOAD {fileName}");
    }
}