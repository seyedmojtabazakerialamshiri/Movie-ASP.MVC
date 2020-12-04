using System.Collections.Generic;
using FilmApp.Models.Entity;

namespace FilmApp.Models.ViewModel
{
    /// <summary>
    /// ActorViewModel Viewmodel to be used in View
    /// </summary>
    public class ActorViewModel
    {
        public IEnumerable<Actor> ActorViewModels { get; set; }
    }
}
