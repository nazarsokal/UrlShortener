using AutoMapper;
using UrlShortener.Application.DTOs.Url;
using UrlShortener.Web.Contracts.Request;
using UrlShortener.Web.Contracts.Response;

namespace UrlShortener.Web.Contracts;

public class ContractsMappingProfile : Profile
{
    public ContractsMappingProfile()
    {
        // Map CreateShortenUrlContract to CreateShortenUrlDto
        CreateMap<CreateShortenUrlRequest, CreateShortenUrlDto>()
            .ForMember(dest => dest.UrlOriginal, opt => opt.MapFrom(src => src.OriginalUrl));

        // Map UrlDetailDto to UrlDetailResponse
        CreateMap<UrlDetailDto, UrlDetailResponse>();

        // Map UrlSummaryDto to UrlSummaryResponse
        CreateMap<UrlSummaryDto, UrlSummaryResponse>();

        // Map UrlSummaryDto to CreateShortenUrlResponse
        CreateMap<UrlSummaryDto, CreateShortenUrlResponse>();
    }
}