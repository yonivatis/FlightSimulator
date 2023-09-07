using ServerAirportFinal.Models;

namespace ServerAirportFinal.BL.AirportBL
{
    public class ProcessStatusObject : ProcessStatusModel
    {
        public ProcessStatusObject(int flightNum, int airplaneId, bool isLanding, int stationNum, bool isTaskDone,
           DateTime creationTime, List<DateTime>? timeEnters = null)
        {
            FlightNum = flightNum;
            AirplaneId = airplaneId;
            IsLanding = isLanding;
            StationNum = stationNum;
            IsTaskDone = isTaskDone;
            CreationTime = creationTime;

            string format = "hh:mm:ss";
            Enters = string.Join(", ", timeEnters.Select(d => d.ToString(format)));
        }
    }
}
