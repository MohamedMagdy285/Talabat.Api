using AutoMapper;
using Tala.Core.Entities.Identity;
using Talab.Api.Dtos;
using Talabat.Core.Entities;
using Tala.Core.Entities.Order_Aggregate;
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

            CreateMap<AddressDto, Tala.Core.Entities.Identity.Address>().ReverseMap();
            
            CreateMap<AddressDto, Tala.Core.Entities.Order_Aggregate.Address>();
        }
    }
}
