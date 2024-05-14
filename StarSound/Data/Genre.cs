using System.ComponentModel.DataAnnotations;

namespace StarSound.Models;

public class Genre
{
    public int Id { get; set; }

    [Required(ErrorMessage = "The field must not be empty")]
    [StringLength(50, ErrorMessage = "The name cannot be longer than 50 characters.")]

    public string Name { get; set; }

    public byte[]? Image { get; set; }

    public virtual ICollection<Song> Songs { get; set; } = new List<Song>();

    //public Genre() { }

    //public Genre(int id, string name, byte[]? image, ICollection<Song> songs)
    //{
    //    Id = id;
    //    Name = name;
    //    Image = image;
    //    Songs = songs;
    //}

    //public override bool Equals(object obj)
    //{
    //    if (obj == null || GetType() != obj.GetType())
    //    {
    //        return false;
    //    }

    //    var other = obj as Genre;
    //    return Name == other.Name && Image == other.Image;
    //}
    //public override int GetHashCode()
    //{
    //    return (Name.GetHashCode() * 19) ^ (Image?.GetHashCode() ?? 0);
    //}

}
