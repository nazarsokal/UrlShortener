using FluentValidation;
using UrlShortener.DAL.Entities;

namespace UrlShortener.Application.Validators;

public class UrlValidator : AbstractValidator<ShortenUrl>
{
    public UrlValidator()
    {
        RuleFor(x => x.UrlOriginal)
            .NotEmpty()
            .MaximumLength(2000)
            .Must(BeValidUrl)
            .WithMessage("UrlOriginal must be a valid URL.");

        RuleFor(x => x.UrlShorten)
            .NotEmpty()
            .MaximumLength(50)
            .Must(BeValidUrl)
            .WithMessage("UrlShorten must be a valid URL.");

        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }

    private static bool BeValidUrl(string? url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out var result)
               && (result.Scheme == Uri.UriSchemeHttp
                   || result.Scheme == Uri.UriSchemeHttps);
    }
}