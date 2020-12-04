using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmApp.Models.Entity
{
    /// <summary>
    /// Film Entity to be used by EF Core for creating database
    /// </summary>
    public class Film
    {
        public int FilmId { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Required")]
        public int Length { get; set; }
        public string Url { get; set; }
        [Required(ErrorMessage = "Required")]
        public string Description { get; set; }
        public virtual ICollection<FilmGenre> FilmGenres { get; set; }
        public virtual ICollection<FilmActor> FilmActors { get; set; }
        public int Deleted { get; set; }
    }
}
