using AutoMapper;
using Microsoft.CodeAnalysis.Differencing;
using ProductManagementSystem.Web.Data;
using ProductManagementSystem.Web.Models.ProductTypes;

namespace ProductManagementSystem.Web.Configurations
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            //map data to view || view to data models
            //direction is important || use reverse map to map both direction
            CreateMap<ProductType, ProductTypeReadOnlyVM>();
            CreateMap<ProductTypeCreateVM,ProductType>();
            CreateMap<ProductTypeEditVM,ProductType>().ReverseMap();
        }
    }
}
