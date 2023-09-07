using ServerAirportFinal.BL.StationBL;
using System.ComponentModel.DataAnnotations;

namespace ServerAirportFinal.Models
{
    public class StationModel
    {
        [Key]
        public int StationId { get; set; }
        public StationType Type { get; set; }
        public bool IsFree { get; set; }
    }
}
