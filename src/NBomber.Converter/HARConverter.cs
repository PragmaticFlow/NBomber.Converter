namespace NBomber.Converter;

using Newtonsoft.Json;

public static class HARConverter
{
    public static HARFile GetHARObject(string harFilePath)
    {
        string harJson = File.ReadAllText(harFilePath);

        return JsonConvert.DeserializeObject<HARFile>(harJson);
    }
}