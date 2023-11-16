using System.ComponentModel.DataAnnotations;

namespace Cinema.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        public List<MovieActor> MovieActors { get; set; }
        public string Poster { get; set; }
        public string Plot { get; set; }
        public double VisitorRating { get; set; }
        public DateTime ProjectionWeek { get; set; }
        public decimal Price { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
