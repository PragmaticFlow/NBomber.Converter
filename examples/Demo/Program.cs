using NBomber.Converter.HAR;
using NBomber.Converter.Postman;

var harFileContent = File.ReadAllText("HarExample_4steps.har");
var harScenario = HARScenarioConverter.Convert(harFileContent);

File.WriteAllText("HelloWorldHarScenario.cs", harScenario);

var postmanCollectionContent = File.ReadAllText("PostmanExample_4steps.json");
var postmanScenario = PostmanScenarioConverter.Convert(postmanCollectionContent);

File.WriteAllText("HelloWorldPostmanScenario.cs", postmanScenario);