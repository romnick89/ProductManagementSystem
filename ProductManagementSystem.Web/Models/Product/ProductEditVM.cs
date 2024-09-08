using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProductManagementSystem.Web.Models.Product
{
    public class ProductEditVM : BaseProductVM, IValidatableObject
    {
        [Required]
        [StringLength(50, ErrorMessage = "Exceeded maximum character allowed")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Exceeded maximum character allowed")]
        [Display(Name = "Product Description")]
        public string Description { get; set; }
        //store productId on edit
        [Display(Name = "Product Type")]
        public int ProductTypeId { get; set; }
        //store list of product types
        public SelectList? ProductTypes { get; set; }
        [Display(Name = "Quantity in Stock")]
        public int Quantity { get; set; }
        [Display(Name = "Amount to be Ordered")]
        public int AmountToBeOrdered { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Quantity < 0)
            {
                yield return new ValidationResult("The QUANTITY must not go lower than zero");
            }
        }
    }
}
