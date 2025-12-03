namespace Models.DTOs.UserDTOs;

public class AuthResponse
{
    public required string Token { get; set; }
    public required ReturnUserDto ReturnUserDto { get; set; }
}