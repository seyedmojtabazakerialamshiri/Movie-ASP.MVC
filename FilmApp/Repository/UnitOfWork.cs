using FilmApp.Models.Entity;

namespace FilmApp.Repository
{
    /// <summary>
    /// Unit Of Work implementation
    /// </summary>
    public class UnitOfWork : IUnitOfWork

    {
        private readonly FilmDbContext _databaseContext;

        public UnitOfWork(FilmDbContext databaseContext)
        {
            _databaseContext = databaseContext;
        }


        private Repository<Film> RepositoryFilm { get; set; }
        private Repository<Genre> RepositoryGenre { get; set; }
        private Repository<Actor> RepositoryActor { get; set; }
        private Repository<FilmGenre> RepositoryFilmGenre { get; set; }
        private Repository<FilmActor> RepositoryFilmActor { get; set; }

        public Repository<Film> FilmRepository => RepositoryFilm ??= new Repository<Film>(_databaseContext);
        public Repository<Genre> GenreRepository => RepositoryGenre ??= new Repository<Genre>(_databaseContext);
        public Repository<Actor> ActorRepository => RepositoryActor ??= new Repository<Actor>(_databaseContext);
        public Repository<FilmGenre> FilmGenreRepository => RepositoryFilmGenre ??= new Repository<FilmGenre>(_databaseContext);
        public Repository<FilmActor> FilmActorRepository => RepositoryFilmActor ??= new Repository<FilmActor>(_databaseContext);


        public void Commit()
        {
            _databaseContext.SaveChanges();
        }

    }
}
