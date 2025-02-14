// See https://aka.ms/new-console-template for more information

using NBomber.Converter;

if (args.Length == 0)
{
    Console.WriteLine("HAR file location was not specified!");
    return;
}    

var har = HARConverter.GetHARObject(args[0]);
var scenarioTemplate = HARScenarioBuilder.Build(har);

string savePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "/HelloWorldScenario.cs"; 
var status = FileWriter.WriteFile(savePath, scenarioTemplate);

Console.WriteLine(status);