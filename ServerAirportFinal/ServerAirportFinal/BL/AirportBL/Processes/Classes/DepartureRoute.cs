using ServerAirportFinal.BL.StationBL;

namespace ServerAirportFinal.BL.AirportBL
{
    public class DepartureRoute : IRoute
    {
        const int RUNWAY = 4;
        const int TRANSPORTATION = 8;
        const int STATION6 = 6;
        const int STATION7 = 7;
        //   const int EXIT_AIRPORT = 9;
        public List<IStationManager> Route { get; }
        public DepartureRoute(List<IStationManager> airportStations) => Route = airportStations;

        public bool IsNextStationFree(int Loaction)
        {
            if (Loaction == STATION6 || Loaction == STATION7) return Route[7].isAvailable(); //stations[7]=station 8
            else if (Loaction == TRANSPORTATION) return Route[3].isAvailable(); //stations[3]=station 4

            return true; //plane is on the runway, no need to continue- finish the process
        }

        public int GetNextStation(int Location) => (Location == STATION6 || Location == STATION7) ? TRANSPORTATION : RUNWAY;

        public void ProceedToNextStaion(IProcess process)
        {
            TakingOff takeOffProcess = (TakingOff)process;

            int currentLocation = takeOffProcess.TakingOffFlight.Airplane.StationId;
            if (currentLocation == RUNWAY)
            {
                takeOffProcess.TakingOffFlight.IsProcessDone = true;
                DateTime date = DateTime.Now;
                takeOffProcess.TakingOffFlight.TimeProcessDone = new DateTime(date.Year, date.Month, date.Day, date.Hour,
                    date.Minute, date.Second);

                Route[3].ExitFromStation();

                return;
            }

            int newLocation = GetNextStation(currentLocation);
            Route[currentLocation - 1].ExitFromStation();
            Route[newLocation - 1].EnterToStation(takeOffProcess.TakingOffFlight.Airplane);
            takeOffProcess.TakeOff();
        }
    }
}
