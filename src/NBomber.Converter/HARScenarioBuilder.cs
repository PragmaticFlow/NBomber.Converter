using Fluid;
using NBomber.Converter.Mappers;
using System.Text.Json;

namespace NBomber.Converter
{
    public class HARScenarioBuilder
    {
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
            var har = JsonSerializer.Deserialize<HARFile>(harJson);

            for (int i = 0; i < har.Log.Entries.Count; i++)
                har.Log.Entries[i].Request = har.Log.Entries[i].Request.ToHARRequestWithActionName();

            return har;
        }
    }
}
