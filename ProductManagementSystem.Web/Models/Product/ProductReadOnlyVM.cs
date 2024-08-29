using ProductManagementSystem.Web.Models.ProductTypes;

namespace ProductManagementSystem.Web.Models.Product
{
    public class ProductReadOnlyVM : BaseProductVM
    {
        [Required]
        [StringLength(50, ErrorMessage = "Exceeded maximum character allowed")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Exceeded maximum character allowed")]
        [Display(Name = "Product Description")]
        public string Description { get; set; }
        [Display(Name = "Product Type")]
        public ProductTypeReadOnlyVM ProductType { get; set; }
        [Display(Name = "Quantity in Stock")]
        public int Quantity { get; set; }
    }
}
