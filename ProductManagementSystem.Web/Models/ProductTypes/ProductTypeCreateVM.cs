using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Web.Models.ProductTypes
{
    public class ProductTypeCreateVM
    {
        [Required]
        [StringLength(50, ErrorMessage = "Exceeded maximum character allowed")]
        [Display(Name = "Product Type Name")]
        public string Name { get; set; } = string.Empty;

        [Required]
        [StringLength(150, ErrorMessage = "Exceeded maximum character allowed")]      
        [Display(Name = "Product Type Description")]
        public string Description { get; set; } = string.Empty;
    }
}
