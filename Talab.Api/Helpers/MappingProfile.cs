using AutoMapper;
using Talab.Api.Dtos;
using Talabat.Core.Entities;

namespace Talab.Api.Helpers
{
    public class MappingProfile :Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductToReturnDto>()
                .ForMember(P => P.ProductBrand, O => O.MapFrom(s => s.ProductBrand.Name))
                .ForMember(P => P.ProductType, O => O.MapFrom(s => s.ProductType.Name))
                .ForMember(P=>P.PictureUrl, O=> O.MapFrom<ProductPictureUrlResolver>());
        }
    }
}
