using System.ComponentModel.DataAnnotations;

namespace VideoHolder.Data.Entities.Abstract;

public abstract class Item
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(1024)]
    public string Url { get; set; }

    [Required]
    [MaxLength(128)]
    public string Name { get; set; }

    [MaxLength(1024)]
    public string Description { get; set; }

    [Required]
    public DateTime PostDateTime { get; set; } = DateTime.Now;
}