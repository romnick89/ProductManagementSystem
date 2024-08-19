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

namespace ProductManagementSystem.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Products.Include(p => p.ProductType);
            await applicationDbContext.ToListAsync();
            var viewData = _mapper.Map<List<ProductReadOnlyVM>>(applicationDbContext);
            return View(viewData);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);

            var viewData = _mapper.Map<ProductReadOnlyVM>(product);
            if (product == null)
            {
                return NotFound();
            }

            return View(viewData);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            var viewData = new ProductCreateVM
            {
                ProductTypes = new SelectList(_context.ProductTypes, "Id", "Name")
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
            if (ModelState.IsValid)
            {
                var data = _mapper.Map<Product>(productCreateVM);
                _context.Add(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            
            productCreateVM.ProductTypes = new SelectList(_context.ProductTypes, "Id", "Name", productCreateVM.ProductTypeId);
            return View(productCreateVM);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            //create new view model
            var viewData = new ProductEditVM
            {
                //add value accordingly from db
                Name = product.Name,
                Description = product.Description,
                ProductTypes = new SelectList(_context.ProductTypes, "Id", "Name"),
                Quantity = product.Quantity
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

            if (ModelState.IsValid)
            {
                try
                {
                    var data = _mapper.Map<Product>(productEditVM);
                    
                    _context.Update(data);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(productEditVM.Id))
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
            productEditVM.ProductTypes = new SelectList(_context.ProductTypes, "Id", "Name", productEditVM.ProductTypeId);
            return View(productEditVM);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            
            var viewData = _mapper.Map<ProductReadOnlyVM>(product);
            
            if (product == null)
            {
                return NotFound();
            }

            return View(viewData);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
