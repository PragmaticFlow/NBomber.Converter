using NBomber.Converter;

var harFileContent = File.ReadAllText("HarExample_15steps.har");
var scenario = HARScenarioConverter.Convert(harFileContent);

File.WriteAllText("HelloWorldScenario.cs", scenario);