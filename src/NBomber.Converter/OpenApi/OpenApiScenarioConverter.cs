using System.Reflection;
using System.Text.Json;
using Fluid;
using NBomber.Converter.Contracts;
using NBomber.Converter.OpenApi.Contracts;

namespace NBomber.Converter.OpenApi;

public static class OpenApiScenarioConverter
{
    public static string Convert(string openApiSpecContent)
    {
        var openApiSpec = ParseOpenApiFile(openApiSpecContent);

        //var template = GetScenarioTemplate();

        //return RenderScenario(template, openApiSpec);

        return "";
    }

    private static OpenApiSpec ParseOpenApiFile(string openApiSpecContent)
    {
        var openApiSpec = JsonSerializer.Deserialize<OpenApiSpec>(openApiSpecContent);

        //if (har is null || har.Log.Entries == null)
        //    throw new FileFormatException("HAR file is corrupted.");

        //foreach (var item in har.Log.Entries)
        //    item.Request = GetHARRequestWithActionName(item.Request);

        return openApiSpec;
    }

    //private static HARRequestWithActionName GetHARRequestWithActionName(HARRequest harRequest)
    //{
    //    var uri = new Uri(harRequest.Url);

    //    return new HARRequestWithActionName
    //    {
    //        Method = harRequest.Method,
    //        Url = harRequest.Url,
    //        HttpVersion = harRequest.HttpVersion,
    //        Headers = harRequest.Headers,
    //        QueryString = harRequest.QueryString,
    //        Cookies = harRequest.Cookies,
    //        BodySize = harRequest.BodySize,
    //        PostData = harRequest.PostData,
    //        ActionName = $"{harRequest.Method} {uri.Host}{uri.PathAndQuery}"
    //    };
    //}

    //private static IFluidTemplate GetScenarioTemplate()
    //{
    //    var assembly = Assembly.GetExecutingAssembly();
    //    var templateResourceName = "NBomber.Converter.HAR.HarScenarioTemplate.txt";

    //    using var stream = assembly.GetManifestResourceStream(templateResourceName);
    //    using var reader = new StreamReader(stream);
    //    var templateContent = reader.ReadToEnd();

    //    var parser = new FluidParser();
    //    return parser.Parse(templateContent);
    //}

    //private static string RenderScenario(IFluidTemplate template, HARFile harFile)
    //{
    //    var options = new TemplateOptions();
    //    options.MemberAccessStrategy = new UnsafeMemberAccessStrategy();
    //    var templateContext = new TemplateContext(new { model = harFile }, options, true);
    //    try
    //    {
    //        return template.Render(templateContext);
    //    }
    //    catch (Exception ex)
    //    {
    //        return ex.Message;
    //    }
    //}
}