using AutoMapper;
using UrlShortener.Application.DTOs.Url;
using UrlShortener.DAL.Entities;

namespace UrlShortener.Application.DTOs;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        CreateMap<CreateShortenUrlDto, ShortenUrl>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.UrlOriginal, opt => opt.MapFrom(src => src.UrlOriginal.Trim().ToLower()));
        
        CreateMap<ShortenUrl, UrlDetailDto>()
            .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.UserCreatedBy.Id));
        
        CreateMap<ShortenUrl, UrlSummaryDto>()
            .ForMember(dest => dest.UrlShortened, opt => opt.MapFrom(src => src.UrlShorten));
        
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.DateJoined, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Role, opt => opt.MapFrom(src => "User"));
        
        CreateMap<User, GetUserSummaryDto>();
    }
}