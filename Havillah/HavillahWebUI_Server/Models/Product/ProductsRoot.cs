namespace HavillahWebUI_Server.Models.Product;

public class ProductsRoot
{
    public List<Value> value { get; set; }
    public bool isSuccess { get; set; }
    public string error { get; set; }
    public object message { get; set; }
    public object responseCode { get; set; }
    
    public class Value
    {
        public string id { get; set; }
        public object productName { get; set; }
        public string productCode { get; set; }
        public object barcode { get; set; }
        public string description { get; set; }
        public int unitOfMeasureId { get; set; }
        public double defaultBuyingPrice { get; set; }
        public double defaultSellingPrice { get; set; }
        public int branchId { get; set; }
        public int currencyId { get; set; }
        public string productImage { get; set; }
        public int productImageLength { get; set; }
        public string productImageExtension { get; set; }
        public DateTime dateAdded { get; set; }
        public string Image { get; set; }
        public string BrandName { get; set; }
        public string Sizes { get; set; }
        public string Colours { get; set; }
    }
}

public class ProductRoot
{
    public Value value { get; set; }
    public bool isSuccess { get; set; }
    public string error { get; set; }
    public object message { get; set; }
    public object responseCode { get; set; }
    
    public class Value
    {
        public string id { get; set; }
        public object productName { get; set; }
        public string productCode { get; set; }
        public object barcode { get; set; }
        public string description { get; set; }
        public int unitOfMeasureId { get; set; }
        public double defaultBuyingPrice { get; set; }
        public double defaultSellingPrice { get; set; }
        public int branchId { get; set; }
        public int currencyId { get; set; }
        public string productImage { get; set; }
        public int productImageLength { get; set; }
        public string productImageExtension { get; set; }
        public DateTime dateAdded { get; set; }
        public string Image { get; set; }
        public string BrandName { get; set; }
        public string Sizes { get; set; }
        public string Colours { get; set; }
    }
}