using System.ComponentModel.DataAnnotations;

namespace Cinema.Models
{
    public class Place
    {
        [Key]
        public int Id { get; set; }
        public string Number { get; set; }
        public decimal Price { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
