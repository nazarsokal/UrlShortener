using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using NSubstitute;
using NSubstitute.ExceptionExtensions;
using UrlShortener.Application.DTOs.Url;
using UrlShortener.Application.Services;
using UrlShortener.DAL.Entities;
using UrlShortener.DAL.RepositoryAbstractions;

namespace UrlShortener.Tests;

public class ShortenUrlServiceTests
{
    private readonly IShortenUrlRepository _repository;
    private readonly IMapper _mapper;
    private readonly IValidator<ShortenUrl> _validator;
    private readonly ShortenUrlService _service;

    public ShortenUrlServiceTests()
    {
        _repository = Substitute.For<IShortenUrlRepository>();
        _mapper = Substitute.For<IMapper>();
        _validator = Substitute.For<IValidator<ShortenUrl>>();

        _service = new ShortenUrlService(_repository, _mapper, _validator);
    }

    [Fact]
    public async Task CreateShortenUrlAsync_ShouldCreateUrl()
    {
        var dto = new CreateShortenUrlDto
        {
            UrlOriginal = "https://youtube.com",
        };

        var entity = new ShortenUrl
        {
            Id = Guid.NewGuid(),
            UrlOriginal = dto.UrlOriginal,
            UrlShorten = "http://localhost:5017/abc123",
        };

        _mapper.Map<ShortenUrl>(dto).Returns(entity);

        _validator.ValidateAsync(entity)
            .Returns(new ValidationResult());

        var result = await _service.CreateShortenUrlAsync(dto);

        Assert.Equal(entity.Id, result);

        await _repository.Received(1).AddAsync(entity);
        await _repository.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task GetUrlSummariesAsync_ShouldReturnUrls()
    {
        var urls = new List<ShortenUrl>
        {
            new()
            {
                Id = Guid.NewGuid(),
                UrlOriginal = "https://youtube.com",
                UrlShorten = "http://localhost:5017/abc123",
            },
            new()
            {
                Id = Guid.NewGuid(),
                UrlOriginal = "https://google.com",
                UrlShorten = "http://localhost:5017/def456",
            },
        };

        var dtos = new List<UrlSummaryDto>
        {
            new()
            {
                Id = Guid.NewGuid(),
                UrlOriginal = "https://youtube.com",
                UrlShortened = "http://localhost:5017/abc123",
            },
            new()
            {
                Id = Guid.NewGuid(),
                UrlOriginal = "https://google.com",
                UrlShortened = "http://localhost:5017/def456",
            },
        };

        _repository.GetAllAsync().Returns(urls);
        _mapper.Map<IEnumerable<UrlSummaryDto>>(urls).Returns(dtos);

        var result = await _service.GetUrlSummariesAsync();

        Assert.Equal(2, result.Count());

        await _repository.Received(1).GetAllAsync();
    }

    [Fact]
    public async Task GetUrlDetailByIdAsync_ShouldReturnUrl()
    {
        var id = Guid.NewGuid();

        var entity = new ShortenUrl
        {
            Id = id,
            UrlOriginal = "https://youtube.com",
            UrlShorten = "http://localhost:5017/abc123",
        };

        var dto = new UrlDetailDto
        {
            Id = id,
            UrlOriginal = "https://youtube.com",
            CreatedByUser = "Some User",
        };

        _repository.GetUrlById(id).Returns(entity);
        _mapper.Map<UrlDetailDto>(entity).Returns(dto);

        var result = await _service.GetUrlDetailByIdAsync(id);

        Assert.Equal(id, result.Id);
    }

    [Fact]
    public async Task DeleteUrlAsync_ShouldDeleteUrl()
    {
        var id = Guid.NewGuid();

        await _service.DeleteUrlAsync(id);

        await _repository.Received(1).DeleteById(id);
        await _repository.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task GetUrlSummariesByUserIdAsync_ShouldReturnUserUrls()
    {
        var userId = Guid.NewGuid();

        var urls = new List<ShortenUrl>
        {
            new()
            {
                Id = Guid.NewGuid(),
                UrlOriginal = "https://youtube.com",
                UrlShorten = "http://localhost:5017/abc123",
                UserId = userId,
            },
        };

        var dtos = new List<UrlSummaryDto>
        {
            new()
            {
                Id = urls[0].Id,
                UrlOriginal = urls[0].UrlOriginal,
                UrlShortened = urls[0].UrlShorten,
            },
        };

        _repository.GetUrlsByUserId(userId).Returns(urls);
        _mapper.Map<IEnumerable<UrlSummaryDto>>(urls).Returns(dtos);

        var result = await _service.GetUrlSummariesByUserIdAsync(userId);

        Assert.Single(result);

        await _repository.Received(1).GetUrlsByUserId(userId);
    }

    [Fact]
    public async Task GetOriginalUrlByShortCodeAsync_ShouldReturnOriginalUrl_WhenUrlExists()
    {
        var shortCode = "abc123";

        var entity = new ShortenUrl
        {
            UrlOriginal = "https://youtube.com",
            UrlShorten = $"http://localhost:5017/{shortCode}",
        };

        _repository.GetByShortCodeAsync(entity.UrlShorten)
            .Returns(entity);

        var result = await _service.GetOriginalUrlByShortCodeAsync(shortCode);

        Assert.Equal("https://youtube.com", result);
    }

    [Fact]
    public async Task GetOriginalUrlByShortCodeAsync_ShouldReturnEmptyString_WhenUrlDoesNotExist()
    {
        var shortCode = "wrong-code";

        _repository.GetByShortCodeAsync($"http://localhost:5017/{shortCode}")
            .Returns((ShortenUrl?)null);

        var result = await _service.GetOriginalUrlByShortCodeAsync(shortCode);

        Assert.Equal(string.Empty, result);
    }
}