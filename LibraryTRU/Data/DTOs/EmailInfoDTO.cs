using System.ComponentModel.DataAnnotations;


namespace LibraryTRU.Data.DTOs;

public class EmailInfoDTO
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    public required string Subject { get; set; }

    [Required]
    public required string Message { get; set; }

    [Required]
    public required string QrHash { get; set; }
}