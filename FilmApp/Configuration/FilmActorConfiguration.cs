using FilmApp.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FilmApp.Configuration
{
    /// <summary>
    /// Class : FilmActor as an intermediate table for relationship between Actor and Film table ( Many to Many )
    /// </summary>
    public class FilmActorConfiguration : IEntityTypeConfiguration<FilmActor>
    {
        /// <summary>
        /// Method :  Defining relationship between Actor and Film table ( Many to Many ) by using FilmActor as an intermediate table
        /// </summary>
        /// <param name="builder">EntityTypeBuilder</param>
        public void Configure(EntityTypeBuilder<FilmActor> builder)
        {
            builder.HasKey(bc => new { bc.FilmId, bc.ActorId });

            builder.HasOne(a => a.Film)
                .WithMany(m => m.FilmActors)
                .HasForeignKey(a => a.FilmId);

            builder.HasOne(a => a.Actor)
                .WithMany(m => m.FilmActors)
                .HasForeignKey(a => a.ActorId);
        }
    }
}
