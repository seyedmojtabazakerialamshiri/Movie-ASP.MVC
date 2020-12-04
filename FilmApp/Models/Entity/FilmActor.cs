namespace FilmApp.Models.Entity
{
    /// <summary>
    /// FilmActor Entity to be used by EF Core for creating database
    /// </summary>
    public class FilmActor
    {
        public int Id { get; set; }
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
        public int FilmId { get; set; }
        public Film Film { get; set; }
        public int Deleted { get; set; }

    }
}
