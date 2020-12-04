using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FilmApp.Models.Entity
{
    /// <summary>
    /// Actor Entity to be used by EF Core for creating database
    /// </summary>
    public class Actor
    {
        public int ActorId { get; set; }
        [Required(ErrorMessage = "Required")]
        public string ActorFirstName { get; set; }
        [Required(ErrorMessage = "Required")]
        public string ActorLastName { get; set; }
        public virtual ICollection<FilmActor> FilmActors { get; set; }
        public int Deleted { get; set; }
    }
}
