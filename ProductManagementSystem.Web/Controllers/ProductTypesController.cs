using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Web.Data;
using ProductManagementSystem.Web.Models.Product;
using ProductManagementSystem.Web.Models.ProductTypes;
using ProductManagementSystem.Web.Services.ProductTypes;

namespace ProductManagementSystem.Web.Controllers
{

    public class ProductTypesController(IProductTypesService productTypeService) : Controller
    {      
        private const string NameExistValidationMessage = "This Product Type already exist.";
        private readonly IProductTypesService _productTypeService = productTypeService;

        [Authorize(Roles = "Administrator, Supervisor, Employee")]
        // GET: ProductTypes
        public async Task<IActionResult> Index()
        {
            var viewData = await _productTypeService.GetAllAsync();
            //return view model
            return View(viewData);
        }

        // GET: ProductTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _productTypeService.GetAsync<ProductTypeReadOnlyVM>(id.Value);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        [Authorize(Roles = Roles.Administrator)]
        // GET: ProductTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ProductTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductTypeCreateVM productTypeCreateVM)
        {
            if (await _productTypeService.CheckIfProductNameExistsAsyncCreate(productTypeCreateVM.Name))
            {
                ModelState.AddModelError(nameof(productTypeCreateVM.Name), NameExistValidationMessage);
            }

            if (ModelState.IsValid)
            {
                await _productTypeService.CreateAsync(productTypeCreateVM);
                return RedirectToAction(nameof(Index));
            }
            return View(productTypeCreateVM);
        }

        [Authorize(Roles = Roles.Administrator)]
        // GET: ProductTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _productTypeService.GetAsync<ProductTypeEditVM>(id.Value);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }       
        // POST: ProductTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ProductTypeEditVM productTypeEditVM)
        {
            if (id != productTypeEditVM.Id)
            {
                return NotFound();
            }

            if (await _productTypeService.CheckIfProductNameExistsAsyncEdit(productTypeEditVM))
            {
                ModelState.AddModelError(nameof(productTypeEditVM.Name), NameExistValidationMessage);
            }

            if (ModelState.IsValid)
            {               
                try
                {
                    await _productTypeService.EditAsync(productTypeEditVM);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_productTypeService.ProductTypeExists(productTypeEditVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(productTypeEditVM);
        }

        [Authorize(Roles = Roles.Administrator)]
        // GET: ProductTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _productTypeService.GetAsync<ProductTypeReadOnlyVM>(id.Value);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        // POST: ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _productTypeService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
