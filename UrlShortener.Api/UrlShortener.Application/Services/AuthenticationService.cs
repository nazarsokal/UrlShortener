using AutoMapper;
using FluentValidation;
using UrlShortener.Application.DTOs;
using UrlShortener.Application.Exceptions;
using UrlShortener.Application.ServiceAbstractions;
using UrlShortener.DAL.Entities;
using UrlShortener.DAL.RepositoryAbstractions;

namespace UrlShortener.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository userRepository;
    private readonly IValidator<User> validator;
    private readonly IMapper mapper;

    public AuthenticationService(IUserRepository userRepository, IMapper mapper, IValidator<User> validator)
    {
        this.userRepository = userRepository;
        this.mapper = mapper;
        this.validator = validator;
    }
    
    public async Task<Guid> RegisterUserAsync(CreateUserDto createUserDto)
    {
        var user = this.mapper.Map<User>(createUserDto);
        
        await this.validator.ValidateAndThrowAsync(user);
        
        await this.userRepository.AddAsync(user);
        await this.userRepository.SaveChangesAsync();
        
        return user.Id;
    }

    public async Task<GetUserSummaryDto> LoginUserAsync(LoginUserDto loginDto)
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
        
        return this.mapper.Map<GetUserSummaryDto>(foundUser);
    }
}