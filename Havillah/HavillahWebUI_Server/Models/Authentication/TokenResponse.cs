namespace HavillahWebUI_MVC.Models.Authentication;

public class TokenResponse
{
    public TokenValue value { get; set; }
    public bool isSuccess { get; set; }
    public string error { get; set; }
    public object message { get; set; }
    public string responseCode { get; set; }
}

public class TokenValue
{
    public string tokenValue { get; set; }
    public string expiration { get; set; }
}