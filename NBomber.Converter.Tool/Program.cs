using CommandLine;
using NBomber.Converter.Tool;
using NBomber.Converter.HARScenarioConverter;
using NBomber.Converter.PostmanScenarioConverter;
using System.Reflection;

var assembly = Assembly.GetExecutingAssembly();
Console.WriteLine($"NBomber Converter toolversion: {assembly.GetName().Version}");

Parser.Default.ParseArguments<Options>(args)
    .WithParsed<Options>(o =>
    {
        var content = File.ReadAllText(o.InputFilePath);
        var fileType = o.InputFileType.HasValue ? 
            o.InputFileType : ToolHelper.DetermineFileType(o.InputFilePath);

        switch (fileType)
        {
            case InputFileType.HAR:
                Console.WriteLine($"Converting {o.InputFilePath}");

                try
                {
                    var harScenario = HARScenarioConverter.Convert(content);
                    File.WriteAllText(o.OutputFilePath, harScenario);
                    Console.WriteLine($"Wrote NBomber scenario to {o.OutputFilePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }               
                break;
            case InputFileType.PostmanCollection:
                Console.WriteLine($"Converting {o.InputFilePath}");

                try
                {
                    var postmanScenario = PostmanScenarioConverter.Convert(content);
                    File.WriteAllText(o.OutputFilePath, postmanScenario);
                    Console.WriteLine($"Wrote NBomber scenario to {o.OutputFilePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                break;
            default:
                Console.WriteLine("Unknown file type");
                break;
        }
    });
