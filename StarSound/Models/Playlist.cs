using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace StarSound.Models;

public class Playlist
{
    public int Id { get; set; }

    public string Name { get; set; } //= null!;

    public byte[]? Image { get; set; }

    public int Duration { get; set; }

    public int CreationYear { get; set; }
    public string Description { get; set; }
    //public virtual ICollection<SongPlaylist> SongPlaylists { get; set; } = new List<SongPlaylist>();

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();



}
