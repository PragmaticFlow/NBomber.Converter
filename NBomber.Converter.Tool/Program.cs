using CommandLine;
using NBomber.Converter.Tool;
using NBomber.Converter;

Parser.Default.ParseArguments<Options>(args)
    .WithParsed<Options>(o =>
    {
        var fileExtension = Path.GetExtension(o.InputFilePath).ToLower();
        var content = File.ReadAllText(o.InputFilePath);

        switch (fileExtension)
        {
            case ".har":
                Console.WriteLine($"Converting {o.InputFilePath}");
                var scenario = String.Empty;

                try
                {
                    scenario = HARScenarioConverter.Convert(content);
                    File.WriteAllText(o.OutputFilePath, scenario);
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
