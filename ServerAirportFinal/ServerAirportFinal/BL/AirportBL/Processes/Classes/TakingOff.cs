using ServerAirportFinal.BL.AirplaneBL;
using ServerAirportFinal.BL.StationBL;
using System.Diagnostics;

namespace ServerAirportFinal.BL.AirportBL
{
    public class TakingOff : ITakingOff
    {
        bool isRegenerated;
        int _taskNum;
        DepartureRoute _takingOffRoute;
        List<TimeSpan> TasksTime;

        public FlightObject TakingOffFlight { get; set; }
        public List<DateTime> AllTimes { get; set; }
        public TakingOff(AirplaneObject airplane, DepartureRoute takeOffStations)
        {
            isRegenerated = false;
            _takingOffRoute = takeOffStations;
            TakingOffFlight = GenerateFlight(airplane);

            _taskNum = 0;
            TasksTime = new List<TimeSpan>();
            TakeOff(); //start the process
        }

        public TakingOff(FlightObject takingOffFlight, DepartureRoute departureRoute)
        {
            isRegenerated = true;
            TakingOffFlight = takingOffFlight;
            _takingOffRoute = departureRoute;
            TasksTime = new List<TimeSpan>();
            _takingOffRoute.Route[TakingOffFlight.Airplane.StationId - 1].EnterToStation(TakingOffFlight.Airplane);
            switch (takingOffFlight.Airplane.StationId)
            {
                case 6:
                case 7:
                    _taskNum = 0;
                    break;
                case 8:
                    _taskNum = 1;
                    break;
                default:
                    _taskNum = 2;
                    break;
            }
            TakeOff();
        }

        private void TakeOffProcess()
        {
            IStationManager currentStation = _takingOffRoute.Route
                .Find(s => s.GetStationId() == TakingOffFlight.Airplane.StationId);

            Trace.WriteLine($"departure Flight number {TakingOffFlight.DemoId}, which holds airplane {TakingOffFlight.AirplaneId}" +
                $" current details: enter to station- {currentStation.GetTypeOfStation()}, start task {_taskNum}");

            if (currentStation.GetTypeOfStation() == StationType.Runway) Trace.WriteLine($"Flight number " +
                $"{TakingOffFlight.DemoId} which holds airplane {TakingOffFlight.AirplaneId} has just finished its departure" +
                $" process...");
        }

        public void TakeOff()
        {
            if (_taskNum == 0 || isRegenerated)
            {
                AllTimes = new List<DateTime>();
                isRegenerated = false;
            }
            else if (!isRegenerated) TimeInStation();

            TakingOffFlight.Airplane.OnProceeding(); //give the order to move
            AllTimes.Add(TakingOffFlight.Airplane.TimeEnterToStation);
            _taskNum++; //index of next task

            TakeOffProcess();
        }
        public ProcessStatusObject GetStatus() => new ProcessStatusObject(TakingOffFlight.DemoId, TakingOffFlight.AirplaneId, false,
              TakingOffFlight.Airplane.StationId, TakingOffFlight.Airplane.IsTaskDone, TakingOffFlight.Airplane.CreatingTime, AllTimes);

        public FlightObject GenerateFlight(AirplaneObject airplane) => new FlightObject(airplane, false);

        public void TimeInStation()
        {
            TimeSpan interval = DateTime.Now.Subtract(TakingOffFlight.Airplane.TimeEnterToStation);
            if (interval.TotalSeconds > StationManager.MAX_TIME_IN_STATION) throw new Exception("Too much time in station...");
            TasksTime.Add(interval);
        }
    }
}
