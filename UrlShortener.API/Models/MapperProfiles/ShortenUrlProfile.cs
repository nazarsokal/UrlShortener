using AutoMapper;
using Models.DTOs.ShortenUrlDTOs;
using Models;

namespace Models.MapperProfiles;

public class ShortenUrlProfile : Profile
{
    public ShortenUrlProfile()
    {
        // Map from Create DTO to entity and back. Create DTO uses `ShortenUrl` while entity uses `ShortenedLink`.
        CreateMap<CreateShortenUrlDto, ShortenUrl>()
            .ForMember(dest => dest.ShortenedLink, opt => opt.MapFrom(src => src.ShortenUrl))
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.UserIdCreatedBy, opt => opt.Ignore())
            .ForMember(dest => dest.ApplicationUserCreated, opt => opt.Ignore());

        // Entity -> summary DTO
        CreateMap<ShortenUrl, GetShortenUrlSummaryDto>();

        // Entity -> detail DTO
        CreateMap<ShortenUrl, GetShortenUrlDetailDto>();
    }
}