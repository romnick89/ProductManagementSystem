using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Web.Data;
using ProductManagementSystem.Web.Models.ProductTypes;

namespace ProductManagementSystem.Web.Controllers
{
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private const string NameExistValidationMessage = "This Product Type already exist.";

        public ProductTypesController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: ProductTypes
        public async Task<IActionResult> Index()
        {
            //Select * Product Types
            var data = await _context.ProductTypes.ToListAsync();
            //convert database data to view model
            var viewData = _mapper.Map<List<ProductTypeReadOnlyVM>>(data);
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

            var productType = await _context.ProductTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productType == null)
            {
                return NotFound();
            }

            var viewData = _mapper.Map<ProductTypeReadOnlyVM>(productType);

            return View(viewData);
        }

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
            if (await CheckIfProductNameExistsAsync(productTypeCreateVM.Name))
            {
                ModelState.AddModelError(nameof(productTypeCreateVM.Name), NameExistValidationMessage);
            }

            if (ModelState.IsValid)
            {
                var productType = _mapper.Map<ProductType>(productTypeCreateVM);
                _context.Add(productType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypeCreateVM);
        }     

        // GET: ProductTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }
            var viewData = _mapper.Map<ProductTypeEditVM>(productType);

            return View(viewData);
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

            if (await CheckIfProductNameExistsAsyncEdit(productTypeEditVM))
            {
                ModelState.AddModelError(nameof(productTypeEditVM.Name), NameExistValidationMessage);
            }

            if (ModelState.IsValid)
            {               
                try
                {
                    var data = _mapper.Map<ProductType>(productTypeEditVM);
                    _context.Update(data);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductTypeExists(productTypeEditVM.Id))
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

        // GET: ProductTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _context.ProductTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (productType == null)
            {
                return NotFound();
            }
            var viewData = _mapper.Map<ProductTypeReadOnlyVM>(productType);
            return View(viewData);
        }

        // POST: ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productType = await _context.ProductTypes.FindAsync(id);
            if (productType != null)
            {
                _context.ProductTypes.Remove(productType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductTypeExists(int id)
        {
            return _context.ProductTypes.Any(e => e.Id == id);
        }

        //Check if product name already exist
        private async Task<bool> CheckIfProductNameExistsAsync(string name)
        {
            return await _context.ProductTypes.AnyAsync(m => m.Name.ToLower() == name.ToLower());
        }

        private async Task<bool> CheckIfProductNameExistsAsyncEdit(ProductTypeEditVM productTypeEditVM)
        {
            return await _context.ProductTypes.AnyAsync(m => m.Name.ToLower() == productTypeEditVM.Name.ToLower()
                && m.Id != productTypeEditVM.Id);
        }
    }
}
