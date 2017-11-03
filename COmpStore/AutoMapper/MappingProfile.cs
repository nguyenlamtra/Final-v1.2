using AutoMapper;
using COmpStore.Dto;
using COmpStore.Schema.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace COmpStore.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();

            CreateMap<SubCategory, SubCategoryDto>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();

            CreateMap<Publisher, PublisherDto>().ReverseMap();
        }
    }
}
