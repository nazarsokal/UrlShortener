using System.Security.Claims;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpPost("shorten")]
    public async Task<ActionResult<string>> ShortenUrlAsync([FromBody] CreateShortenUrlRequest request)
    {
        var url = this.mapper.Map<CreateShortenUrlDto>(request);
        
        url.UserId = GetCurrentUserId();
        
        var shortenedUrl = await this.shortenUrlService.CreateShortenUrlAsync(url);
        return Ok(shortenedUrl);
    }
    
    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UrlSummaryDto>>> GetUrlsAsync()
    {
        var urls = await this.shortenUrlService.GetUrlSummariesAsync();
        return Ok(urls);
    }
    
    [Authorize]
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

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUrlAsync(Guid id)
    {
        var currentUserId = GetCurrentUserId();
        var ownerId = await shortenUrlService.GetOwnerIdByUrlIdAsync(id);

        if (ownerId == null)
            return NotFound();

        if (ownerId.Value != currentUserId)
            return Forbid();

        await shortenUrlService.DeleteUrlAsync(id);
        return Ok();
    }
    
    [Authorize]
    [HttpGet("createdBy/{userId}")]
    public async Task<ActionResult<IEnumerable<UrlSummaryDto>>> GetUrlsByUserIdAsync(Guid userId)
    {
        var currentUserId = GetCurrentUserId();
        var urls = await shortenUrlService.GetUrlSummariesByUserIdAsync(currentUserId);
        return Ok(urls);
    }
    
    private Guid GetCurrentUserId()
    {
        var raw = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (raw != null)
        {
            return Guid.Parse(raw);
        }
        
        return Guid.Empty;
    }
}