using CommandLine;
using NBomber.Converter.Tool;
using NBomber.Converter;

Parser.Default.ParseArguments<Options>(args)
    .WithParsed<Options>(o =>
    {
        var converterType = Path.GetExtension(o.InputFilePath).ToLower();
        var content = File.ReadAllText(o.InputFilePath);

        switch (converterType)
        {
            case ".har":
                var scenario = HARScenarioConverter.Convert(content);
                File.WriteAllText(o.OutputFilePath, scenario);
                break;
            default:
                Console.WriteLine("Unknown file type");
                break;
        }
    });
