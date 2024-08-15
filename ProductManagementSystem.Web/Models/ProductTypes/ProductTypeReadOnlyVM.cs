using System.ComponentModel.DataAnnotations;

namespace ProductManagementSystem.Web.Models.ProductTypes
{
    public class ProductTypeReadOnlyVM : BaseProductTypeVM
    {
        [Display(Name = "Product type Name")]
        public string Name { get; set; } = string.Empty;

        [Display(Name = "Product Type Description")]
        public string Description { get; set; } = string.Empty;
    }
}
