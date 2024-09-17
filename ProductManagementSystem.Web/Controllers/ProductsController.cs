using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProductManagementSystem.Web.Data;
using ProductManagementSystem.Web.Models.Product;
using ProductManagementSystem.Web.Models.ProductTypes;
using ProductManagementSystem.Web.Services.Products;
using ProductManagementSystem.Web.Services.ProductTypes;

namespace ProductManagementSystem.Web.Controllers
{
    public class ProductsController : Controller
    {
        private const string NameExistValidationMessage = "This Product Name already exist.";
        private readonly IProductsService _productsService;
        private readonly IProductTypesService _productTypesService;

        public ProductsController(IProductsService productsService, IProductTypesService productTypesService)
        {
            _productsService = productsService;
            _productTypesService = productTypesService;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var viewData = await _productsService.GetAllProductsAsync();
            return View(viewData);
        }
        //Get: products by type
        public async Task<IActionResult> DisplayByType(int id)
        {
            var products = await _productsService.GetAllProductsAsyncByType(id);
            //add productId in TempData
            TempData["productId"] = id.ToString();
            
            var list = new List<ProductForOrderOnlyVM>();
            foreach(var product in products)
            {
                var prod = new ProductForOrderOnlyVM
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    ProductType = product.ProductType,
                    Quantity = product.Quantity,
                    AmountToBeOrdered = product.AmountToBeOrdered,
                    IsSelected = product.IsSelected,
                };
                list.Add(prod);
            }

            return View(list);
        }       

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewData = await _productsService.GetProductAsync<ProductReadOnlyVM>(id.Value);
            
            if (viewData == null)
            {
                return NotFound();
            }

            return View(viewData);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create(int id)
        {

            var productTypesList = new SelectList(await _productTypesService.GetAllAsync(), "Id", "Name");
            var viewData = new ProductCreateVM
            {
                ProductTypeId = id,
                ProductTypes = productTypesList
            };           

            return View(viewData);
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductCreateVM productCreateVM)
        {
            if (await _productsService.CheckIfProductNameExistsAsyncCreate(productCreateVM.Name))
            {
                ModelState.AddModelError(nameof(productCreateVM.Name), NameExistValidationMessage);
            }

            if (ModelState.IsValid)
            {
                await _productsService.CreateAsync(productCreateVM);

                return RedirectToAction("DisplayByType", new { id = productCreateVM.ProductTypeId });
            }

            productCreateVM.ProductTypes = new SelectList(await _productTypesService.GetAllAsync(), "Id", "Name", productCreateVM.ProductTypeId);
            
            return View(productCreateVM);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productsService.GetProductAsync<ProductEditVM>(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            var productTypes = await _productTypesService.GetAllAsync();
            var productTypesList = new SelectList(productTypes, "Id", "Name");

            //create new view model
            var viewData = new ProductEditVM
            {
                //add value accordingly from db
                Name = product.Name,
                Description = product.Description,
                ProductTypeId = product.ProductTypeId,
                ProductTypes = productTypesList,
                Quantity = product.Quantity,
                AmountToBeOrdered = product.AmountToBeOrdered,
                IsSelected = product.IsSelected
            };
            
            return View(viewData);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductEditVM productEditVM)
        {
            if (id != productEditVM.Id)
            {
                return NotFound();
            }
            if (await _productsService.CheckIfProductNameExistsAsyncEdit(productEditVM)) 
            {
                ModelState.AddModelError(nameof(productEditVM.Name), NameExistValidationMessage);
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _productsService.EditProductAsync(productEditVM);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_productsService.ProductExists(productEditVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("DisplayByType", new {id = productEditVM.ProductTypeId});
            }
            productEditVM.ProductTypes = new SelectList(await _productTypesService.GetAllAsync(), "Id", "Name", productEditVM.ProductTypeId);
            
            return View(productEditVM);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _productsService.GetProductAsync<ProductReadOnlyVM>(id.Value);
            
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _productsService.GetProductAsync<ProductEditVM>(id);
            var productTypeId = product?.ProductTypeId;
            
            await _productsService.DeleteAsync(id);
            return RedirectToAction("DisplayByType", new { id = productTypeId });
        }
    }
}
