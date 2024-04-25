namespace StarSound.Models;

public class AlbumPerformer
{
    public int Id { get; set; }

    public int AlbumId { get; set; }

    public int PerformerId { get; set; }

    public virtual Album Album { get; set; }

    public virtual Performer Performer { get; set; }
}
