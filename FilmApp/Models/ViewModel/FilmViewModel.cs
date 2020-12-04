using System.Collections.Generic;
using FilmApp.Models.Entity;

namespace FilmApp.Models.ViewModel
{
    /// <summary>
    /// FilmViewModel Viewmodel to be used in View
    /// </summary>
    public class FilmViewModel
    {
        public IEnumerable<Film> FilmViewModels { get; set; }
    }
}
