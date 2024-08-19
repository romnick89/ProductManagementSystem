using ProductManagementSystem.Web.Models.ProductTypes;

namespace ProductManagementSystem.Web.Services.ProductTypes
{
    public interface IProductTypesService
    {
        Task<bool> CheckIfProductNameExistsAsyncCreate(string name);
        Task<bool> CheckIfProductNameExistsAsyncEdit(ProductTypeEditVM productTypeEditVM);
        Task CreateAsync(ProductTypeCreateVM viewModel);
        Task DeleteAsync(int id);
        Task EditAsync(ProductTypeEditVM viewModel);
        Task<List<ProductTypeReadOnlyVM>> GetAllAsync();
        Task<T?> GetAsync<T>(int id) where T : class;
        bool ProductTypeExists(int id);
    }
}