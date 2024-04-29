using System;
using AutoMapper;
using nc.productmanager.Data.Models;
using nc.productmanager.Dto.Product;

namespace nc.productmanager.Api.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Product, GetProductDto>().ReverseMap();
            CreateMap<ProductCategory, ProductCategoryDto>().ReverseMap();
            CreateMap<ProductCategory, GetProductCategoryDto>().ReverseMap();

        }
    }
}

