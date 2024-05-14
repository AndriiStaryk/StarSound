using System.ComponentModel.DataAnnotations;

namespace StarSound.Models;

public class Performer
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The field must not be empty")]
    [StringLength(50, ErrorMessage = "The name cannot be longer than 50 characters.")]
    public string Name { get; set; } = null!;

    public int Year { get; set; }

    public byte[]? Image { get; set; }

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();
    public virtual ICollection<Album> Albums { get; set; } = new List<Album>();
}
