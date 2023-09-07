using System.ComponentModel.DataAnnotations;

namespace ServerAirportFinal.Models
{
    public class AirportImage
    {
        [Key]
        public int ImageId { get; set; }
        public List<ProcessStatusModel> Processes { get; set; }
        public DateTime ImageTime { get; set; }
        public bool DeparturePriority { get; set; }
    }
}
