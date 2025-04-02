using NBomber.Converter.Contracts;

namespace NBomber.Converter.Tests;

[CollectionDefinition("NonParallelTests", DisableParallelization = true)]
public class PostmanConverterTests
{
    [Fact]
    public void Convert_Should_Convert_PostmanCollection_To_NBomberScenario()
    {
        // Arrange
        var scenarioForComparison = File.ReadAllText(@"Resources/GeneratedPostmanScenarioForComparison.cs");
        scenarioForComparison = ConverterTestHelper.RemoveSpacesAndBackslashSymbols(scenarioForComparison);
        var postmanCollectionContent = File.ReadAllText(@"Resources/PostmanExample_4steps.json");

        // Act
        var generatedScenario = Postman.PostmanScenarioConverter.Convert(postmanCollectionContent);
        generatedScenario = ConverterTestHelper.RemoveSpacesAndBackslashSymbols(generatedScenario);

        // Assert
        Assert.Equal(scenarioForComparison, generatedScenario);
    }

    [Fact]
    public void Convert_Corrupted_PostmanCollection_Should_Throw_NullReferenceException()
    {
        // Arrange
        var postmanCollectionContent = File.ReadAllText(@"Resources/CorruptedPostman_4steps.json");

        // Act
        Action act = () => Postman.PostmanScenarioConverter.Convert(postmanCollectionContent);

        // Assert
        Assert.Throws<FileFormatException>(act);
    }

    [Fact]
    public void EndToEnd()
    {
        // Arrange
        var postmanCollectionContent = File.ReadAllText(@"Resources/PostmanExample_4steps.json");
        var generatedScenario = Postman.PostmanScenarioConverter.Convert(postmanCollectionContent);

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

        var assemblies = new string[]
        {
            "NBomber.dll",
            "NBomber.Contracts.dll",
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