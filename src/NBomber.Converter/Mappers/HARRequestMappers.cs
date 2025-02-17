using NBomber.Converter.Models;

namespace NBomber.Converter.Mappers
{
    public static class HARRequestMappers
    {
        public static HARRequestWithActionName ToHARRequestWithActionName(this HARRequest harRequest)
        {
            var uri = new Uri(harRequest.Url);

            return new HARRequestWithActionName
            {
                Method = harRequest.Method,
                Url = harRequest.Url,
                HttpVersion = harRequest.HttpVersion,
                Headers = harRequest.Headers,
                QueryString = harRequest.QueryString,
                Cookies = harRequest.Cookies,
                BodySize = harRequest.BodySize,
                PostData = harRequest.PostData,
                ActionName = $"{harRequest.Method} {uri.Host}{uri.PathAndQuery}"
            };
        }
    }
}
