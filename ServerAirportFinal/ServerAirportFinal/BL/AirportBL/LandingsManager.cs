using ServerAirportFinal.BL.StationBL;
using System.Diagnostics;

namespace ServerAirportFinal.BL.AirportBL
{
    //get a plane (from simulator) and do some tasks with it
    public class LandingsManager : ILandingsManager
    {
        bool _started = false;

        public CreateLading LandingSimulator { get; set; }
        public LandingRoute LandingRoute { get; set; }

        public LandingsManager(List<IProcess> processes, List<IStationManager> stations)
        {
            LandingRoute = GetLandingRoute(stations);
            LandingSimulator = new CreateLading(LandingRoute);
        }

        public LandingRoute GetLandingRoute(List<IStationManager> stations) => new LandingRoute(stations);

        public void Start()
        {
            if (_started)
            {
                Trace.WriteLine($"==>{GetType().Name} already started");
                return;
            }

            _started = true;
            Trace.WriteLine($"==>{GetType().Name} is starting");
        }

        internal void AllowLandingContinue(List<IProcess> processes, Landing landingProcess) =>
          LandingRoute.ProceedToNextStaion(landingProcess);
    }
}
