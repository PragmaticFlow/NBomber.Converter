// See https://aka.ms/new-console-template for more information

using NBomber.Converter;

if (args.Length == 0)
{
    Console.WriteLine("HAR file location was not specified!");
    return;
}    

var har = HARConverter.GetHARObject(args[0]);
var scenarioTemplate = ScenarioTemplateProvider.GetHARScenarioTemplate(har);
var status = FileWriter.WriteFile("./HelloWorldScenario.cs", scenarioTemplate);

Console.WriteLine(status);