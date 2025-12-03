namespace Models.DTOs.UserDTOs;

public class LoginUserDto
{
    public required string UserName { get; set; }

    public required string Email { get; set; }

    public required string Password { get; set; }
}