using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManagementSystem.Web.Data
{
    public class ProductType
    {
        public int Id { get; set; }
        
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        
        [Column(TypeName = "nvarchar(150)")]
        public string Description { get; set; }
    }
}
