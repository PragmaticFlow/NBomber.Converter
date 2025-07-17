using NBomber.CSharp;
using NBomber.Http.CSharp;
using System.Text;
using System.Net.Http;

public class HelloWorldPostmanExample
{
    public void Run()
    {
        using var httpClient = Http.CreateDefaultClient();

        var scn1 = Scenario.Create("scenario_1", async context =>
        {
            var step1 = await Step.Run("step 1 - SEND SMALL IMAGE", context, async () =>
            {
                var request = Http.CreateRequest("PUT", "https://clients6.google.com/upload/drive/v2internal/batch?key=AIzaSyD_InbmSFufIEps5UAt2NmB_3LvBH3Sz_8")
                    .WithHeader("Key1", "Value1")
                    .WithHeader("Key2", "Value2");

                request.WithBody(new StringContent("", Encoding.UTF8, "multipart/form-data"));

                var response = await Http.Send(httpClient, request);

                return response;
            });

            var step2 = await Step.Run("step 2 - LIST USERS", context, async () =>
            {
                var request = Http.CreateRequest("GET", "https://reqres.in/api/users?page=2")
                    .WithHeader("Key1", "Value1")
                    .WithHeader("Key2", "Value2");

                var response = await Http.Send(httpClient, request);

                return response;
            });

            var step3 = await Step.Run("step 3 - CREATE RESOURCE", context, async () =>
            {
                var request = Http.CreateRequest("POST", "https://reqres.in/api/users")
                    .WithHeader("Key1", "Value1")
                    .WithHeader("Key2", "Value2");

                request.WithBody(new StringContent("""{    "name": "morpheus",    "job": "leader"}""", Encoding.UTF8, "application/json"));

                var response = await Http.Send(httpClient, request);

                return response;
            });

            var step4 = await Step.Run("step 4 - DELETE RESOURCE", context, async () =>
            {
                var request = Http.CreateRequest("DELETE", "https://reqres.in/api/users/2")
                    .WithHeader("Key1", "Value1")
                    .WithHeader("Key2", "Value2");

                var response = await Http.Send(httpClient, request);

                return response;
            });

            return Response.Ok();
        })
        .WithRestartIterationOnFail(shouldRestart: false);

        NBomberRunner
            .RegisterScenarios(scn1)
            .Run();
    }
}