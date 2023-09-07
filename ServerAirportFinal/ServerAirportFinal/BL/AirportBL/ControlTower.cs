using ServerAirportFinal.BL.StationBL;
using ServerAirportFinal.Models;
using System.Diagnostics;

namespace ServerAirportFinal.BL.AirportBL
{
    public class ControlTower : IControlTower
    {
        bool _started;
        bool _iterationDone;
        bool _updateDb;
        bool _historyIncluded;
        List<IStationManager> _stations;

        public event EventHandler OnChanged;
        public event EventHandler<FlightObject> OnAddFlight;
        public event EventHandler<StationModel> OnStationStateChanged;

        public bool DeparturePriority { get; set; }
        public bool UIChange { get; private set; }
        public LandingsManager _landingsManager { get; set; }
        public TakeoffsManager _takeoffsManager { get; set; }

        //process can be landing or taking off (all of those are flights)
        public List<IProcess> _processes { get; set; }
        public ControlTower()
        {
            _started = false;
            _iterationDone = false;
            _updateDb = false;
            _processes = new List<IProcess>();
        }
       
        public ControlTower(List<IStationManager> stations, bool departurePriority = false) : this()
        {
            bool firstTime = true;
            _stations = stations;
            foreach (var station in _stations)
            {
                if (!station.isAvailable())
                {
                    firstTime = false;
                    break;
                }
            }
            if (firstTime)
            {
                _historyIncluded = false;
                DeparturePriority = departurePriority;
                Init(true);
            }
        }
        public ControlTower(List<IStationManager> stations, AirportImage airportImage) :
        this(stations) => Init(false, airportImage);

        private void Init(bool FirstTime, AirportImage? airportImage = null) //consider to do pass the airport image in the init
        {
            InstantiateManagers();
            if (!FirstTime) //fill processes list
            {
                DeparturePriority = airportImage.DeparturePriority;
                _historyIncluded = true;
                foreach (ProcessStatusModel processStatusModel in airportImage.Processes.OrderBy(p => p.CreationTime))
                {
                    if (processStatusModel.IsLanding) _landingsManager.LandingSimulator.RegenerateLanding(_processes,
                        processStatusModel.AirplaneId, processStatusModel.StationNum, processStatusModel.FlightNum,
                        processStatusModel.CreationTime);

                    else _takeoffsManager.TakingOffSimulator.RegenerateTakingOff(_processes,
                        processStatusModel.AirplaneId, processStatusModel.StationNum, processStatusModel.FlightNum,
                        processStatusModel.CreationTime);
                } //sort processes in a way that departure will be the first (by creation time)
            }
        }

        void InstantiateManagers()
        {
            _landingsManager = new LandingsManager(_processes, _stations);
            _takeoffsManager = new TakeoffsManager(_processes, _stations);
        }

        //run on the processes list & check every plane if it cans continue its process
        public void CheckFlightsAsync()
        {
            _updateDb = _iterationDone = UIChange = false;
            CreateProcesses();


            Trace.WriteLine("check processes");
            for (int i = 0; i < _processes.Count; i++) //changed from start to end
            {
                if (_processes[i] is Landing)
                {
                    Landing landingProcess = (Landing)_processes[i];

                    Trace.WriteLine($"check landing flight {landingProcess.LandingFlight.DemoId}");
                    if (landingProcess.LandingFlight.Airplane.IsTaskDone &&
                        _landingsManager.LandingRoute.IsNextStationFree(landingProcess.LandingFlight.Airplane.StationId))
                    {
                        bool isApproach = _stations.Find(s => s.GetTypeOfStation() == StationType.Approach &&
                               s.GetAirplane() == landingProcess.LandingFlight.Airplane) != null;

                        if (isApproach && DeparturePriority) DeparturePriority = false;//next time the departure will be blocked

                        else if (isApproach && !DeparturePriority)
                            if (_historyIncluded) continue;

                        _landingsManager.AllowLandingContinue(_processes, landingProcess);

                        UpdateDBStation(landingProcess.LandingFlight.Airplane.StationId);
                        if (landingProcess.LandingFlight.IsProcessDone)
                        {
                            AddFlight(landingProcess.LandingFlight);
                            _updateDb = true;
                        }

                        if (_takeoffsManager.TakingOffSimulator.ProcessesIsConverted)
                        {
                            _takeoffsManager.TakingOffSimulator.ProcessesIsConverted = false;
                            break; //the conversion is dangerous during iterating. block iteration
                        }
                    }
                    continue;
                }

                TakingOff takingOffProcess = (TakingOff)_processes[i];
                Trace.WriteLine($"check departure flight {takingOffProcess.TakingOffFlight.DemoId}");
                bool readyForDeparture = _stations.Find(s => s.GetTypeOfStation() == StationType.Transportaion &&
                          s.GetAirplane() == takingOffProcess.TakingOffFlight.Airplane) != null; //plane in station 8

                if (takingOffProcess.TakingOffFlight.Airplane.IsTaskDone &&
                    _takeoffsManager.departureRoute.IsNextStationFree(takingOffProcess.TakingOffFlight.Airplane.StationId))
                {
                    if (readyForDeparture && !DeparturePriority) DeparturePriority = true;
                    else if (readyForDeparture && DeparturePriority) //DeparturePriority was already given
                    {
                        if (!_historyIncluded) continue;
                        _historyIncluded = false;
                        continue;
                    }

                    _takeoffsManager.AllowTakingOffContinue(_processes, takingOffProcess);
                    UpdateDBStation(takingOffProcess.TakingOffFlight.Airplane.StationId);

                    if (takingOffProcess.TakingOffFlight.IsProcessDone)
                    {
                        AddFlight(takingOffProcess.TakingOffFlight);
                        _updateDb = true;
                        _processes.Remove(takingOffProcess); //avoiding itertate while changing the list

                        break;
                    }

                    if (_takeoffsManager.TakingOffSimulator.ProcessesIsConverted)
                    {
                        _takeoffsManager.TakingOffSimulator.ProcessesIsConverted = false;
                        break; //the conversion is dangerous during iterating. block iteration
                    }
                }
            }
            _iterationDone = true;
            if (_updateDb) RaiseOnChanged();   //save changes   
        }

        private void CreateProcesses()
        {
            _landingsManager.LandingSimulator.GenerateLanding(_processes);
            _takeoffsManager.TakingOffSimulator.GenerateTakingOff(_processes);
        }


        private void UpdateDBStation(int stationId)
        {
            UIChange = true;
            RaiseOnStationStateChanged(_stations[stationId - 1].GetStation());
        }

        private void AddFlight(FlightObject flight) => RaiseOnAdd(flight);


        public void Start()
        {
            if (_started)
            {
                Trace.WriteLine($"==>{GetType().Name} already started");
                return;
            }

            _started = true;
            Trace.WriteLine($"==>{GetType().Name} is starting");
            _landingsManager.Start();
            _takeoffsManager.Start();
        }

        public List<ProcessStatusModel> GetAllStatuses()
        {
            List<ProcessStatusModel> ProcessStatusModels = new List<ProcessStatusModel>();
            _processes.ForEach(p => { ProcessStatusModels.Add(p.GetStatus()); });
            return ProcessStatusModels;
        }


        public bool UpdateStations() => UIChange;

        public bool PrioriryStatus() => DeparturePriority;

        public bool ItertaionIsOver() => _iterationDone;
        private void RaiseOnAdd(FlightObject flight) => OnAddFlight?.Invoke(this, flight);

        private void RaiseOnChanged() =>
            OnChanged?.Invoke(this, new EventArgs());
        private void RaiseOnStationStateChanged(StationModel stationModel) =>
        OnStationStateChanged?.Invoke(this, stationModel);
    }
}
