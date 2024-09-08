using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Web.Models.OrderList;

namespace ProductManagementSystem.Web.Services.OrderLists
{
    public class OrderListsService(ApplicationDbContext context, IMapper mapper) : IOrderListsService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task AddToOrderList(OrderListAddToOrderVM orderListAddToOrderVM)
        {
            var data = _mapper.Map<OrderList>(orderListAddToOrderVM);
            
            await _context.AddAsync(data);
            await _context.SaveChangesAsync();
        }

        public async Task<List<OrderListReadOnlyVM>> GetAllOrdersList()
        {
            var orderList = await _context.OrderList.Include(o => o.Product)
                .ThenInclude(x => x.ProductType).OrderBy(s => s.Product.ProductType)
                .ToListAsync();
            return _mapper.Map<List<OrderListReadOnlyVM>>(orderList);
        }

        public async Task<T?> GetOrderListAsync<T>(int id) where T : class
        {
            var orderList = await _context.OrderList
                .Include(o => o.Product)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (orderList == null)
            {
                return null;
            }

            return _mapper.Map<T>(orderList);
        }

        public async Task RemoveFromOrderList(int id)
        {
            var orderList = await _context.OrderList.FindAsync(id);
            if (orderList != null)
            {
                _context.OrderList.Remove(orderList);
            }

            await _context.SaveChangesAsync();
        }

        public bool OrderListExists(int id)
        {
            return _context.OrderList.Any(e => e.Id == id);
        }

        public async Task<bool> CheckIfProductAlredyInOrderList(int id)
        {
            return await _context.OrderList.AnyAsync(x => x.ProductId == id);
        }
    }
}
