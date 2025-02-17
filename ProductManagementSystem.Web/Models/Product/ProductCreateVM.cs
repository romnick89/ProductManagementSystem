﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProductManagementSystem.Web.Models.Product
{
    public class ProductCreateVM : IValidatableObject
    {
        [Required]
        [StringLength(50, ErrorMessage = "Exceeded maximum character allowed")]
        [Display(Name = "Product Name")]
        public string Name { get; set; }
        [Required]
        [StringLength(50, ErrorMessage = "Exceeded maximum character allowed")]
        [Display(Name = "Product Description")]
        public string Description { get; set; }
        //store productId on create
        [Display(Name = "Product Type")]
        public int ProductTypeId { get; set; }
        //store list of product types
        public SelectList? ProductTypes { get; set; }
        [Display(Name = "Quantity in Stock")]
        public int Quantity { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Quantity < 0)
            {
                yield return new ValidationResult("The QUANTITY must not go lower than zero");
            }
        }
    }
}
