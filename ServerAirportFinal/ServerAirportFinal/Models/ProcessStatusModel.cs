using System.ComponentModel.DataAnnotations;

namespace ServerAirportFinal.Models
{
    public class ProcessStatusModel
    {
        [Key]
        public int Index { get; set; }
        public int FlightNum { get; set; }
        public int AirplaneId { get; set; }
        public bool IsLanding { get; set; }
        public int StationNum { get; set; }
        public bool IsTaskDone { get; set; }
        public DateTime CreationTime { get; set; }
        //    public DateTime LastEnterTime { get; set; }
        public string Enters { get; set; }
    }
}
