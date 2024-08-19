using ProductManagementSystem.Web.Models.ProductTypes;

namespace ProductManagementSystem.Web.Models.Product
{
    public class ProductReadOnlyVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ProductTypeReadOnlyVM ProductType { get; set; }
        public int Quantity { get; set; }
    }
}
