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

        var template = GetScenarioTemplate();

        return RenderScenario(template, openApiSpec);
    }

    private static OpenApiSpec ParseOpenApiFile(string openApiSpecContent)
    {
        var openApiSpec = JsonSerializer.Deserialize<OpenApiSpec>(openApiSpecContent);

        if (openApiSpec is null || openApiSpec.Paths == null)
            throw new FileFormatException("Open API specification file is corrupted.");

        openApiSpec.Paths = openApiSpec.Paths.ToDictionary(
            kvp => kvp.Key,
            kvp => (PathItem) GetPathItemWithActionList(kvp.Value)
        );

        return openApiSpec;
    }

    private static PathItemWithMethodList GetPathItemWithActionList(PathItem pathItem)
    {
        var actions = new List<string>();
        Type type = pathItem.GetType();

        foreach(var prop in type.GetProperties())
        {
            var value = prop.GetValue(pathItem);

            if (value != null)
                actions.Add(prop.Name.ToUpper());
        }

        return new PathItemWithMethodList
        {
            Get = pathItem.Get,
            Post = pathItem.Post,
            Put = pathItem.Put,
            Delete = pathItem.Delete,
            Methods = actions
        };
    }

    private static IFluidTemplate GetScenarioTemplate()
    {
        var assembly = Assembly.GetExecutingAssembly();
        var templateResourceName = "NBomber.Converter.OpenApi.OpenApiScenarioTemplate.txt";

        using var stream = assembly.GetManifestResourceStream(templateResourceName);
        using var reader = new StreamReader(stream);
        var templateContent = reader.ReadToEnd();

        var parser = new FluidParser();
        return parser.Parse(templateContent);
    }

    private static string RenderScenario(IFluidTemplate template, OpenApiSpec openApiSpec)
    {
        var options = new TemplateOptions();
        options.MemberAccessStrategy = new UnsafeMemberAccessStrategy();

        var templateContext = new TemplateContext(new { model = openApiSpec }, options, true);
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