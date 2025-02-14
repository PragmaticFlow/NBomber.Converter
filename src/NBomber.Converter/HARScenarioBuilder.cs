using Fluid;

namespace NBomber.Converter
{
    public static class HARScenarioBuilder
    {
        // Move deserialization here
        // Test HTTP requests put, delete
        // Remove Newtonsoft.JSON
        public static string Build(HARFile harFile)
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
    }
}
