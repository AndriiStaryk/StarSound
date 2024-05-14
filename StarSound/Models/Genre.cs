using System.ComponentModel.DataAnnotations;

namespace StarSound.Models;

public class Genre
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The field must not be empty")]
    [StringLength(50, ErrorMessage = "The name cannot be longer than 50 characters.")]

    public string Name { get; set; }

    public byte[]? Image { get; set; }

    public virtual ICollection<SongGenre> SongGenres { get; set; } = new List<SongGenre>();

}
