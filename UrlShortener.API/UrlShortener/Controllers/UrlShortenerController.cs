using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Models;
using Models.DTOs.ShortenUrlDTOs;
using Services.ServiceContracts;

namespace UrlShortener.Controllers;

[ApiController]
[Route("api/urlshortener")]
public class UrlShortenerController : ControllerBase
{
    private readonly IUrlShortenerService _urlShortenerService;
    private readonly UserManager<ApplicationUser> _userManager;


    public UrlShortenerController(IUrlShortenerService urlShortenerService, UserManager<ApplicationUser> userManager)
    {
        _urlShortenerService = urlShortenerService;
        _userManager = userManager;
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "USER,ADMIN")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateShortenedUrl([FromBody] CreateShortenUrlDto dto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
        var result = await _urlShortenerService.SaveShortenUrlAsync(dto, userId).ConfigureAwait(false);

        if (result != Guid.Empty)
        {
            return Ok(result);
        }

        return BadRequest();
    }
    
    [HttpGet("generate")]
    public async Task<IActionResult> GenerateShortenedUrl(string originalUrl)
    {
        var result = await _urlShortenerService.ShortenUrlAsync(originalUrl).ConfigureAwait(false);

        if (!string.IsNullOrEmpty(result))
        {
            return Ok(result);
        }

        return BadRequest();
    }
    
    [HttpGet("get/{id}")]
    public async Task<IActionResult> GetShortenedUrlById(Guid id)
    {
        var result = await _urlShortenerService.GetShortenedUrlAsync(id).ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllShortenedUrls()
    {
        var result = await _urlShortenerService.GetShortenedUrlsAsync().ConfigureAwait(false);
        return Ok(result);
    }

    [HttpGet("/{shortLink}")]
    public async Task<IActionResult> GetShortenedUrlByShortLink(string shortLink)
    {
        var urlSummary = await _urlShortenerService.GetShortenedUrlByShortenedLinkAsync(shortLink).ConfigureAwait(false);
        
        if (urlSummary.IsNullOrEmpty())
            return NotFound();
        
        return Redirect(urlSummary);
    }

    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "USER,ADMIN")]
    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteShortenedUrl(Guid id)
    {
        var result = await _urlShortenerService.DeleteShortenedUrlAsync(id).ConfigureAwait(false);
        if (result)
            return Ok();
        return NotFound();
    }
}