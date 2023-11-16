using System.ComponentModel.DataAnnotations;

namespace Cinema.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public int VisitorId { get; set; }
        public Visitor Visitor { get; set; }
        public int PlaceId { get; set; }
        public Place Place { get; set; }
        public decimal Price { get; set; }
    }
}
