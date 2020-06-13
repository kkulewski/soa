namespace Retail.Catalog.Web.Models
{
    public class Product
    {
        public string ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
