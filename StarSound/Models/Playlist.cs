using Microsoft.AspNetCore.Mvc.ViewEngines;
using System.ComponentModel.DataAnnotations;

namespace StarSound.Models;

public class Playlist
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The field must not be empty")]
    [StringLength(50, ErrorMessage = "The name cannot be longer than 50 characters.")]
    public string Name { get; set; } = null!;

    public byte[]? Image { get; set; }

    [Range(1, 7200, ErrorMessage = "Duration must be greater than 0")]
    public int Duration { get; set; }

    [Range(1200, 2024, ErrorMessage = "Release year must be valid.")]
    public int CreationYear { get; set; }

    [Required(ErrorMessage = "The field must not be empty")]
    [StringLength(256, ErrorMessage = "The name cannot be longer than 256 characters.")]
    public string Description { get; set; } = null!;
    public virtual ICollection<SongPlaylist> SongPlaylists { get; set; } = new List<SongPlaylist>();



}
