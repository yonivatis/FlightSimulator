using ServerAirportFinal.BL.AirplaneBL;
using ServerAirportFinal.BL.StationBL;
using System.Diagnostics;

namespace ServerAirportFinal.BL.AirportBL
{
    public class Landing : ILanding
    {
        bool isRegenerated;
        int _taskNum;
        List<TimeSpan> TasksTime;
        LandingRoute _landingRoute;

        public FlightObject LandingFlight { get; set; }
        public List<DateTime> AllTimes { get; set; }


        public Landing(AirplaneObject airplane, LandingRoute landingStations)
        {
            isRegenerated = false;
            _landingRoute = landingStations;
            LandingFlight = GenerateFlight(airplane);
            _landingRoute.Route[0].EnterToStation(airplane);
            _taskNum = 0;
            TasksTime = new List<TimeSpan>();
            Land(); //start the process                  
        }
        public Landing(FlightObject Flight, LandingRoute landingStations)//regenerated flight //isTaskDone is changed from unknown reason
        {
            isRegenerated = true;
            LandingFlight = Flight; //flight id missing,
            _landingRoute = landingStations;
            TasksTime = new List<TimeSpan>();
            _landingRoute.Route[LandingFlight.Airplane.StationId - 1].EnterToStation(LandingFlight.Airplane);
            _taskNum = (int)_landingRoute.Route.SingleOrDefault(s => s.GetAirplane() != null &&
            s.GetAirplane().AirplaneId == Flight.Airplane.AirplaneId).GetTypeOfStation();
            Land();
        }


        private void LandProcess() //run through all stations and start their task
        {
            IStationManager currentStation = _landingRoute.Route.Find(s => s.GetAirplane() == LandingFlight.Airplane);

            Trace.WriteLine($"landing Flight number {LandingFlight.DemoId} which holds airplane {LandingFlight.AirplaneId}," +
                $" current details: enter to station- {currentStation.GetTypeOfStation()}, start task {_taskNum}");

            if (currentStation.GetTypeOfStation() == StationType.Load) Trace.WriteLine($"Flight number " +
                $"{LandingFlight.DemoId} which holds airplane {LandingFlight.AirplaneId} has just finished its land process...");
        }

        public void Land()
        {
            if (_taskNum == 0 || isRegenerated)
            {
                AllTimes = new List<DateTime>();
                isRegenerated = false;
            }
            else if (!isRegenerated) TimeInStation();

            LandingFlight.Airplane.OnProceeding(); //give the order to move- change its stationId       
            AllTimes.Add(LandingFlight.Airplane.TimeEnterToStation);
            _taskNum++;

            LandProcess();
        }

        public ProcessStatusObject GetStatus() => new ProcessStatusObject(LandingFlight.DemoId, LandingFlight.AirplaneId, true,
                 LandingFlight.Airplane.StationId, LandingFlight.Airplane.IsTaskDone, LandingFlight.Airplane.CreatingTime, AllTimes);


        public FlightObject GenerateFlight(AirplaneObject airplane) => new FlightObject(airplane, true);

        public void TimeInStation() //test
        {
            TimeSpan interval = DateTime.Now.Subtract(LandingFlight.Airplane.TimeEnterToStation);
            if (interval.TotalSeconds > StationManager.MAX_TIME_IN_STATION) throw new Exception("Too much time in station...");
            TasksTime.Add(interval); //for printing
        }
    }
}
