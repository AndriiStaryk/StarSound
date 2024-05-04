using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace StarSound.Models;

public class StarSoundContext : DbContext
{
    public virtual DbSet<Album> Albums { get; set; }

   // public virtual DbSet<AlbumPerformer> AlbumPerformers { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Performer> Performers { get; set; }

    public virtual DbSet<Playlist> Playlists { get; set; }

    public virtual DbSet<Song> Songs { get; set; }

    //public virtual DbSet<SongGenre> SongGenres { get; set; }

    //public virtual DbSet<SongPerformer> SongPerformers { get; set; }

    //public virtual DbSet<SongPlaylist> SongPlaylists { get; set; }


    public StarSoundContext(DbContextOptions<StarSoundContext> options) : base(options)
    {
        Database.EnsureCreated();
    }


//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.

//       => optionsBuilder.UseSqlServer("Server= DESKTOP-7KLUTEC\\SQLEXPRESS; Database=StarSound; Trusted_Connection=True; MultipleActiveResultSets=true");





}
