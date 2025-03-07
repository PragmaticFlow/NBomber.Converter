using Fluid;
using NBomber.Converter.Models;
using NBomber.Converter.PostmanScenarioConverter.Models;
using System.Collections.Generic;
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace NBomber.Converter.PostmanScenarioConverter
{
    public static class PostmanScenarioConverter
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

            for (int i = 0; i < postmanCollection.Items.Count; i++)
                postmanCollection.Items[i].Request = GetRequestWithPlainUrl(postmanCollection.Items[i].Request);             

            return postmanCollection;
        } 

        private static PostmanRequestWithPlainUrl GetRequestWithPlainUrl(Request postmanRequest)
        {
            string plainUrl = string.Empty;

            if (postmanRequest.Url is JsonObject urlJsonObject)
            {
                if (urlJsonObject.ContainsKey("raw"))
                    plainUrl = urlJsonObject["raw"].ToString();
                else
                    throw new FileFormatException("Postman collection file is corrupted.");
            }
            else if (postmanRequest.Url is JsonValue)
            {
                plainUrl = postmanRequest.Url.ToString();
            }
            else
                throw new FileFormatException("Postman collection file is corrupted.");

            var contentType = postmanRequest.Body == null ? String.Empty : GetContentType(postmanRequest.Body);

            return new PostmanRequestWithPlainUrl
            {
                Method = postmanRequest.Method,
                Headers = postmanRequest.Headers,
                Url = postmanRequest.Url,
                Body = postmanRequest.Body,
                PlainUrl = plainUrl,
                ContentType = contentType
            };
        }

        private static string GetContentType(Body requestBody)
        {
            if (requestBody.Mode == "raw") return "application/json";
            else if (requestBody.Mode == "formdata") return "multipart/form-data";
            else if (requestBody.Mode == "urlencoded") return "application/x-www-form-urlencoded";
            else if (requestBody.Mode == "file") return "application/octet-stream"; // only binary files
            else if (requestBody.Mode == "graphql") return "application/graphql\r\n";
            else return requestBody.Mode;
        }

        private static IFluidTemplate GetScenarioTemplate()
        {
            var assembly = Assembly.GetExecutingAssembly();
            var templateResourceName = "NBomber.Converter.PostmanScenarioConverter.PostmanScenarioTemplate.txt";

            using var stream = assembly.GetManifestResourceStream(templateResourceName);
            using var reader = new StreamReader(stream);
            var templateContent = reader.ReadToEnd();

            var parser = new FluidParser();
            return parser.Parse(templateContent);
        }

        private static string RenderScenario(IFluidTemplate template, PostmanCollection postmanCollection)
        {
            var options = new TemplateOptions();
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
