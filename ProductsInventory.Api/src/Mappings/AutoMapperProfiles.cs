using AutoMapper;
using ProductsInventory.Api.Data.DTOs;
using ProductsInventory.Api.Data.Entities;
using ProductsInventory.Api.Data.Requests;
using ProductsInventory.Api.Entities;

namespace ProductsInventory.Api.Mappings;
public class AutoMapperProfiles : Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Product, ProductDto>().ReverseMap();
        CreateMap<CreateProductRequest, Product>();
        CreateMap<User, UserDto>();
        CreateMap<User, UserRequest>()
            .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.PasswordHash));
        CreateMap<UserRequest, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.MapFrom(src => src.Password));
    }

}