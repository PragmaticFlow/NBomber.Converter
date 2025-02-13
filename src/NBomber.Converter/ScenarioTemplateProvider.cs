using Fluid;

namespace NBomber.Converter
{
    public static class ScenarioTemplateProvider
    {
        public static string GetHARScenarioTemplate(HARFile harFile)
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
                    // handling of parser error
                }
            }
            else
            {
                // handling of the template parsing error
            }

            return outputScenario;
        }
    }
}
