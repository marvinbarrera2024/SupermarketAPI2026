using AutoMapper;
using SupermarketAPI.DTOs;
using SupermarketAPI.Models;

namespace SupermarketAPI.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
            //Modelo -> DTO
            CreateMap<Product, ProductResponse>();
            CreateMap<User, UserResponse>();
            CreateMap<Brand, BrandResponse>();

            //DTO -> Modelo
            CreateMap<ProductRequest, Product>();
            CreateMap<UserRequest, User>();
            CreateMap<BrandRequest, Brand>();
        }
    }
}
