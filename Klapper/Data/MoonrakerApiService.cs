using System.Text.Json;
using Klapper.Classes;
using Klapper.Shared.Components;
using Newtonsoft.Json.Linq;
using RestSharp;
using JsonSerializer = SpanJson.JsonSerializer;

namespace Klapper.Data;

public class MoonrakerApiService
{
    private readonly ILogger<MoonrakerApiService> _logger;
    private readonly RestClient _client;

    public MoonrakerApiService(ILogger<MoonrakerApiService> logger, string baseAddress)
    {
        _logger = logger;
        _client = new RestClient(baseAddress);
        _client.UseSerializer<SpanJsonSerializationAdapter>();
    }

    public async Task<MoonrakerObjectListClass> GetFullObjectList()
    {
        var request = new RestRequest("/printer/objects/list", Method.Get);
        var result = await _client.ExecuteAsync<MoonrakerObjectListClass>(request);
        return result.Data;
    }

    public async Task<bool> RunGCode(string query)
    {
        var request = new RestRequest($"/printer/gcode/script?script={query}", Method.Post); 
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
        var request = new RestRequest($"/server/files/list", Method.Get);
        var result = await _client.ExecuteAsync(request);

        var deserializedClass = JsonSerializer.Generic.Utf16.Deserialize<GCodeFileRoot>(result.Content);
        
        return deserializedClass;
    }

    public async Task<HeatableSensible> GetISensible(string query)
    {
        var request = new RestRequest($"/printer/objects/query?{query}", Method.Get);
        var result = await _client.ExecuteAsync(request);
        var jo = JObject.Parse(result.Content);
        var parsedJson = Filter(jo, query);
        
        var deserializedClass = JsonSerializer.Generic.Utf16.Deserialize<HeatableSensible>(parsedJson.ToString());

        deserializedClass.IsHeater = parsedJson.Any(x => x.Type == JTokenType.Property && ((JProperty)x).Name == "power");
        
        return deserializedClass;
    }
    
    public async Task<SystemInfo> GetSystemInfo()
    {
        var request = new RestRequest($"/machine/system_info", Method.Get);
        var result = await _client.ExecuteAsync(request);
        var jo = JObject.Parse(result.Content);
        var parsedJson = Filter(jo, "system_info");
        
        var deserializedClass = System.Text.Json.JsonSerializer.Deserialize<SystemInfo>(parsedJson.ToString());
        
        return deserializedClass;
    }
    
    public async Task<SystemInfoStatus> GetSystemInfoStatus(string query)
    {
        var request = new RestRequest($"/machine/system_info", Method.Get);
        var result = await _client.ExecuteAsync(request);
        var jo = JObject.Parse(result.Content);
        var parsedJson = Filter(jo, query);
        
        var deserializedClass = System.Text.Json.JsonSerializer.Deserialize<SystemInfoStatus>(parsedJson.ToString());
        
        return deserializedClass;
    }
    
    public async Task<MoonrakerQueryResultObject> GetQueryableObjects(string query)
    {
        var request = new RestRequest($"/printer/objects/query?{query}", Method.Get);
        var result = await _client.ExecuteAsync<MoonrakerQueryResultObject>(request);
        return result.Data;
    }

    private static JToken? Filter(JObject jObject, string filter)
    {
        return jObject.Descendants()
            .Where(t => t.Type == JTokenType.Property && ((JProperty)t).Name == filter)
            .Select(p => ((JProperty)p).Value)
            .FirstOrDefault();
    }
}