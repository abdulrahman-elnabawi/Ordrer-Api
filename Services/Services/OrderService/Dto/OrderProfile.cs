using AutoMapper;
using Core.Entities.OrderEntities;
using Core.IdentityEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.OrderService.Dto
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
            CreateMap<AddressDto, ShippingAddress>().ReverseMap();

            CreateMap<Order, OrderResultDto>()
                .ForMember(dest => dest.DeliveryMethod, option => option.MapFrom(src => src.DeliveryMethod.ShortName))
                .ForMember(dest => dest.ShippingPrice, option => option.MapFrom(src => src.DeliveryMethod.Price));

            CreateMap<OrderItem, OrderItemDto>()
                    .ForMember(dest => dest.ProductItemId, option => option.MapFrom(src => src.ItemOrderd.ProductItemId))
                    .ForMember(dest => dest.ProductName, option => option.MapFrom(src => src.ItemOrderd.ProductName))
                    .ForMember(dest => dest.PictureUrl, option => option.MapFrom(src => src.ItemOrderd.PictureUrl))
                    .ForMember(dest => dest.PictureUrl, option => option.MapFrom<OrderItemUrlResolver>()).ReverseMap();


        }
    }
}
