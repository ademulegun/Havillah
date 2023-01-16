namespace HavillahWebUI_Server.Models.Product;

public class Response<T>
{
    public bool isSuccess { get; set; }
    public string error { get; set; }
    public string message { get; set; }
    public string responseCode { get; set; }
    public T value { get; set; }
}