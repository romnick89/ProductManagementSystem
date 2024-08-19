using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagementSystem.Web.Data
{
    public class Product : BaseEntity
    {
        public int Quantity { get; set; }

        [ForeignKey("ProductTypeId")]
        public ProductType? ProductType { get; set; }
        public int ProductTypeId { get; set; }
    }
}
