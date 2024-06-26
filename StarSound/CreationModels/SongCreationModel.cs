﻿using System.ComponentModel.DataAnnotations;

namespace StarSound.CreationModels;


public class SongCreationModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The field must not be empty")]
    [StringLength(50, ErrorMessage = "The name cannot be longer than 50 characters.")]
    public string Name { get; set; } = null!;

    public int AlbumId { get; set; }

    [Range(1, 1800, ErrorMessage = "Duration must be greater than 0")]
    public int Duration { get; set; }

    public bool IsFavorite { get; set; }

    public bool IsExplicit { get; set; }

    public byte[]? Image { get; set; }

    [Range(1200, 2024, ErrorMessage = "Release year must be valid.")]
    public int ReleaseYear { get; set; }

    public string? Lyrics { get; set; }
}
