using ProductManagementSystem.Web.Models.Product;

namespace ProductManagementSystem.Web.Models.OrderList
{
    public class OrderListReadOnlyVM
    {
        public int Id { get; set; }
        public ProductForOrderOnlyVM Product { get; set; } = new ProductForOrderOnlyVM();       
    }
}
