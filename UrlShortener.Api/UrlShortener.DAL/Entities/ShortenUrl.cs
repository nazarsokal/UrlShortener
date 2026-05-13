using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UrlShortener.DAL.Entities;

public class ShortenUrl
{
    [Key]
    public Guid Id { get; set; }

    public required string UrlOriginal { get; set; }

    public required string UrlShorten { get; set; }
    
    public string? Description { get; set; }

    public DateTime DateCreated { get; set; }
    
    public User UserCreatedBy { get; set; }
    
    public Guid UserId { get; set; }
}