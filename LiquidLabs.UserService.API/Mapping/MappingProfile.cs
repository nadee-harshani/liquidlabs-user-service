using AutoMapper;
using LiquidLabs.UserService.API.Dtos;
using LiquidLabs.UserService.Domain.Entities;

namespace LiquidLabs.UserService.API.Mapping
{
    /// <summary>
    /// AutoMapper profile defining mappings between domain models and DTOs.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<AddressDto, Address>().ReverseMap();
        }
    }
}
