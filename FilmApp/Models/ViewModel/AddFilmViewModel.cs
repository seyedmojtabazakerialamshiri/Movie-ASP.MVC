using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using FilmApp.Models.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FilmApp.Models.ViewModel
{
    /// <summary>
    /// AddFilmViewModel Viewmodel to be used in View
    /// </summary>
    public class AddFilmViewModel
    {
       
        [Required(ErrorMessage = "Required")]
        public int SelectedGenre { set; get; }
        public int OriginalSelectedGenre { set; get; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<SelectListItem> SelectedGenres { get; set; }
        public IEnumerable<SelectListItem> OriginalSelectedGenres { get; set; }
        [Required(ErrorMessage = "Required")]
        public IFormFile PostedFile { get; set; }
        public Film Film { get; set; }
    }
}
