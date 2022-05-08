using RestSharp;
using RestSharp.Serializers;
using SpanJson;

namespace Klapper.Data;

public class SpanJsonSerializationAdapter : IRestSerializer, ISerializer, IDeserializer {
    public string? Serialize(object? obj)
    {
        return obj == null ? null : JsonSerializer.Generic.Utf16.Serialize(obj);
    }

    public string? Serialize(Parameter bodyParameter) => Serialize(bodyParameter.Value);

    public T? Deserialize<T>(RestResponse response) {
        if (response.Content == null)
            throw new DeserializationException(response, new InvalidOperationException("Response content is null"));

        return JsonSerializer.Generic.Utf16.Deserialize<T>(response.Content);
    }

    public ISerializer   Serializer   => this;
    public IDeserializer Deserializer => this;

    public string[] AcceptedContentTypes => RestSharp.Serializers.ContentType.JsonAccept;

    public string ContentType { get; set; } = "text/json";

    public SupportsContentType SupportsContentType => contentType => contentType.Contains("json");

    public DataFormat DataFormat => DataFormat.Json;
}