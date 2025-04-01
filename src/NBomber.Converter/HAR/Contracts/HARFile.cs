using System.Text.Json.Serialization;

namespace NBomber.Converter.HAR.Contracts;

public class HARFile
{
    [JsonPropertyName("log")]
    public HARLog Log { get; set; }
}

public class HARLog
{
    [JsonPropertyName("version")]
    public string Version { get; set; }

    [JsonPropertyName("creator")]
    public Creator Creator { get; set; }

    [JsonPropertyName("pages")]
    public List<HARPage> Pages { get; set; }

    [JsonPropertyName("entries")]
    public List<HAREntry> Entries { get; set; }
}

public class Creator
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("version")]
    public string Version { get; set; }
}

public class HARPage
{
    [JsonPropertyName("startedDateTime")]
    public DateTime StartedDateTime { get; set; }

    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("title")]
    public string Title { get; set; }

    [JsonPropertyName("pageTimings")]
    public PageTimings PageTimings { get; set; }
}

public class PageTimings
{
    [JsonPropertyName("onContentLoad")]
    public double OnContentLoad { get; set; }

    [JsonPropertyName("onLoad")]
    public double OnLoad { get; set; }
}

public class HAREntry
{
    [JsonPropertyName("startedDateTime")]
    public DateTime StartedDateTime { get; set; }

    [JsonPropertyName("time")]
    public double Time { get; set; }

    [JsonPropertyName("request")]
    public HARRequest Request { get; set; }

    [JsonPropertyName("response")]
    public HARResponse Response { get; set; }

    [JsonPropertyName("timings")]
    public HARTimings Timings { get; set; }
}

public class HARRequest
{
    [JsonPropertyName("method")]
    public string Method { get; set; }

    [JsonPropertyName("url")]
    public string Url { get; set; }

    [JsonPropertyName("httpVersion")]
    public string HttpVersion { get; set; }

    [JsonPropertyName("headers")]
    public List<Header> Headers { get; set; }

    [JsonPropertyName("queryString")]
    public List<QueryString> QueryString { get; set; }

    [JsonPropertyName("cookies")]
    public List<Cookie> Cookies { get; set; }

    [JsonPropertyName("bodySize")]
    public int BodySize { get; set; }

    [JsonPropertyName("postData")]
    public PostData PostData { get; set; }
}

public class PostData
{
    [JsonPropertyName("mimeType")]
    public string MimeType { get; set; }

    [JsonPropertyName("text")]
    public string Text { get; set; }

    [JsonPropertyName("params")]
    public List<(string, string)> Params { get; set; }
}

public class HARResponse
{
    [JsonPropertyName("status")]
    public int Status { get; set; }

    [JsonPropertyName("statusText")]
    public string StatusText { get; set; }

    [JsonPropertyName("headers")]
    public List<Header> Headers { get; set; }

    [JsonPropertyName("cookies")]
    public List<Cookie> Cookies { get; set; }

    [JsonPropertyName("content")]
    public HARContent Content { get; set; }

    [JsonPropertyName("redirectURL")]
    public string RedirectUrl { get; set; }

    [JsonPropertyName("bodySize")]
    public int BodySize { get; set; }
}

public class HARContent
{
    [JsonPropertyName("mimeType")]
    public string MimeType { get; set; }

    [JsonPropertyName("size")]
    public int Size { get; set; }
}

public class HARTimings
{
    [JsonPropertyName("blocked")]
    public double Blocked { get; set; }

    [JsonPropertyName("dns")]
    public double Dns { get; set; }

    [JsonPropertyName("connect")]
    public double Connect { get; set; }

    [JsonPropertyName("send")]
    public double Send { get; set; }

    [JsonPropertyName("wait")]
    public double Wait { get; set; }

    [JsonPropertyName("receive")]
    public double Receive { get; set; }

    [JsonPropertyName("ssl")]
    public double Ssl { get; set; }
}

public class Header
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class QueryString
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}

public class Cookie
{
    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("value")]
    public string Value { get; set; }
}