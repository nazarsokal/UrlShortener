using FluentValidation;
using UrlShortener.DAL.Entities;

namespace UrlShortener.Application.Validators;

public class UrlValidator : AbstractValidator<ShortenUrl>
{
    public UrlValidator()
    {
        RuleFor(x => x.UrlOriginal)
            .NotEmpty()
            .MaximumLength(2000);

        RuleFor(x => x.UrlShorten)
            .NotEmpty()
            .MaximumLength(50);

        RuleFor(x => x.UserId)
            .NotEmpty();

        RuleFor(x => x.Description)
            .MaximumLength(500);
    }
}