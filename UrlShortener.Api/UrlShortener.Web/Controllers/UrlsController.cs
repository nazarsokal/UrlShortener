using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.DTOs.Url;
using UrlShortener.Application.ServiceAbstractions;
using UrlShortener.Web.Contracts.Request;

namespace UrlShortener.Web.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UrlsController : ControllerBase
{
    private readonly IShortenUrlService shortenUrlService;
    private readonly IMapper mapper;

    public UrlsController(IShortenUrlService shortenUrlService, IMapper mapper)
    {
        this.shortenUrlService = shortenUrlService;
        this.mapper = mapper;
    }

    [HttpPost("/shorten")]
    public async Task<ActionResult<string>> ShortenUrlAsync([FromBody] CreateShortenUrlRequest request)
    {
        var url = this.mapper.Map<CreateShortenUrlDto>(request);
        var shortenedUrl = await this.shortenUrlService.CreateShortenUrlAsync(url);
        return Ok(shortenedUrl);
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UrlSummaryDto>>> GetUrlsAsync()
    {
        var urls = await this.shortenUrlService.GetUrlSummariesAsync();
        return Ok(urls);
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<UrlDetailDto>> GetUrlByIdAsync(Guid id)
    {
        var url = await this.shortenUrlService.GetUrlDetailByIdAsync(id);
        if (url == null)
        {
            return NotFound();
        }
        
        return Ok(url);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUrlAsync(Guid id)
    {
        await this.shortenUrlService.DeleteUrlAsync(id);

        return Ok();
    }
}