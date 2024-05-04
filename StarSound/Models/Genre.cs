namespace StarSound.Models;

public class Genre
{
    public int Id { get; set; }
    public string Name { get; set; }

    public byte[]? Image { get; set; }

    //public virtual ICollection<SongGenre> SongGenres { get; set; } = new List<SongGenre>();
    
    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();


}
