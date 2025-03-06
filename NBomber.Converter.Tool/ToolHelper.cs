namespace NBomber.Converter.Tool
{
    public static class ToolHelper
    {
        public static InputFileType DetermineFileType(string filePath)
        {
            var fileExtension = Path.GetExtension(filePath).ToLower();

            if (fileExtension == ".json")
            {
                Console.WriteLine("Automatically detected file type - Postman collection");
                return InputFileType.PostmanCollection;
            }
            else
            {
                Console.WriteLine("Automatically detected file type - HAR");
                return InputFileType.HAR;
            }
        }
    }
}
