using AutoMapper;
using Models.DTOs.UserDTOs;
using Models;

namespace Models.MapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        // Map registration DTO to ApplicationUser. Password is not stored on the entity directly here (Identity handles it),
        // so we ignore it on reverse map.
        CreateMap<RegisterUserDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ShortenedUrls, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Password, opt => opt.Ignore());

        // Login DTO -> ApplicationUser (used sometimes for convenience). Reverse mapping ignores Password.
        CreateMap<LoginUserDto, ApplicationUser>()
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ShortenedUrls, opt => opt.Ignore())
            .ReverseMap()
            .ForMember(dest => dest.Password, opt => opt.Ignore());

        // ApplicationUser -> ReturnUserDto. Roles are managed outside of the entity, so ignore mapping for Roles here.
        CreateMap<ApplicationUser, ReturnUserDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Roles, opt => opt.Ignore());
    }
}

