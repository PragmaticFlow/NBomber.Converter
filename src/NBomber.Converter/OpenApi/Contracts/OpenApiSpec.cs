using NBomber.Converter.Postman.Contracts;
using System.Text.Json.Serialization;

namespace NBomber.Converter.OpenApi.Contracts;

public class OpenApiSpec
{
    [JsonPropertyName("openapi")]
    public string OpenApi { get; set; }

    [JsonPropertyName("info")]
    public Info Info { get; set; }

    [JsonPropertyName("servers")]
    public List<Server> Servers { get; set; }

    [JsonPropertyName("paths")]
    public Dictionary<string, PathItem> Paths { get; set; }

    [JsonPropertyName("components")]
    public object Components { get; set; }
}

public class Info
{
    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }

    [JsonPropertyName("version")]
    public string Version { get; set; }
}

public class Server
{
    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("description")]
    public string Description { get; set; }
}

public class PathItem
{
    [JsonPropertyName("get")]
    public Operation? Get { get; set; }

    [JsonPropertyName("post")]
    public Operation? Post { get; set; }

    [JsonPropertyName("put")]
    public Operation? Put { get; set; }

    [JsonPropertyName("delete")]
    public Operation? Delete { get; set; }
}

public class Operation
{
    [JsonPropertyName("summary")]
    public string Summary { get; set; }

    [JsonPropertyName("operationId")]
    public string OperationId { get; set; }

    [JsonPropertyName("responses")]
    public Dictionary<string, Response> Responses { get; set; }
}
