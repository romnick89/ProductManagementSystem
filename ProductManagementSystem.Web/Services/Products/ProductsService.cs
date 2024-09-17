using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductManagementSystem.Web.Data.Migrations;
using ProductManagementSystem.Web.Models.Product;
using ProductManagementSystem.Web.Models.ProductTypes;
using ProductManagementSystem.Web.Services.ProductTypes;

namespace ProductManagementSystem.Web.Services.Products
{
    public class ProductsService(ApplicationDbContext context, IMapper mapper) : IProductsService
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IMapper _mapper = mapper;

        public async Task CreateAsync(ProductCreateVM productCreateVM)
        {
            
            var product = _mapper.Map<Product>(productCreateVM);
            _context.Add(product);
            await _context.SaveChangesAsync();          
        }

        public async Task DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductReadOnlyVM>> GetAllProductsAsync()
        {
            var products = _context.Products.Include(p => p.ProductType).OrderBy(x => x.ProductType).ThenBy(m => m.Name);
            await products.ToListAsync();            
            var viewData = _mapper.Map<List<ProductReadOnlyVM>>(products);

            return viewData;
        }

        public async Task<List<ProductForOrderOnlyVM>> GetAllProductsAsyncByType(int id)
        {
            var products = _context.Products.Include(p => p.ProductType).Where(q => q.ProductTypeId == id).OrderBy(x => x.Name);
            await products.ToListAsync();

            var viewData = _mapper.Map<List<ProductForOrderOnlyVM>>(products);
            return viewData;
        }

        public async Task<T?> GetProductAsync<T>(int id) where T : class
        {
            var product = await _context.Products
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return null;
            }

            var viewData = _mapper.Map<T>(product);

            return viewData;
        }

        public async Task EditProductAsync(ProductEditVM productEditVM)
        {
            var product = _mapper.Map<Product>(productEditVM);

            _context.Update(product);
            await _context.SaveChangesAsync();
        }

        public bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }

        public async Task<bool> CheckIfProductNameExistsAsyncCreate(string name)
        {
            return await _context.Products.AnyAsync(e => e.Name.ToLower() == name.ToLower());
        }

        public async Task<bool> CheckIfProductNameExistsAsyncEdit(ProductEditVM productEditVM)
        {
            return await _context.Products.AnyAsync(e => e.Name.ToLower() == productEditVM.Name.ToLower() && e.Id != productEditVM.Id);
        }


        public async Task UpdateAsync(ProductForOrderOnlyVM entity)
        {
            var product = _mapper.Map<Product>(entity);
            
            _context.Update(product);
            await _context.SaveChangesAsync();            
        }
    }
}
