namespace HavillahWebUI_Server.Models.Product;

public class AddProductResponse
{
    public bool IsSuccess { get; set; }
    public string Error { get; set; }
    public string Message { get; set; }
    public bool ResponseCode { get; set; }
}