using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Application.Exceptions;
using UrlShortener.Application.ServiceAbstractions;

namespace UrlShortener.Web.Controllers;

[Route("")]
[AllowAnonymous]
public class HomeController : Controller
{
    private readonly IShortenUrlService shortenUrlService;

    public HomeController(IShortenUrlService shortenUrlService)
    {
        this.shortenUrlService = shortenUrlService;
    }
    
    [HttpGet("{code}")]
    public async Task<IActionResult> Rediderct(string code)
    {
        var originalUrl = await this.shortenUrlService.GetOriginalUrlByShortCodeAsync(code);

        if (string.IsNullOrEmpty(originalUrl))
        {
            throw new NotFoundException($"Could not find original url for code {code}");
        }
        
        if (!originalUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && 
            !originalUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            originalUrl = "https://" + originalUrl;
        }
        
        return this.Redirect(originalUrl);
    }
}