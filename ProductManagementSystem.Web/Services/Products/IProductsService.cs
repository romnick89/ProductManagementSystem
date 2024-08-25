using Microsoft.AspNetCore.Mvc.Rendering;
using ProductManagementSystem.Web.Models.Product;
using ProductManagementSystem.Web.Models.ProductTypes;

namespace ProductManagementSystem.Web.Services.Products
{
    public interface IProductsService
    {
        Task <List<ProductReadOnlyVM>> GetAllProductsAsync();
        Task<T?> GetProductAsync<T>(int id) where T : class;
        Task CreateAsync(ProductCreateVM productCreateVM);
        Task EditProductAsync(ProductEditVM productEditVM);
        Task DeleteAsync(int id);
        bool ProductExists(int id);
        Task<bool> CheckIfProductNameExistsAsyncCreate(string name);
        Task<bool> CheckIfProductNameExistsAsyncEdit(ProductEditVM productEditVM);
        Task<List<ProductReadOnlyVM>> GetAllProductsAsyncByType(int id);
    }
}
