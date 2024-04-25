namespace StarSound.Models;

public class Performer
{
    public int Id { get; set; }

    public string Name { get; set; } //= null!;

    public int Year { get; set; }

    public byte[]? Image { get; set; }

    public virtual ICollection<SongPerformer> SongPerformers { get; set; } = new List<SongPerformer>();

    public virtual ICollection<AlbumPerformer> AlbumPerformers { get; set; } = new List<AlbumPerformer>();


}
