using Fluid;

namespace NBomber.Converter
{
    public static class ScenarioTemplateProvider
    {
        public static string GetHARScenarioTemplate(HARFile harFile)
        {
            string templateContent = "using NBomber.CSharp;\r\n\r\nnamespace Demo.HelloWorld;\r\n\r\npublic class ScenarioWithInit\r\n{\r\n    public void Run()\r\n    {\r\n        var scn1 = Scenario.Create(\"scenario_1\", async context =>\r\n        {\r\n            await Task.Delay(1000);\r\n            return Response.Ok();\r\n        })\r\n\r\n        NBomberRunner\r\n            .RegisterScenarios(scn1)\r\n            .Run();\r\n    }\r\n};";
            string output = String.Empty;

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
                    output = template.Render(ctx);
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

            return output;
        }
    }
}
