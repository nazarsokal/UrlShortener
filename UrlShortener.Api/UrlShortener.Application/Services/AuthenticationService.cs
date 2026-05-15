using AutoMapper;
using FluentValidation;
using UrlShortener.Application.DTOs;
using UrlShortener.Application.Exceptions;
using UrlShortener.Application.Helpers;
using UrlShortener.Application.ServiceAbstractions;
using UrlShortener.DAL.Entities;
using UrlShortener.DAL.RepositoryAbstractions;

namespace UrlShortener.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository userRepository;
    private readonly IValidator<User> validator;
    private readonly JwtHelper jwtHelper;
    private readonly IMapper mapper;

    public AuthenticationService(IUserRepository userRepository, IMapper mapper, IValidator<User> validator, JwtHelper jwtHelper)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.validator = validator;
        this.jwtHelper = jwtHelper;
    }
    
    public async Task<AuthUserDto> RegisterUserAsync(CreateUserDto createUserDto)
    {
        var user = this.mapper.Map<User>(createUserDto);
        
        await this.validator.ValidateAndThrowAsync(user);
        
        await this.userRepository.AddAsync(user);
        await this.userRepository.SaveChangesAsync();

        return new AuthUserDto()
        {
            Id = user.Id,
            Username = user.Username,
            AccessToken = this.jwtHelper.Generate(user),
        };
    }

    public async Task<AuthUserDto> LoginUserAsync(LoginUserDto loginDto)
    {
        var foundUser = await this.userRepository.GetUserByUsernameAsync(loginDto.Username);

        if (foundUser == null)
        {
            throw new NotFoundException("Invalid username");
        }

        if (loginDto.Password != foundUser.Password)
        {
            throw new UnauthorizedAccessException("Invalid password");
        }
        
        return new AuthUserDto()
        {
            Id = foundUser.Id,
            Username = foundUser.Username,
            AccessToken = this.jwtHelper.Generate(foundUser),
        };
    }
}