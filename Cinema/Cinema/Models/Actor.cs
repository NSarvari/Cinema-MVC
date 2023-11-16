using System.ComponentModel.DataAnnotations;

namespace Cinema.Models
{
    public class Actor
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string Nationality { get; set; }
        public double ImdbRating { get; set; }
        public string Photo { get; set; }
        public List<MovieActor> MovieActors { get; set; }
    }
}
