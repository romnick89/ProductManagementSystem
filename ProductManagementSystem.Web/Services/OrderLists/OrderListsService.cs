using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Web.Models.OrderList;
using ProductManagementSystem.Web.Services.Products;

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

        public async Task RemoveAllFromOrderList()
        {
            var orderList = await _context.OrderList.Include(o => o.Product)               
                .ToListAsync();
            foreach (var order in orderList) 
            {
                _context.Remove(order);
            }
            await _context.SaveChangesAsync();
        }

        public async Task AddAllToOrderList(int id)
        {
            var products = _context.Products.Include(p => p.ProductType).Where(q => q.ProductTypeId == id).OrderBy(x => x.Name);
            await products.ToListAsync();

            var orderList = await _context.OrderList.Include(o => o.Product)
                .ThenInclude(x => x.ProductType).OrderBy(s => s.Product.ProductType)
                .ToListAsync();

            foreach (var product in products)
            {
                if (product.IsSelected.Equals(true) && !await CheckIfProductAlredyInOrderList(product.Id))
                {
                    var data = new OrderListAddToOrderVM
                    {                        
                        ProductId = product.Id
                    };
                    await AddToOrderList(data);
                }
                
            }
        }
    }
}
