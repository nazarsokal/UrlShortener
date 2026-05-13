using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.DTOs.Url;
using UrlShortener.Application.ServiceAbstractions;
using UrlShortener.Web.Contracts.Request;

namespace UrlShortener.Web.Controllers;

[ApiController]
[Route("url")]
public class UrlShortenerController : ControllerBase
{
    private readonly IShortenUrlService shortenUrlService;
    private readonly IMapper mapper;

    public UrlShortenerController(IShortenUrlService shortenUrlService, IMapper mapper)
    {
        this.shortenUrlService = shortenUrlService;
        this.mapper = mapper;
    }

    [HttpPost("/shorten")]
    public async Task<ActionResult<string>> ShortenUrlAsync([FromBody] CreateShortenUrlRequest request)
    {
        var url = this.mapper.Map<CreateShortenUrlDto>(request);
        var shortenedUrl = await this.shortenUrlService.CreateShortenUrl(url);
        return Ok(shortenedUrl);
    }
}