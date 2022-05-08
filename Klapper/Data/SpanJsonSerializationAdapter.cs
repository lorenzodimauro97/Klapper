using RestSharp;
using RestSharp.Serializers;
using SpanJson;

namespace Klapper.Data;

public class SpanJsonSerializationAdapter : IRestSerializer, ISerializer, IDeserializer
{
    public T? Deserialize<T>(RestResponse response)
    {
        if (response.Content == null)
            throw new DeserializationException(response, new InvalidOperationException("Response content is null"));

        return JsonSerializer.Generic.Utf16.Deserialize<T>(response.Content);
    }

    public string? Serialize(Parameter bodyParameter)
    {
        return Serialize(bodyParameter.Value);
    }

    public ISerializer Serializer => this;
    public IDeserializer Deserializer => this;

    public string[] AcceptedContentTypes => RestSharp.Serializers.ContentType.JsonAccept;

    public SupportsContentType SupportsContentType => contentType => contentType.Contains("json");

    public DataFormat DataFormat => DataFormat.Json;

    public string? Serialize(object? obj)
    {
        return obj == null ? null : JsonSerializer.Generic.Utf16.Serialize(obj);
    }

    public string ContentType { get; set; } = "text/json";
}