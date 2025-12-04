using AutoMapper;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Moq;
using Models;
using Models.DTOs.ShortenUrlDTOs;
using Services;
using Xunit;

namespace UrlShortenerTests;

public class UrlShortenerServiceTests
{
    private readonly UrlShortenerDbContext _dbContext;
    private readonly Mock<IMapper> _mapperMock;
    private readonly UrlShortenerService _service;

    public UrlShortenerServiceTests()
    {
        var options = new DbContextOptionsBuilder<UrlShortenerDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        _dbContext = new UrlShortenerDbContext(options);

        _mapperMock = new Mock<IMapper>();

        _service = new UrlShortenerService(_dbContext, _mapperMock.Object);
    }

    [Fact]
    public async Task SaveShortenUrlAsync_ShouldReturnId_WhenSuccess()
    {
        var dto = new CreateShortenUrlDto
        {
            OriginalLink = "https://google.com"
        };

        var entity = new ShortenUrl
        {
            Id = Guid.NewGuid(),
            OriginalLink = dto.OriginalLink
        };

        _mapperMock
            .Setup(m => m.Map<ShortenUrl>(dto))
            .Returns(entity);

        var userId = Guid.NewGuid();
        
        var result = await _service.SaveShortenUrlAsync(dto, userId);
        
        Assert.NotEqual(Guid.Empty, result);
        Assert.Single(_dbContext.ShortenedUrls);
    }

    [Fact]
    public async Task SaveShortenUrlAsync_ShouldReturnEmpty_WhenDuplicate()
    {
        var existing = new ShortenUrl
        {
            Id = Guid.NewGuid(),
            ShortenedLink = "http://localhost:5130/test"
        };

        _dbContext.ShortenedUrls.Add(existing);
        await _dbContext.SaveChangesAsync();

        var dto = new CreateShortenUrlDto
        {
            OriginalLink = "https://google.com",
            ShortenUrl = existing.ShortenedLink
        };

        _mapperMock.Setup(m => m.Map<ShortenUrl>(dto))
            .Returns(existing);
        
        var result = await _service.SaveShortenUrlAsync(dto, Guid.NewGuid());
        
        Assert.Equal(Guid.Empty, result);
    }

    [Fact]
    public async Task ShortenUrlAsync_ShouldReturnShortUrl_WhenUnique()
    {
        var originalUrl = "https://google.com";
        
        var result = await _service.ShortenUrlAsync(originalUrl);
        
        Assert.NotEmpty(result);
    }

    [Fact]
    public async Task ShortenUrlAsync_ShouldReturnEmpty_WhenExists()
    {
        var entity = new ShortenUrl
        {
            ShortenedLink = "http://localhost:5130/abc"
        };

        _dbContext.ShortenedUrls.Add(entity);
        await _dbContext.SaveChangesAsync();
        
        var result = await _service.ShortenUrlAsync("https://google.com");
        
        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetShortenedUrlsAsync_ShouldReturnList()
    {
        var list = new List<ShortenUrl>
        {
            new ShortenUrl { Id = Guid.NewGuid() }
        };

        _dbContext.ShortenedUrls.AddRange(list);
        await _dbContext.SaveChangesAsync();

        _mapperMock
            .Setup(m => m.Map<List<GetShortenUrlSummaryDto>>(It.IsAny<List<ShortenUrl>>()))
            .Returns(new List<GetShortenUrlSummaryDto> { new() });

        var result = await _service.GetShortenedUrlsAsync();

        Assert.Single(result);
    }

    [Fact]
    public async Task GetShortenedUrlAsync_ShouldReturnDetail()
    {
        var id = Guid.NewGuid();

        var entity = new ShortenUrl
        {
            Id = id
        };

        _dbContext.ShortenedUrls.Add(entity);
        await _dbContext.SaveChangesAsync();

        _mapperMock
            .Setup(m => m.Map<GetShortenUrlDetailDto>(entity))
            .Returns(new GetShortenUrlDetailDto());
        
        var result = await _service.GetShortenedUrlAsync(id);

        Assert.NotNull(result);
    }

    [Fact]
    public async Task GetShortenedUrlByShortenedLinkAsync_ShouldReturnOriginalLink()
    {
        var entity = new ShortenUrl
        {
            OriginalLink = "https://google.com",
            ShortenedLink = "abc"
        };

        _dbContext.ShortenedUrls.Add(entity);
        await _dbContext.SaveChangesAsync();

        var result = await _service.GetShortenedUrlByShortenedLinkAsync("abc");

        Assert.Equal(entity.OriginalLink, result);
    }


    [Fact]
    public async Task DeleteShortenedUrlAsync_ShouldReturnTrue_WhenDeleted()
    {
        var entity = new ShortenUrl
        {
            Id = Guid.NewGuid()
        };

        _dbContext.ShortenedUrls.Add(entity);
        await _dbContext.SaveChangesAsync();
        
        var result = await _service.DeleteShortenedUrlAsync(entity.Id);

        Assert.True(result);
        Assert.Empty(_dbContext.ShortenedUrls);
    }

    [Fact]
    public async Task DeleteShortenedUrlAsync_ShouldReturnFalse_WhenNotFound()
    {
        var id = Guid.NewGuid();
        
        var result = await _service.DeleteShortenedUrlAsync(id);

        Assert.False(result);
    }
}

