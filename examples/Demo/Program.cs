using NBomber.Converter.HAR;
using NBomber.Converter.Postman;

var harFileContent = File.ReadAllText("HarExample_15steps.har");
var harScenario = HARScenarioConverter.Convert(harFileContent);

File.WriteAllText("HelloWorldHarScenario.cs", harScenario);

var postmanCollectionContent = File.ReadAllText("PostmanExample_15steps_v2.0.json");
var postmanScenario = PostmanScenarioConverter.Convert(postmanCollectionContent);

File.WriteAllText("HelloWorldPostmanScenario.cs", postmanScenario);