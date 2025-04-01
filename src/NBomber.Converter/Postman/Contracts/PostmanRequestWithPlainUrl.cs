namespace NBomber.Converter.Postman.Contracts;

class PostmanRequestWithPlainUrl : Request
{
    public string PlainUrl { get; set; }
    public string ContentType { get; set; }
}