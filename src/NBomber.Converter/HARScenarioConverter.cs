using Fluid;
using NBomber.Converter.Mappers;
using System.Reflection;
using System.Text.Json;

namespace NBomber.Converter
{
    public static class HARScenarioConverter
    {
        public static string Convert(string harFilePath)
        {
            HARFile harFile = GetHARObject(harFilePath);

            var assembly = Assembly.GetExecutingAssembly();
            string templateResourceName = "NBomber.Converter.HelloWorldScenarioTemplate.txt";
            string templateContent, outputScenario;

            using (Stream stream = assembly.GetManifestResourceStream(templateResourceName))
            {
                if (stream != null)
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        templateContent = reader.ReadToEnd();
                    }
                }
                else
                {
                    return "Scenario template not found!";
                }
            }

            var parser = new FluidParser();

            if (parser.TryParse(templateContent,
                                out IFluidTemplate template,
                                out string error))
            {
                TemplateOptions options = new TemplateOptions();
                options.MemberAccessStrategy = new UnsafeMemberAccessStrategy();
                var templateContext = new TemplateContext(
                                   new { model = harFile }, options, true);
                try
                {
                    outputScenario = template.Render(templateContext);
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

        private static HARFile GetHARObject(string harFilePath)
        {
            string harJson = File.ReadAllText(harFilePath);
            var har = JsonSerializer.Deserialize<HARFile>(harJson);

            for (int i = 0; i < har.Log.Entries.Count; i++)
                har.Log.Entries[i].Request = har.Log.Entries[i].Request.ToHARRequestWithActionName();

            return har;
        }
    }
}
