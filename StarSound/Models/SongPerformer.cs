namespace StarSound.Models;

public class SongPerformer
{
    public int Id { get; set; }

    public int SongId { get; set; }

    public int PerformerId { get; set; }

    public virtual Song Song { get; set; }

    public  virtual Performer Performer { get; set; }
}
