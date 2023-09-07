using ServerAirportFinal.BL.StationBL;

namespace ServerAirportFinal.BL.AirportBL
{
    public class LandingRoute : IRoute
    {
        const int STATION6 = 6;
        const int STATION7 = 7;
        public List<IStationManager> Route { get; }
        public LandingRoute(List<IStationManager> airportStations) => Route = airportStations;

        public bool IsNextStationFree(int Loaction)
        {
            if (Loaction <= 4) return Route[Loaction].isAvailable(); //stations[Loaction]=location+1
            else if (Loaction == 5) return Route[5].isAvailable() || Route[6].isAvailable(); //location 6,7

            return true; //6,7 no need to continue- finish the process
        }

        public int GetNextStation(int Loaction)
        {
            if (Loaction <= 4) Loaction++;
            else
            {
                if (Route[5].isAvailable()) Loaction = STATION6;
                else if (Route[6].isAvailable()) Loaction = STATION7;
            }

            return Loaction;
        }

        public void ProceedToNextStaion(IProcess process)
        {
            Landing landingProcess = (Landing)process;

            int currentLocation = landingProcess.LandingFlight.Airplane.StationId;
            if (currentLocation == STATION6 || currentLocation == STATION7)
            {
                landingProcess.LandingFlight.IsProcessDone = true; //all todo list is done
                DateTime date = DateTime.Now;
                landingProcess.LandingFlight.TimeProcessDone = new DateTime(date.Year, date.Month, date.Day, date.Hour,
                    date.Minute, date.Second);
                return;
            }
            int newLocation = GetNextStation(currentLocation);
            Route[currentLocation - 1].ExitFromStation(); //  stations[currentLocation - 1] = currentLocation
            Route[newLocation - 1].EnterToStation(landingProcess.LandingFlight.Airplane);
            landingProcess.Land();
        }
    }
}
