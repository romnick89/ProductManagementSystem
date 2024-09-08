using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Web.Data;
using ProductManagementSystem.Web.Models.OrderList;
using ProductManagementSystem.Web.Models.Product;

namespace ProductManagementSystem.Web.Controllers
{
    public class OrderListController : Controller
    {
        private const string ProductInListValidationMessage = "This Product is already on the order list.";
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public OrderListController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: OrderList
        public async Task<IActionResult> Index()
        {
            var data = await _context.OrderList.Include(o => o.Product).ThenInclude(x => x.ProductType).OrderBy(s => s.Product.ProductType).ToListAsync();           
            var viewData = _mapper.Map<List<OrderListReadOnlyVM>>(data);
            return View(viewData);
        }

        /*// GET: OrderList/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderList = await _context.OrderList
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderList == null)
            {
                return NotFound();
            }

            return View(orderList);
        }*/
        //POST: OrderList/AddToOrder
        public async Task<IActionResult> AddToOrder(int id)
        {
            
            if (await CheckIfProductAlredyInOrderList(id))
            {
                ModelState.AddModelError(nameof(id.ToString), ProductInListValidationMessage);
                return RedirectToAction(nameof(Index));
            }

            var viewData = new OrderListAddToOrderVM 
            {
                ProductId = id                              
            };

            var data = _mapper.Map<OrderList>(viewData);
            if (ModelState.IsValid)
            {               
                _context.Add(data);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(data);

        }

        /*// GET: OrderList/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name");
            return View();
        }

        // POST: OrderList/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,Id")] OrderList orderList)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderList);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", orderList.ProductId);
            return View(orderList);
        }*/

        // GET: OrderList/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderList = await _context.OrderList.FindAsync(id);
            if (orderList == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", orderList.ProductId);
            return View(orderList);
        }

        // POST: OrderList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name")] OrderList orderList)
        {
            if (id != orderList.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderList);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderListExists(orderList.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Id", orderList.ProductId);
            return View(orderList);
        }

        // GET: OrderList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderList = await _context.OrderList
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (orderList == null)
            {
                return NotFound();
            }
            var viewData = _mapper.Map<OrderListReadOnlyVM>(orderList);
            return View(viewData);
        }

        // POST: OrderList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderList = await _context.OrderList.FindAsync(id);
            if (orderList != null)
            {
                _context.OrderList.Remove(orderList);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderListExists(int id)
        {
            return _context.OrderList.Any(e => e.Id == id);
        }

        public async Task<bool> CheckIfProductAlredyInOrderList(int id)
        {
            return await _context.OrderList.AnyAsync(x => x.ProductId == id);
        }
    }
}
