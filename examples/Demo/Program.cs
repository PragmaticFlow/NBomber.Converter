using NBomber.Converter.HAR;
using NBomber.Converter.OpenApi;
using NBomber.Converter.Postman;

//var harFileContent = File.ReadAllText("HarExample_15steps.har");
//var harScenario = HARScenarioConverter.Convert(harFileContent);

//File.WriteAllText("HelloWorldHarScenario.cs", harScenario);

//var postmanCollectionContent = File.ReadAllText("PostmanExample_15steps_v2.0.json");
//var postmanScenario = PostmanScenarioConverter.Convert(postmanCollectionContent);

//File.WriteAllText("HelloWorldPostmanScenario.cs", postmanScenario);

var openApiSpecContent = File.ReadAllText("OpenApiExample_4steps.json");
var openApiScenario = OpenApiScenarioConverter.Convert(openApiSpecContent);

File.WriteAllText("HelloWorldOpenApiScenario.cs", openApiScenario);