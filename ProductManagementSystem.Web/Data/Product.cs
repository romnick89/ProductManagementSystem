using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagementSystem.Web.Data
{
    public class Product : BaseEntity
    {
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }

        [Column(TypeName = "nvarchar(150)")]
        public string Description { get; set; }
        public int Quantity { get; set; }

        [ForeignKey("ProductTypeId")]
        public ProductType? ProductType { get; set; }
        public int ProductTypeId { get; set; }

        public int AmountToBeOrdered { get; set; }
    }
}
