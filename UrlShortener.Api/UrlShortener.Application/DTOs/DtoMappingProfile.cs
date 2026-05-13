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
            .ForMember(dest => dest.DateCreated, opt => opt.MapFrom(src => DateTime.Now));
        
        CreateMap<ShortenUrl, UrlDetailDto>()
            .ForMember(dest => dest.CreatedByUser, opt => opt.MapFrom(src => src.UserCreatedBy.Username));
        
        CreateMap<ShortenUrl, UrlSummaryDto>()
            .ForMember(dest => dest.UrlShortened, opt => opt.MapFrom(src => src.UrlShorten));
    }
}