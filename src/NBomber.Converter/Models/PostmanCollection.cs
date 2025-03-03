using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace NBomber.Converter.Models
{
    public class PostmanCollection
    {
        [JsonPropertyName("info")]
        public Info Info { get; set; }

        [JsonPropertyName("item")]
        public List<Item> Items { get; set; }
    }

    public class Info
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("schema")]
        public string Schema { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }
    }

    public class Item
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("request")]
        public Request Request { get; set; }
        
        [JsonPropertyName("response")]
        public List<Response> Responses { get; set; } // Response object
    }

    public class Request
    {
        [JsonPropertyName("method")]
        public string Method { get; set; }

        [JsonPropertyName("header")]
        public List<Header> Headers { get; set; }

        [JsonPropertyName("url")]
        public JsonNode Url { get; set; }

        [JsonPropertyName("body")]
        public Body Body { get; set; }
    }

    public class Url
    {
        [JsonPropertyName("raw")]
        public string Raw { get; set; }

        [JsonPropertyName("protocol")]
        public string Protocol { get; set; }

        [JsonPropertyName("host")]
        public List<string> Host { get; set; }

        [JsonPropertyName("path")]
        public List<string> Path { get; set; }

        [JsonPropertyName("query")]
        public (string, string) Query { get; set; }
    }

    public class Body
    {
        [JsonPropertyName("mode")]
        public string Mode { get; set; }

        [JsonPropertyName("raw")]
        public string Raw { get; set; }

        [JsonPropertyName("options")]
        public Options Options { get; set; }
    }

    public class Options
    {
        [JsonPropertyName("raw")]
        public Raw Raw { get; set; }
    }

    public class Raw
    {
        [JsonPropertyName("language")]
        public string Language { get; set; }
    }

    // Response object
    public class Response
    {
        [JsonPropertyName("status")]
        public string Status { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }

        [JsonPropertyName("headers")]
        public List<Header> Headers { get; set; }
    }

    public class Header
    {
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("value")]
        public string Value { get; set; }
    }
}
