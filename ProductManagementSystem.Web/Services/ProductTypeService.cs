using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductManagementSystem.Web.Data;
using ProductManagementSystem.Web.Models.ProductTypes;

namespace ProductManagementSystem.Web.Services
{
    public class ProductTypeService : IProductTypeService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ProductTypeService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<ProductTypeReadOnlyVM>> GetAllAsync()
        {
            //Select * Product Types
            var data = await _context.ProductTypes.ToListAsync();
            //convert database data to view model
            var viewData = _mapper.Map<List<ProductTypeReadOnlyVM>>(data);
            //return view model
            return viewData;
        }

        public async Task<T?> GetAsync<T>(int id) where T : class
        {
            var data = await _context.ProductTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (data == null)
            {
                return null;
            }
            var viewData = _mapper.Map<T>(data);
            return viewData;
        }
        public async Task CreateAsync(ProductTypeCreateVM viewModel)
        {
            var viewData = _mapper.Map<ProductType>(viewModel);
            await _context.AddAsync(viewData);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(ProductTypeEditVM viewModel)
        {
            var viewData = _mapper.Map<ProductType>(viewModel);
            _context.Update(viewData);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var data = _context.ProductTypes.FirstOrDefault(x => x.Id == id);
            if (data != null)
            {
                _context.ProductTypes.Remove(data);
                await _context.SaveChangesAsync();
            }
        }
        public bool ProductTypeExists(int id)
        {
            return _context.ProductTypes.Any(e => e.Id == id);
        }

        //Check if product name already exist on create
        public async Task<bool> CheckIfProductNameExistsAsyncCreate(string name)
        {
            return await _context.ProductTypes.AnyAsync(m => m.Name.ToLower() == name.ToLower());
        }
        //Check if product name already exist on edit
        public async Task<bool> CheckIfProductNameExistsAsyncEdit(ProductTypeEditVM productTypeEditVM)
        {
            return await _context.ProductTypes.AnyAsync(m => m.Name.ToLower() == productTypeEditVM.Name.ToLower()
                && m.Id != productTypeEditVM.Id);
        }
    }
}
