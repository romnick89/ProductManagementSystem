using ProductManagementSystem.Web.Models.Product;

namespace ProductManagementSystem.Web.Models.OrderList
{
    public class OrderListAddToOrderVM
    {
        public int Id { get; set; }
        
        //public ProductForOrderOnlyVM? ProductForOrderOnlyVM { get; set; }
        public int ProductId { get; set; }
    }
}
