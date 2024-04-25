namespace StarSound.Models;

public class SongPlaylist
{
    public int Id { get; set; }

    public int SongId { get; set; }

    public int PlaylistId { get; set; }

    public virtual Song Song { get; set; }

    public virtual Playlist Playlist { get; set; }
}
