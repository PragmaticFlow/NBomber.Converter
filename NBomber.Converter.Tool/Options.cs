using CommandLine;

namespace NBomber.Converter.Tool
{
    class Options
    {
        [Value(0, MetaName = "input", HelpText = "Input file path", Required = true)]
        public string InputFilePath { get; set; }

        [Option('o', "output", Required = true, HelpText = "Location of the output file")]
        public string OutputFilePath { get; set; }
    }
}
