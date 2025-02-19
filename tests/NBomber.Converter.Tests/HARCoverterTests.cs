namespace NBomber.Converter.Tests;

public class HARCoverterTests
{
    [Fact]
    public void Build_HarExample_SameAsManualScenario()
    {
        // Arrange
        string manualScenario = File.ReadAllText(@"ManualScenario.cs");
        string harPath = @"HarExample.har";

        // Act
        var generatedScenario = new HARScenarioBuilder(harPath).Build();

        // Assert
        Assert.Equal(manualScenario, generatedScenario);
    }
}