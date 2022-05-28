using System.ComponentModel.DataAnnotations;

namespace VideoHolder.Data.Entities;

public class Account
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(64)]
    public string Login { get; set; }

    [Required]
    [MaxLength(2048)]
    public string PasswordHash { get; set; }
}