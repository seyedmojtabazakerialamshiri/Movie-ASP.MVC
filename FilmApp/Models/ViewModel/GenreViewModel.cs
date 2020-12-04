using System.Collections.Generic;
using FilmApp.Models.Entity;

namespace FilmApp.Models.ViewModel
{
    /// <summary>
    /// GenreViewModel Viewmodel to be used in View
    /// </summary>
    public class GenreViewModel
    {
        public IEnumerable<Genre> GenreViewModels { get; set; }
    }
}
