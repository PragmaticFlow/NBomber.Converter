// See https://aka.ms/new-console-template for more information

using NBomber.Converter;

if (args.Length == 0)
{
    Console.WriteLine("HAR file location was not specified!");
    return;
}    

var har = HARDeserializer.GetHARObject(args[0]);

Console.WriteLine(har.Log.Creator.Name);