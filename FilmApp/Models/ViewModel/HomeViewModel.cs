using System.Collections.Generic;
using System.Linq;
using FilmApp.Models.Entity;

namespace FilmApp.Models.ViewModel
{
    /// <summary>
    /// GenreViewModel Viewmodel to be used in View
    /// </summary>
    public class HomeViewModel
    {
        public IEnumerable<Film> FilmViewModels { get; set; }
        public IEnumerable<Genre> GenreViewModels { get; set; }
        public int SelectedGenre { get; set; }

    }
}
