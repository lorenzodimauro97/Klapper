using Klapper.Classes;
using Klapper.Shared.Components;
using Newtonsoft.Json.Linq;
using RestSharp;
using SpanJson;

namespace Klapper.Data;

public class MoonrakerApiService
{
    private readonly RestClient _client;
    private readonly ILogger<MoonrakerApiService> _logger;

    public MoonrakerApiService(ILogger<MoonrakerApiService> logger, string baseAddress)
    {
        _logger = logger;
        _client = new RestClient(baseAddress);
        _client.UseSerializer<SpanJsonSerializationAdapter>();
    }

    /*public async Task<MoonrakerObjectListClass> GetFullObjectList()
    {
        var request = new RestRequest("/printer/objects/list");
        var result = await _client.ExecuteAsync<MoonrakerObjectListClass>(request);
        return result.Data;
    }*/

    public async Task<bool> RunGCode(string query)
    {
        var request = new RestRequest($"/printer/gcode/script?script={query}", Method.Post);
        var result = await _client.ExecuteAsync(request);
        return result.IsSuccessful;
    }
    
    public async Task<bool> PauseCancelResumePrint(int code)
    {
        var requestUrl = code switch
        {
            0 => "/printer/print/pause",
            1 => "/printer/print/cancel",
            2 => "/printer/print/resume",
            _ => string.Empty
        };

        var request = new RestRequest(requestUrl, Method.Post);
        var result = await _client.ExecuteAsync(request);
        return result.IsSuccessful;
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

    public async Task<HeatableSensible> GetISensible(string query)
    {
        var request = new RestRequest($"/printer/objects/query?{query}");

        return await LaunchGetRequest<HeatableSensible>(request, true, query);
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

    public async Task<MoonrakerQueryResultObject> GetQueryableObjects(string query)
    {
        var request = new RestRequest($"/printer/objects/query?{query}");
        return await LaunchGetRequest<MoonrakerQueryResultObject>(request, false);
    }

    private async Task<T> LaunchGetRequest<T>(RestRequest request, bool filter, string filterQuery = "")
    {
        var executeRequest = await _client.ExecuteAsync(request);
        var requestResult = executeRequest.Content;

        if (!executeRequest.IsSuccessful)
        {
            throw new BadHttpRequestException(executeRequest.StatusCode.ToString());
        }

        if (filter)
        {
            var jo = JObject.Parse(requestResult);
            requestResult = Filter(jo, filterQuery).ToString();
        }
        
        var deserializedClass = System.Text.Json.JsonSerializer.Deserialize<T>(requestResult);

        return deserializedClass;
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
}