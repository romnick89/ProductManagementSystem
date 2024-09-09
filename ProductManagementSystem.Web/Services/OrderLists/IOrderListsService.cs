using ProductManagementSystem.Web.Models.OrderList;

namespace ProductManagementSystem.Web.Services.OrderLists
{
    public interface IOrderListsService
    {
        Task AddToOrderList(OrderListAddToOrderVM orderListAddToOrderVM);
        Task<bool> CheckIfProductAlredyInOrderList(int id);
        Task <List<OrderListReadOnlyVM>> GetAllOrdersList();
        Task <T?> GetOrderListAsync<T>(int id) where T : class;
        bool OrderListExists(int id);
        Task RemoveFromOrderList(int id);
        Task RemoveAllFromOrderList();
    }
}
