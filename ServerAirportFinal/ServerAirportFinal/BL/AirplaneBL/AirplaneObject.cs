using ServerAirportFinal.Models;

namespace ServerAirportFinal.BL.AirplaneBL
{
    public class AirplaneObject : AirplaneModel, IAirplane
    {
        const double TIME_IN_STATION = 2.5;

        public static int nextId = 1;
        public DateTime TimeEnterToStation { get; set; }


        public bool IsTaskDone => !default(DateTime).Equals(TimeEnterToStation) &&
                   DateTime.Now.Subtract(TimeEnterToStation) >= TimeSpan.FromSeconds(TIME_IN_STATION);



        public AirplaneObject()
        {
            AirplaneId = nextId++;
            DateTime date = DateTime.Now;
            CreatingTime = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
        }

        public AirplaneObject(int airplaneId, int stationId, DateTime creationTime) //for regenerating
        {
            AirplaneId = airplaneId;
            StationId = stationId;
            CreatingTime = creationTime;
        }
        public void OnProceeding() => TimeEnterToStation = DateTime.Now;

    }
}
