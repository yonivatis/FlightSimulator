using ServerAirportFinal.BL.AirplaneBL;
using System.Diagnostics;
using TakingOffSimulator;

namespace ServerAirportFinal.BL.AirportBL
{
    public class CreateTakingOff : ISimulatorB<IProcess>
    {
        public bool ProcessesIsConverted { get; set; }
        public DepartureRoute DepartureRoute { get; set; }

        public CreateTakingOff(DepartureRoute departureRoute)
        {
            DepartureRoute = departureRoute;
            ProcessesIsConverted = false;
        }

        public void GenerateTakingOff(List<IProcess> processes)
        {
            Trace.WriteLine("try to create take off");//check
            AirplaneObject airplaneForDeparture = GetAirplane(processes);
            Trace.WriteLine($"{(airplaneForDeparture == null ? "no plane for take off" : $"airplane {airplaneForDeparture.AirplaneId} is ready to take off")}"); //check
            if (airplaneForDeparture == null) return;

            AddTakingOff(processes, airplaneForDeparture);
        }
        private AirplaneObject GetAirplane(List<IProcess> processes)
        {
            var landingsFinished = processes.FindAll(p => p is Landing && ((Landing)p).LandingFlight.IsProcessDone);

            if (landingsFinished.Count == 0) return null; //no planes are parking or no planes are ready

            else if (landingsFinished.Count == 1) return ((Landing)landingsFinished[0]).LandingFlight.Airplane;

            return GetOldestPlane(landingsFinished); //priority for the the plane that exists more time          
        }
        private AirplaneObject GetOldestPlane(List<IProcess> landingsFinished)
        {
            AirplaneObject oldestPlane = ((Landing)landingsFinished[0]).LandingFlight.Airplane;
            for (int i = 1; i < landingsFinished.Count; i++)
            {
                AirplaneObject landingAirplane = ((Landing)landingsFinished[i]).LandingFlight.Airplane;
                if (landingAirplane.CreatingTime.CompareTo(oldestPlane.CreatingTime) < 0) oldestPlane = landingAirplane;
            }

            return oldestPlane;
        }
        private void AddTakingOff(List<IProcess> processes, AirplaneObject airplane) //not adding to the list- convert it
        {
            var landingFinished = processes.Find(p => p is Landing && ((Landing)p).LandingFlight.Airplane == airplane);
            if (landingFinished == null) return; //for safety (unlikely to happen)

            int indexToConvert = processes.IndexOf(landingFinished);
            processes[indexToConvert] = new TakingOff(airplane, DepartureRoute);
            ProcessesIsConverted = true;
        }

        public void RegenerateTakingOff(List<IProcess> processes, int airplaneId, int stationId, int flightNum, DateTime creationTime)
        {
            AirplaneObject airplane = new AirplaneObject(airplaneId, stationId, creationTime);
            FlightObject TakingOffFlight = new FlightObject(airplane, flightNum, false);
            processes.Add(new TakingOff(TakingOffFlight, DepartureRoute));
        }
    }
}
