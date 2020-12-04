using Microsoft.EntityFrameworkCore;

namespace FilmApp.Models.Entity
{
    /// <summary>
    /// Database Context of application
    /// </summary>
    public class FilmDbContext : DbContext
    {
        public FilmDbContext(DbContextOptions<FilmDbContext> options) : base(options)
        { }

        public DbSet<Film> Films { get; set; }
        public DbSet<Actor> Actors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<FilmGenre> FilmGenres { get; set; }
        public DbSet<FilmActor> FilmActors { get; set; }

    }

    
}
