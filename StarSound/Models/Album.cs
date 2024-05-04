namespace StarSound.Models;

public class Album
{
    public int Id { get; set; }

    public string Name { get; set; } //= null!;

    public byte[]? Image { get; set; }

    public int Duration { get; set; }

    public int ReleaseYear { get; set; }

    public string Description { get; set; }

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();

    //public virtual ICollection<AlbumPerformer> AlbumPerformers { get; set; } = new List<AlbumPerformer>();
    public virtual ICollection<Performer> Performers { get; set; } = new List<Performer>();

}
