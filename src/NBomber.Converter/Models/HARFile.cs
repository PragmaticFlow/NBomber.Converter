using Newtonsoft.Json;

public class HARFile
{
    [JsonProperty("log")]
    public HARLog Log { get; set; }
}

public class HARLog
{
    [JsonProperty("version")]
    public string Version { get; set; }

    [JsonProperty("creator")]
    public Creator Creator { get; set; }

    [JsonProperty("pages")]
    public List<HARPage> Pages { get; set; }

    [JsonProperty("entries")]
    public List<HAREntry> Entries { get; set; }
}

public class Creator
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("version")]
    public string Version { get; set; }
}

public class HARPage
{
    [JsonProperty("startedDateTime")]
    public DateTime StartedDateTime { get; set; }

    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("title")]
    public string Title { get; set; }

    [JsonProperty("pageTimings")]
    public PageTimings PageTimings { get; set; }
}

public class PageTimings
{
    [JsonProperty("onContentLoad")]
    public int OnContentLoad { get; set; }

    [JsonProperty("onLoad")]
    public int OnLoad { get; set; }
}

public class HAREntry
{
    [JsonProperty("startedDateTime")]
    public DateTime StartedDateTime { get; set; }

    [JsonProperty("time")]
    public int Time { get; set; }

    [JsonProperty("request")]
    public HARRequest Request { get; set; }

    [JsonProperty("response")]
    public HARResponse Response { get; set; }

    [JsonProperty("timings")]
    public HARTimings Timings { get; set; }
}

public class HARRequest
{
    [JsonProperty("method")]
    public string Method { get; set; }

    [JsonProperty("url")]
    public string Url { get; set; }

    [JsonProperty("httpVersion")]
    public string HttpVersion { get; set; }

    [JsonProperty("headers")]
    public List<Header> Headers { get; set; }

    [JsonProperty("queryString")]
    public List<QueryString> QueryString { get; set; }

    [JsonProperty("cookies")]
    public List<Cookie> Cookies { get; set; }

    [JsonProperty("bodySize")]
    public int BodySize { get; set; }
}

public class HARResponse
{
    [JsonProperty("status")]
    public int Status { get; set; }

    [JsonProperty("statusText")]
    public string StatusText { get; set; }

    [JsonProperty("headers")]
    public List<Header> Headers { get; set; }

    [JsonProperty("cookies")]
    public List<Cookie> Cookies { get; set; }

    [JsonProperty("content")]
    public HARContent Content { get; set; }

    [JsonProperty("redirectURL")]
    public string RedirectUrl { get; set; }

    [JsonProperty("bodySize")]
    public int BodySize { get; set; }
}

public class HARContent
{
    [JsonProperty("mimeType")]
    public string MimeType { get; set; }

    [JsonProperty("size")]
    public int Size { get; set; }
}

public class HARTimings
{
    [JsonProperty("blocked")]
    public int Blocked { get; set; }

    [JsonProperty("dns")]
    public int Dns { get; set; }

    [JsonProperty("connect")]
    public int Connect { get; set; }

    [JsonProperty("send")]
    public int Send { get; set; }

    [JsonProperty("wait")]
    public int Wait { get; set; }

    [JsonProperty("receive")]
    public int Receive { get; set; }

    [JsonProperty("ssl")]
    public int Ssl { get; set; }
}

public class Header
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }
}

public class QueryString
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }
}

public class Cookie
{
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }
}