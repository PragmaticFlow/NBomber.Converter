using CommandLine;

namespace NBomber.Converter.Tool;

public enum InputFileType
{
    HAR,
    PostmanCollection
}

class CliArgs
{
    [Value(0, MetaName = "input", HelpText = "Input file path", Required = true)]
    public string InputFilePath { get; set; }

    [Option('t', "file-type", Required = false, HelpText = "Input file type")]
    public InputFileType? InputFileType { get; set; }

    [Option('o', "output", Required = true, HelpText = "Location of the output file")]
    public string OutputFilePath { get; set; }
}