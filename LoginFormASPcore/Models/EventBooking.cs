using System.ComponentModel.DataAnnotations;

namespace LoginFormASPcore.Models
{
    public class EventBooking
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string EventName { get; set; } = null!;

        [Required]
        public DateTime EventDate { get; set; }

        public string BookedBy { get; set; } = null!; // Stores the logged-in user's email


    }


    }
