using FilmApp.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmApp.Configuration
{
    /// <summary>
    /// Class : FilmGenre as an intermediate table for relationship between Genre and Film table ( Many to Many )
    /// </summary>
    public class FilmGenreConfiguration : IEntityTypeConfiguration<FilmGenre>
    {
        /// <summary>
        /// Method :  Defining relationship between Genre and Film table ( Many to Many ) by using FilmGenre as an intermediate table
        /// </summary>
        /// <param name="builder">EntityTypeBuilder</param>
        public void Configure(EntityTypeBuilder<FilmGenre> builder)
        {
            builder.HasKey(bc => new { bc.FilmId, bc.GenreId });

            builder.HasOne(a => a.Film)
                .WithMany(m => m.FilmGenres)
                .HasForeignKey(a => a.FilmId);

            builder.HasOne(a=>a.Genre)
                .WithMany(m=>m.FilmGenres)
                .HasForeignKey(a => a.GenreId);


        }
    }
}
