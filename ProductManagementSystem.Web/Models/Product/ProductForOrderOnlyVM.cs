﻿using ProductManagementSystem.Web.Models.ProductTypes;

namespace ProductManagementSystem.Web.Models.Product
{
    public class ProductForOrderOnlyVM : BaseProductVM
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
        [Display(Name = "Amount to be Ordered")]
        public int AmountToBeOrdered { get; set; }
        [Display(Name = "Select to Add in the Order List")]
        public bool IsSelected { get; set; }
    }
}
