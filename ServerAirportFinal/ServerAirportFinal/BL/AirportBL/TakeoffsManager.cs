using ServerAirportFinal.BL.StationBL;
using System.Diagnostics;

namespace ServerAirportFinal.BL.AirportBL
{
    public class TakeoffsManager : ITakeoffsManager
    {
        bool _started = false;

        public CreateTakingOff TakingOffSimulator { get; set; }
        public DepartureRoute departureRoute { get; set; }
        public TakeoffsManager(List<IProcess> processes, List<IStationManager> stations)
        {
            departureRoute = GetDepartureRoute(stations);
            TakingOffSimulator = new CreateTakingOff(departureRoute);
        }


        public DepartureRoute GetDepartureRoute(List<IStationManager> stations) => new DepartureRoute(stations);

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

        internal void AllowTakingOffContinue(List<IProcess> processes, TakingOff takingOffProcess) =>
            departureRoute.ProceedToNextStaion(takingOffProcess);
    }
}
