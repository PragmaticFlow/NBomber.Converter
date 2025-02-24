using System.Diagnostics;

namespace NBomber.Converter.Tests;

public class HARCoverterTests
{
    [Fact]
    public void Build_HarExample_SameAsManualScenario()
    {
        // Arrange
        string manualScenario = File.ReadAllText(@"ManualScenario.cs");
        manualScenario = manualScenario.Replace(" ", String.Empty);
        string harPath = @"HarExample_4steps.har";

        // Act
        var generatedScenario = HARScenarioConverter.Convert(harPath);
        generatedScenario = generatedScenario.Replace(" ", String.Empty);

        // Assert
        Assert.Equal(manualScenario, generatedScenario);
    }

    [Fact]
    public async void Run_GeneratedScenario_RunSuccessfully()
    {
        // Arrange
        string harPath = "HarExample_4steps.har";
        var generatedScenario = HARScenarioConverter.Convert(harPath);

        if (Directory.Exists("reports"))
            Directory.Delete("reports", true);

        // Build scenario
        string scenarioNuGetPackages =
        "#r \"nuget: NBomber, 5.8.2\"\n" +
        "#r \"nuget: NBomber.Http, 5.2.1\"\n" +
        "#r \"nuget: System.Net.Http, 4.3.4\"\n";
        string entryPoint = "\nnew HelloWorldExample().Run();";
        generatedScenario = scenarioNuGetPackages + generatedScenario + entryPoint;

        // Write script file
        string scriptPath = "HelloWorldScenario.csx";
        FileWriter.WriteFile(scriptPath, generatedScenario);

        string executable = "powershell.exe";
        string arguments = $"dotnet script {scriptPath}"; // Roslyn check

        // Create the process
        Process process = new Process();
        process.StartInfo.FileName = executable;
        process.StartInfo.Arguments = arguments;

        // Act
        process.Start();
        await Task.Run(() => process.WaitForExit());

        // Get current report folder
        var reportFolderName = Directory.GetDirectories("reports").FirstOrDefault();

        // Get report row count (row with column names + row with global statistics + step number = 6)
        var csvFileName = Directory.GetFiles(reportFolderName, "*.csv").FirstOrDefault();
        int rowCount = File.ReadLines(csvFileName).Count();

        // Assert
        Assert.Equal(6, rowCount);
    }
}