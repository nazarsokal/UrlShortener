using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public class ShortenUrl
{
    [Key]
    [Column("UrlId")]
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public string OriginalLink { get; set; }
    
    public string ShortenedLink { get; set; }
    
    public DateTime CreatedAt { get; set; }
    
    public Guid UserIdCreatedBy { get; set; }
    
    public ApplicationUser ApplicationUserCreated { get; set; }
}