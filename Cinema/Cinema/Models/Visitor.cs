using System.ComponentModel.DataAnnotations;

namespace Cinema.Models
{
    public class Visitor
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public List<Ticket> Tickets { get; set; }
    }
}
