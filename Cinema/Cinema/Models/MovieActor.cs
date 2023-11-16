using System.ComponentModel.DataAnnotations;

namespace Cinema.Models
{
    public class MovieActor
    {
        [Key]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
    }
}
