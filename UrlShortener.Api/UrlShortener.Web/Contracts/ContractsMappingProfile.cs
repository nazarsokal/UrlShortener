using AutoMapper;
using UrlShortener.Application.DTOs;
using UrlShortener.Application.DTOs.Url;
using UrlShortener.Web.Contracts.Request;
using UrlShortener.Web.Contracts.Response;

namespace UrlShortener.Web.Contracts;

public class ContractsMappingProfile : Profile
{
    public ContractsMappingProfile()
    {
        CreateMap<CreateShortenUrlRequest, CreateShortenUrlDto>()
            .ForMember(dest => dest.UrlOriginal, opt => opt.MapFrom(src => src.OriginalUrl));

        CreateMap<UrlDetailDto, UrlDetailResponse>();

        CreateMap<UrlSummaryDto, UrlSummaryResponse>();

        CreateMap<UrlSummaryDto, CreateShortenUrlResponse>();

        CreateMap<PostUserRequest, CreateUserDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.DateJoined, opt => opt.MapFrom(src => DateTime.Now));

        CreateMap<PostUserRequest, LoginUserDto>();
        
        CreateMap<AuthUserDto, AuthResponse>();
    }
}