using AutoMapper;
using Ordering.APIs.DTOs;
using Ordering.Core.Models;

namespace Ordering.APIs.Helpers
{
    public class MappingProfile : Profile
    {


        public MappingProfile() 
        {
            CreateMap<Product, ProductToReturnDTO>()
                     .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
                     .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                     .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());
                     //.ForMember(d => d.PictureUrl, o => o.MapFrom(s => $"/{s.PictureUrl}"));
        }
    }
}
