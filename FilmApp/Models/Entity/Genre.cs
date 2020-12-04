using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmApp.Models.Entity
{
    /// <summary>
    /// Genre Entity to be used by EF Core for creating database
    /// </summary>
    public class Genre
    {
        public int GenreId { get; set; }
        [Required(ErrorMessage = "Required")]
        public string GenreName { get; set; }
        public virtual ICollection<FilmGenre> FilmGenres { get; set; }
        public int Deleted { get; set; }
    }
}
