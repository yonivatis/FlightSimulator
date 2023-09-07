using ServerAirportFinal.BL.AirplaneBL;
using ServerAirportFinal.Models;
using System.Diagnostics;

namespace ServerAirportFinal.BL.StationBL
{
    public class StationManager : IStationManager
    {
        public const int MAX_TIME_IN_STATION = 30;
        public StationModel Station { get; }
        public AirplaneObject? Airplane { get; set; }

        public StationManager(StationModel station, AirplaneObject? airplane = null)
        {
            Station = station;
            Station.IsFree = true; //even if it's not the first time, init to true (after, the processes will update thier status)
            Airplane = airplane;
        }

        public void EnterToStation(AirplaneObject airplane)
        {
            Station.IsFree = false;
            Airplane = airplane;
            Airplane.StationId = Station.StationId; //redundant
            Trace.WriteLine($"plane {airplane.AirplaneId} has entered to staion number {Station.StationId}");
        }

        public void ExitFromStation()
        {
            Station.IsFree = true;
            Airplane = null;
        }

        public int GetStationId() => Station.StationId;

        public StationType GetTypeOfStation() => Station.Type;

        public bool isAvailable() => Station.IsFree;

        public AirplaneObject GetAirplane() => Airplane;

        public StationModel GetStation() => Station;
    }
}
