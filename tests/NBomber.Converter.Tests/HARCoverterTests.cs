using NBomber.Converter.Contracts;
using NBomber.Converter.HAR;

namespace NBomber.Converter.Tests;

[CollectionDefinition("NonParallelTests", DisableParallelization = true)]
public class HARCoverterTests
{
    [Fact]
    public void Convert_Should_Convert_HARFile_To_NBomberScenario()
    {
        // Arrange
        var scenarioForComparison = File.ReadAllText(@"Resources/GeneratedHarScenarioForComparison.cs");
        scenarioForComparison = ConverterTestHelper.RemoveSpacesAndBackslashSymbols(scenarioForComparison);
        var harFileContent = File.ReadAllText(@"Resources/HarExample_4steps.har");

        // Act
        var generatedScenario = HARScenarioConverter.Convert(harFileContent);
        generatedScenario = ConverterTestHelper.RemoveSpacesAndBackslashSymbols(generatedScenario);

        // Assert
        Assert.Equal(scenarioForComparison, generatedScenario);
    }

    [Fact]
    public void Convert_Corrupted_HAR_Should_Throw_NullReferenceException()
    {
        // Arrange
        var harFileContent = File.ReadAllText(@"Resources/CorruptedHar_4steps.har");

        // Act
        Action act = () => HARScenarioConverter.Convert(harFileContent);

        // Assert
        Assert.Throws<FileFormatException>(act);
    }

    [Fact]
    public void EndToEnd()
    {
        // Arrange
        var harFileContent = File.ReadAllText(@"Resources/HarExample_4steps.har");
        var generatedScenario = HARScenarioConverter.Convert(harFileContent);

        ConverterTestHelper.DeleteReportsIfExists();        

        var invokePart = @"
        
        public class Program
        {
            public static void Main()
            {
                new HelloWorldExample().Run();
            }
        }";

        var code = generatedScenario + invokePart;

        var assemblies = new[]
        {
            "NBomber.dll",
            // "NBomber.Contracts.dll",
            "NBomber.Http.dll"
        };

        // Act
        ConverterTestHelper.RunCSharpCode(code, assemblies);        

        // Get current report folder
        var reportFolderName = Directory.GetDirectories("reports").FirstOrDefault();

        // Get report row count (row with column names + row with global statistics + step number = 6)
        var csvFileName = Directory.GetFiles(reportFolderName, "*.csv").FirstOrDefault();
        var rowCount = File.ReadLines(csvFileName).Count();

        // Assert
        Assert.Equal(6, rowCount);
    }
}