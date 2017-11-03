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
            CreateMap<CategoryEntity, CategoryDto>().ReverseMap();

            CreateMap<SubCategoryEntity, SubCategoryDto>().ReverseMap();

            CreateMap<ProductEntity, ProductDto>().ReverseMap();

            CreateMap<PublisherEntity, PublisherDto>().ReverseMap();
        }
    }
}
