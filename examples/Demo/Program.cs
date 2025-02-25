using NBomber.Converter;

var scenario = HARScenarioConverter.Convert("HarExample_15steps.har");
string savePath = "HelloWorldScenario.cs"; 
var status = FileWriter.WriteFile(savePath, scenario);
Console.WriteLine(status);