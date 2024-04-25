namespace StarSound.Models;

public class Song
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    //public int PerformerId { get; set; }

    //public int GenreId { get; set; }

    public int AlbumId { get; set; }

    public int Duration { get; set; }

    public bool IsFavorite { get; set; }

    public bool IsExplicit { get; set; }

    public byte[]? Image { get; set; }

    public int ReleaseYear { get; set; }

    public string? Lyrics { get; set; }

    //public virtual Performer? Performer { get; set; }

    //public virtual Genre? Genre { get; set; }

    public virtual Album Album { get; set; }

    public virtual ICollection<SongGenre> SongGenres { get; set; } = new List<SongGenre>();

    public virtual ICollection<SongPerformer> SongPerformers { get; set; } = new List<SongPerformer>();


    public virtual ICollection<SongPlaylist> SongPlaylists { get; set; } = new List<SongPlaylist>();

}
