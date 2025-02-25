using Fluid;
using NBomber.Converter.Mappers;
using System.Reflection;
using System.Text.Json;

namespace NBomber.Converter
{
    public static class HARScenarioConverter
    {
        public static string Convert(string harFileContent)
        {
            var harFile = ParseHARFile(harFileContent);

            var template = GetScenarioTemplate();

            return RenderScenario(template, harFile);
        }

        private static HARFile ParseHARFile(string harFileContent)
        {            
            var har = JsonSerializer.Deserialize<HARFile>(harFileContent);

            for (int i = 0; i < har.Log.Entries.Count; i++)
                har.Log.Entries[i].Request = har.Log.Entries[i].Request.ToHARRequestWithActionName();

            return har;
        }

        private static IFluidTemplate GetScenarioTemplate()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var templateResourceName = "NBomber.Converter.HelloWorldScenarioTemplate.txt";

            using var stream = assembly.GetManifestResourceStream(templateResourceName);
            using var reader = new StreamReader(stream);
            var templateContent = reader.ReadToEnd();

            var parser = new FluidParser();
            return parser.Parse(templateContent);
        }

        private static string RenderScenario(IFluidTemplate template, HARFile harFile)
        {
            TemplateOptions options = new TemplateOptions();
            options.MemberAccessStrategy = new UnsafeMemberAccessStrategy();
            var templateContext = new TemplateContext(
                               new { model = harFile }, options, true);
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
