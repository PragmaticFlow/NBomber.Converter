namespace NBomber.Converter
{
    public static class FileWriter
    {
        public static string WriteFile(string path, string content)
        {
            try
            {
                File.WriteAllText(path, content);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return "File written successfully.";
        }
    }
}
