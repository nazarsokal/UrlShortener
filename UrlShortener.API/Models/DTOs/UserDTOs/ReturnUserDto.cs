namespace Models.DTOs.UserDTOs;

public class ReturnUserDto
{
    public Guid Id { get; set; }
    
    public string UserName { get; set; }
    
    public string Email { get; set; }
    
    public IList<string> Roles { get; set; }
}