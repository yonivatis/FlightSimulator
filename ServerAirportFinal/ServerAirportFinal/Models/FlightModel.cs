using System.ComponentModel.DataAnnotations;

namespace ServerAirportFinal.Models
{
    public class FlightModel //the flight is an event entity
    {
        [Key]
        [Range(100, 100000000)]
        public int FlightId { get; set; }
        public bool IsLanding { get; set; } // true=landing/false=taking off
        public int AirplaneId { get; set; }
        public DateTime TimeProcessDone { get; set; }
    }
}
