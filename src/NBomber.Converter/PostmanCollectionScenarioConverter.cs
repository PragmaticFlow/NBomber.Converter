using Fluid;
using NBomber.Converter.Models;
using System.Reflection;
using System.Text.Json;

namespace NBomber.Converter
{
    public static class PostmanCollectionScenarioConverter
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

            return postmanCollection;
        }

        private static IFluidTemplate GetScenarioTemplate()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var templateResourceName = "NBomber.Converter.PostmanCollectionScenarioTemplate.txt";

            using var stream = assembly.GetManifestResourceStream(templateResourceName);
            using var reader = new StreamReader(stream);
            var templateContent = reader.ReadToEnd();

            var parser = new FluidParser();
            return parser.Parse(templateContent);
        }

        private static string RenderScenario(IFluidTemplate template, PostmanCollection postmanCollection)
        {
            TemplateOptions options = new TemplateOptions();
            options.MemberAccessStrategy = new UnsafeMemberAccessStrategy();
            var templateContext = new TemplateContext(new { model = postmanCollection }, options, true);
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
}
