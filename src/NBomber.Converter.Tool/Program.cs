using CommandLine;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using NBomber.Converter.HAR;
using NBomber.Converter.Postman;
using NBomber.Converter.Tool;

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console(
        outputTemplate: "{Timestamp:HH:mm:ss} [{Level:u3}] {Message:lj}{NewLine}{Exception}",
        theme: AnsiConsoleTheme.Sixteen
    )
    .CreateLogger();

var version = typeof(HARScenarioConverter).Assembly.GetName().Version;
Log.Information("{0} version: {1}.{2}.{3}", "NBomber Converter", version.Major, version.Minor, version.Build);

Parser.Default.ParseArguments<CliArgs>(args)
    .WithParsed<CliArgs>(o =>
    {
        var content = File.ReadAllText(o.InputFilePath);
        var fileType = o.InputFileType.HasValue ? o.InputFileType : DetermineFileType(o.InputFilePath);

        switch (fileType)
        {
            case InputFileType.HAR:
                Log.Information($"Converting {o.InputFilePath}");

                try
                {
                    var harScenario = HARScenarioConverter.Convert(content);
                    File.WriteAllText(o.OutputFilePath, harScenario);
                    Log.Information($"Wrote NBomber scenario to {o.OutputFilePath}");
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }               
                break;
            
            case InputFileType.PostmanCollection:
                Log.Information($"Converting {o.InputFilePath}");

                try
                {
                    var postmanScenario = PostmanScenarioConverter.Convert(content);
                    File.WriteAllText(o.OutputFilePath, postmanScenario);
                    Log.Information($"Wrote NBomber scenario to {o.OutputFilePath}");
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }
                break;
            
            default:
                Log.Error("Unknown file type");
                break;
        }
    });

Log.CloseAndFlush();

static InputFileType DetermineFileType(string filePath)
{
    var fileExtension = Path.GetExtension(filePath).ToLower();

    if (fileExtension == ".json")
    {
        Log.Information("Automatically detected file type - Postman collection");
        return InputFileType.PostmanCollection;
    }
    else
    {
        Log.Information("Automatically detected file type - HAR");
        return InputFileType.HAR;
    }
}