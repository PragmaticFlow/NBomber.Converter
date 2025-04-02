using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;
using Fluid;
using NBomber.Converter.Contracts;
using NBomber.Converter.Postman.Contracts;

namespace NBomber.Converter.Postman;

public static class PostmanScenarioConverter
{
    public static string Convert(string postmanCollectionContent)
    {
        var postmanCollection = ParseCollection(postmanCollectionContent);

        var template = GetScenarioTemplate();

        return RenderScenario(template, postmanCollection);
    }

    private static PostmanCollection ParseCollection(string collectionContent)
    {
        var postmanCollection = JsonSerializer.Deserialize<PostmanCollection>(collectionContent);

        if (postmanCollection == null || postmanCollection.Items == null)
            throw new FileFormatException("Postman collection file is corrupted.");

        foreach (var item in postmanCollection.Items)
            item.Request = GetRequestWithPlainUrl(item.Request);

        return postmanCollection;
    } 

    private static PostmanRequestWithPlainUrl GetRequestWithPlainUrl(Request postmanRequest)
    {
        var plainUrl = string.Empty;

        if (postmanRequest.Url is JsonObject urlJsonObject)
        {
            if (urlJsonObject.ContainsKey("raw"))
                plainUrl = urlJsonObject["raw"].ToString();
            else
                throw new FileFormatException("Postman collection file is corrupted.");
        }
        else if (postmanRequest.Url is JsonValue)
        {
            plainUrl = postmanRequest.Url.ToString();
        }
        else
            throw new FileFormatException("Postman collection file is corrupted.");

        var contentType = postmanRequest.Body == null ? String.Empty : GetContentType(postmanRequest.Body);

        return new PostmanRequestWithPlainUrl
        {
            Method = postmanRequest.Method,
            Headers = postmanRequest.Headers,
            Url = postmanRequest.Url,
            Body = postmanRequest.Body,
            PlainUrl = plainUrl,
            ContentType = contentType
        };
    }

    private static string GetContentType(Body requestBody)
    {
        return requestBody.Mode switch
        {
            "raw" => "application/json",
            "formdata" => "multipart/form-data",
            "urlencoded" => "application/x-www-form-urlencoded",
            "file" => "application/octet-stream",
            "graphql" => "application/graphql\r\n",
            _ => requestBody.Mode
        };
    }

    private static IFluidTemplate GetScenarioTemplate()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var templateResourceName = "NBomber.Converter.Postman.PostmanScenarioTemplate.txt";

        using var stream = assembly.GetManifestResourceStream(templateResourceName);
        using var reader = new StreamReader(stream);
        var templateContent = reader.ReadToEnd();

        var parser = new FluidParser();
        return parser.Parse(templateContent);
    }

    private static string RenderScenario(IFluidTemplate template, PostmanCollection postmanCollection)
    {
        var options = new TemplateOptions
        {
            MemberAccessStrategy = new UnsafeMemberAccessStrategy()
        };
        var templateContext = new TemplateContext(new { model = postmanCollection }, options, allowModelMembers: true);
        
        try
        {
            return template.Render(templateContext);
        }
        catch (Exception ex)
        {
            return ex.Message;
        }
    }
}