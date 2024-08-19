using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProductManagementSystem.Web.Models.Product
{
    public class ProductEditVM : IValidatableObject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //store productId on create
        public int ProductTypeId { get; set; }
        //store list of product types
        public SelectList? ProductTypes { get; set; }
        public int Quantity { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Quantity < 0)
            {
                yield return new ValidationResult("The QUANTITY must not go lower than zero");
            }
        }
    }
}
