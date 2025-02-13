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
                    return ex.Message;
                }
            }
            else
            {
                return "Template parsing error";
            }

            return outputScenario;
        }

        public async void d()
        {
            var step1 = await Step.Run("action1", context, async () =>
            {
                var request = Http.CreateRequest("POST", "https://expensescalculator.azurewebsites.net/Items/Create?dayexpensesid=11")

                    .WithHeader("Accept", "*/*")

                    .WithHeader("Accept-Encoding", "gzip, deflate, br, zstd")

                    .WithHeader("Accept-Language", "en-US,en;q=0.9")

                    .WithHeader("Cache-Control", "no-cache")

                    .WithHeader("Connection", "keep-alive")

                    .WithHeader("Content-Length", "379")

                    .WithHeader("Content-Type", "application/x-www-form-urlencoded; charset=UTF-8")

                    .WithHeader("Host", "expensescalculator.azurewebsites.net")

                    .WithHeader("Origin", "https://expensescalculator.azurewebsites.net")

                    .WithHeader("Pragma", "no-cache")

                    .WithHeader("Referer", "https://expensescalculator.azurewebsites.net/DayExpenses/ShowChecks/11")

                    .WithHeader("Sec-Fetch-Dest", "empty")

                    .WithHeader("Sec-Fetch-Mode", "cors")

                    .WithHeader("Sec-Fetch-Site", "same-origin")

                    .WithHeader("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/131.0.0.0 Safari/537.36 OPR/116.0.0.0")

                    .WithHeader("X-Requested-With", "XMLHttpRequest")

                    .WithHeader("sec-ch-ua", "\"Opera\";v=\"116\", \"Chromium\";v=\"131\", \"Not_A Brand\";v=\"24\"")

                    .WithHeader("sec-ch-ua-mobile", "?0")

                    .WithHeader("sec-ch-ua-platform", "\"Windows\"")


                    .WithBody(new StringContent("Name=%D0%9A%D0%BE%D0%BA%D0%B0+%D0%BA%D0%BE%D0%BB%D0%B0&Description=2L&Price=100&Amount=1&UserList%5B%5D=P1&UserList%5B%5D=P2&UserList%5B%5D=P3&CheckId=30&__RequestVerificationToken=CfDJ8Hgki0zpS0hInH8CZzqj2GS8cBYc22df7OnGNxXJZ1ELLhrIRn4V37gcY6fVZlFLVdX8gkPbwTDfdKrT49JGFArLVNIoUo4rDVIhL5yPce0MqMJyYsz3MIw4FqjA-hiYTr1D7apHPd-cAIzmUyl8MHK2-lQoGUhYYH_KIuzDV4I9-CX1kBcl4VPQvcwuedarzA", Encoding.UTF8, "application/x-www-form-urlencoded; charset=UTF-8"))
                ;
            });
        }
    }
}
