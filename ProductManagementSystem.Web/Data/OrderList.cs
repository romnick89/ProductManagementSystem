using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagementSystem.Web.Data
{
    public class OrderList : BaseEntity
    {
        [ForeignKey("ProductId")]
        public Product? Product { get; set; }
        public int ProductId { get; set; }
    }
}
