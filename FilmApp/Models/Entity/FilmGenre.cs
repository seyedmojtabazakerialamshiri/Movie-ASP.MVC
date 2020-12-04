namespace FilmApp.Models.Entity
{

    /// <summary>
    /// FilmGenre Entity to be used by EF Core for creating database
    /// </summary>
    public class FilmGenre
    {
        public int Id { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public int Deleted { get; set; }

    }
}
