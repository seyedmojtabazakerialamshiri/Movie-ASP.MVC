using FilmApp.Models.Entity;

namespace FilmApp.Repository
{
    /// <summary>
    /// Interface for Unit Of Work
    /// </summary>
    public interface IUnitOfWork
    {
        Repository<Film> FilmRepository { get; }
        Repository<Genre> GenreRepository { get; }
        Repository<Actor> ActorRepository { get; }
        Repository<FilmGenre> FilmGenreRepository { get; }
        Repository<FilmActor> FilmActorRepository { get; }
        void Commit();
    }
}
