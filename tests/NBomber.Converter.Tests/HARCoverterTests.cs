using Microsoft.CodeAnalysis;

namespace NBomber.Converter.Tests;

public class HARCoverterTests
{
    [Fact]
    public void Convert_Should_Convert_HARFile_To_NBomberScenario()
    {
        // Arrange
        string scenarioForComparison = File.ReadAllText(@"Resources/GeneratedScenarioForComparison.cs");
        scenarioForComparison = ConverterTestHelper.RemoveSpacesAndBackslashSymbols(scenarioForComparison);
        string harPath = @"HarExample_4steps.har";

        // Act
        var generatedScenario = HARScenarioConverter.Convert(harPath);
        generatedScenario = ConverterTestHelper.RemoveSpacesAndBackslashSymbols(generatedScenario);

        // Assert
        Assert.Equal(scenarioForComparison, generatedScenario);
    }


    [Fact]
    public void EndToEnd()
    {
        // Arrange
        string harPath = "HarExample_4steps.har";
        var generatedScenario = HARScenarioConverter.Convert(harPath);

        ConverterTestHelper.DeleteReportsIfExists();        

        string invokePart = @"
        
        public class Program
        {
            public static void Main()
            {
                new HelloWorldExample().Run();
            }
        }";

        string code = generatedScenario + invokePart;

        var assemblies = new string[]
        {
            "NBomber.dll",
            "NBomber.Http.dll"
        };

        // Act
        ConverterTestHelper.RunCSharpCode(code, assemblies);        

        // Get current report folder
        var reportFolderName = Directory.GetDirectories("reports").FirstOrDefault();

        // Get report row count (row with column names + row with global statistics + step number = 6)
        var csvFileName = Directory.GetFiles(reportFolderName, "*.csv").FirstOrDefault();
        int rowCount = File.ReadLines(csvFileName).Count();

        // Assert
        Assert.Equal(6, rowCount);
    }
}