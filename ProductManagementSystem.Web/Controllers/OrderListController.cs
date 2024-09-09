using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Web.Models.OrderList;
using ProductManagementSystem.Web.Services.OrderLists;


namespace ProductManagementSystem.Web.Controllers
{
    public class OrderListController : Controller
    {
        private const string ProductInListValidationMessage = "This Product is already on the order list.";
        private readonly IOrderListsService _orderListsService;

        public OrderListController(IOrderListsService orderListsService)
        {
            _orderListsService = orderListsService;
        }

        // GET: OrderList
        public async Task<IActionResult> Index()
        {
            var viewData = await _orderListsService.GetAllOrdersList();
            return View(viewData);
        }

        //POST: OrderList/AddToOrder
        public async Task<IActionResult> AddToOrder(int id)
        {
            
            if (await _orderListsService.CheckIfProductAlredyInOrderList(id))
            {
                ModelState.AddModelError(nameof(id.ToString), ProductInListValidationMessage);
                return RedirectToAction(nameof(Index));
            }

            var viewData = new OrderListAddToOrderVM 
            {
                ProductId = id                              
            };
            
            if (ModelState.IsValid)
            {               
                await _orderListsService.AddToOrderList(viewData);
                return RedirectToAction(nameof(Index));
            }
            return View(viewData);

        }

        // GET: OrderList/Edit/5
        /*public async Task<IActionResult> Edit(int? id)
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
        }*/

        // POST: OrderList/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        /*[HttpPost]
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
            ViewData["ProductId"] = new SelectList(_context.Products, "Id", "Name", orderList.ProductId);
            return View(orderList);
        }*/

        // GET: OrderList/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var viewData = await _orderListsService.GetOrderListAsync<OrderListReadOnlyVM>(id.Value);
            return View(viewData);
        }

        // POST: OrderList/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _orderListsService.RemoveFromOrderList(id);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> RemoveAll()
        {
            await _orderListsService.RemoveAllFromOrderList();
            return RedirectToAction(nameof(Index));
        }
    }
}
