using ServerAirportFinal.BL.AirplaneBL;
using ServerAirportFinal.Models;

namespace ServerAirportFinal.BL.AirportBL
{
    public class FlightObject : FlightModel
    {
        public static int nextId = 100;
        public int DemoId { get; set; } //just for printing in the server (avoid using the primary key)
        public AirplaneObject Airplane { get; set; }
        public bool IsProcessDone { get; set; } //the whole process

        public FlightObject(AirplaneObject airplane, bool isLanding)
        {
            DemoId = nextId++;
            Airplane = airplane;
            AirplaneId = Airplane.AirplaneId;
            IsLanding = isLanding;
            IsProcessDone = false;
        }
        public FlightObject(AirplaneObject airplane, int flightNum, bool isLanding)  //for regenerating
        {
            DemoId = flightNum;
            Airplane = airplane;
            AirplaneId = Airplane.AirplaneId;
            IsLanding = isLanding;
        }
    }
}
