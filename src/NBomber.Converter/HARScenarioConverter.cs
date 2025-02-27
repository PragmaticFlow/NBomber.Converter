using Fluid;
using NBomber.Converter.Models;
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

            if (har is null || har.Log.Entries == null)
                throw new FileFormatException("HAR file is corrupted.");

            for (int i = 0; i < har.Log.Entries.Count; i++)
                har.Log.Entries[i].Request = GetHARRequestWithActionName(har.Log.Entries[i].Request);

            return har;
        }

        private static HARRequestWithActionName GetHARRequestWithActionName(HARRequest harRequest)
        {
            var uri = new Uri(harRequest.Url);

            return new HARRequestWithActionName
            {
                Method = harRequest.Method,
                Url = harRequest.Url,
                HttpVersion = harRequest.HttpVersion,
                Headers = harRequest.Headers,
                QueryString = harRequest.QueryString,
                Cookies = harRequest.Cookies,
                BodySize = harRequest.BodySize,
                PostData = harRequest.PostData,
                ActionName = $"{harRequest.Method} {uri.Host}{uri.PathAndQuery}"
            };
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
            var templateContext = new TemplateContext(new { model = harFile }, options, true);
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
