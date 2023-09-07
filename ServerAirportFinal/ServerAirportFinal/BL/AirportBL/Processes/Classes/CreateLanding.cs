using LandingSimulator;
using ServerAirportFinal.BL.AirplaneBL;
using System.Diagnostics;

namespace ServerAirportFinal.BL.AirportBL
{
    public class CreateLading : ISimulatorA<IProcess>
    {
        public LandingRoute LandingRoute { get; set; }
        public CreateLading(LandingRoute landingRoute) => LandingRoute = landingRoute;

        public void GenerateLanding(List<IProcess> processes)
        {
            Trace.WriteLine($"try to create landing. station status: {LandingRoute.Route[0].isAvailable()}");//check
            if (LandingRoute.Route[0].isAvailable())
            {
                AirplaneObject newAirplane = new AirplaneObject();
                AddLanding(processes, newAirplane);
                Trace.WriteLine($"new landing is generated"); //check
                LandingRoute.Route[0].EnterToStation(newAirplane);
            }
        }
        private void AddLanding(List<IProcess> processes, AirplaneObject airplane) =>
          processes.Add(new Landing(airplane, LandingRoute));

        public void RegenerateLanding(List<IProcess> processes, int airplaneId, int stationId, int flightNum, DateTime creationTime)
        {
            AirplaneObject airplane = new AirplaneObject(airplaneId, stationId, creationTime); //time enter to station will be now problem here!!!!
            FlightObject landingFlight = new FlightObject(airplane, flightNum, true); //how to init flight id (primary key)
            processes.Add(new Landing(landingFlight, LandingRoute));
        }
    }
}
