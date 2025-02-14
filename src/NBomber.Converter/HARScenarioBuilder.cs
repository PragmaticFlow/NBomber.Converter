using Fluid;
using System.Text.Json;

namespace NBomber.Converter
{
    public class HARScenarioBuilder
    {
        // Test HTTP requests put, delete
        private readonly HARFile harFile;

        public HARScenarioBuilder(string harFilePath)
        {
            harFile = GetHARObject(harFilePath);
        }

        public string Build()
        {
            string templateLocation = @"ScenarioTemplates/HelloWorldScenarioTemplate.txt";
            string templateContent = File.ReadAllText(templateLocation);
            string outputScenario = String.Empty;

            var parser = new FluidParser();

            if (parser.TryParse(templateContent,
                                out IFluidTemplate template,
                                out string error))
            {
                TemplateOptions options = new TemplateOptions();
                options.MemberAccessStrategy = new UnsafeMemberAccessStrategy();
                var ctx = new TemplateContext(
                                   new { model = harFile }, options, true);
                try
                {
                    outputScenario = template.Render(ctx);
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
            else
            {
                return "Template parsing error";
            }

            return outputScenario;
        }

        private HARFile GetHARObject(string harFilePath)
        {
            string harJson = File.ReadAllText(harFilePath);

            return JsonSerializer.Deserialize<HARFile>(harJson);
        }
    }
}
