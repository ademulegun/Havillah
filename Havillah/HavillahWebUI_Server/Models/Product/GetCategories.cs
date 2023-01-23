namespace HavillahWebUI_Server.Models.Product;

public class GetCategories
{
    public int id { get; set; }
    public string name { get; set; }

    public class CategoriesResult
    {
        public List<GetCategories>? value { get; set; }
        public bool isSuccess { get; set; }
        public string error { get; set; }
        public string message { get; set; }
        public string responseCode { get; set; }
    }
}