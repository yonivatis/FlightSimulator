using Microsoft.EntityFrameworkCore;
using ServerAirportFinal.BL.StationBL;
using ServerAirportFinal.Data;
using ServerAirportFinal.Hubs;
using ServerAirportFinal.Models;
using System.Diagnostics;
using System.Timers;
using Timer = System.Timers.Timer;

namespace ServerAirportFinal.BL.AirportBL
{
    public class Airport : IAirport, IDisposable
    {
        bool _started = false;
        IControlTower _controlTower;
        System.Threading.SemaphoreSlim SemaphoreSaveUI;
        System.Threading.SemaphoreSlim SemaphoreSaveFlights;
        System.Threading.SemaphoreSlim SemaphoreHistory;
        AirportContext _context;

        private readonly INotificationService<AirportImage> _notificationService;
        private readonly object lockObj = new object();

        public Timer _timer { get; set; }
        public IStationManager[] StationManagers { get; set; }

        public Airport(IDbContextFactory<AirportContext> contextFactory,
            INotificationService<AirportImage> notificationService)
        {
            SemaphoreSaveUI = new System.Threading.SemaphoreSlim(1, 1);
            SemaphoreSaveFlights = new System.Threading.SemaphoreSlim(1, 1);
            SemaphoreHistory = new System.Threading.SemaphoreSlim(1, 1);
            _context = contextFactory.CreateDbContext();
            _notificationService = notificationService;
        }

        private async Task Save()
        {
            await SemaphoreSaveUI.WaitAsync();
            await _context.AddAsync(GetAirportImage());
            await _context.SaveChangesAsync();
            SemaphoreSaveUI.Release();
        }


        private void UpdateStationsStatus()
        {
            StationManagers = new IStationManager[_context.Stations.Count()];

            foreach (StationModel stationIndex in _context.Stations)
                StationManagers[stationIndex.StationId - 1] = new StationManager(stationIndex);
        }//all station will be initiated to be free at first. if they are not (because db show something else,
         //the processes will updated them

        public void StartAsync() //activate airport
        {
            if (_started)
            {
                Trace.WriteLine($"==>{GetType().Name} already started");
                return;
            }

            _started = true;
            Init();
            _timer = new Timer(1000);

            _timer.Elapsed += TimerElapsed;
            _timer.Start();
            _controlTower.Start();
        }

        private  void TimerElapsed(object sender, ElapsedEventArgs e) =>
             Task.Run(_controlTower.CheckFlightsAsync);

        private async void CheckUI()
        {
            Trace.WriteLine($"can update??? {_controlTower.ItertaionIsOver() && _controlTower.UpdateStations()}");
            if (_controlTower.ItertaionIsOver() && _controlTower.UpdateStations()) await Save();
        }

        private void Init()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();
            UpdateStationsStatus();
            if (_context.AirportImages.Count() == 0) _controlTower = new ControlTower(StationManagers.ToList()); //no history
            else
            {
                _controlTower = new ControlTower(StationManagers.ToList(),
              _context.AirportImages.Include(a => a.Processes).OrderByDescending(a => a.ImageId).FirstOrDefault()); //last image is the most updated one
            }

            _controlTower.OnChanged += (s, e) => CheckUI();
            _controlTower.OnStationStateChanged += (s, station) => UpdateDbContext(station);
            _controlTower.OnAddFlight += (s, flight) => AddProcess(flight);
        }

        private async void AddProcess(FlightObject flight)
        {
            await SemaphoreSaveFlights.WaitAsync();
            await _context.Flights.AddAsync(flight);
            SemaphoreSaveFlights.Release();
        }

        private async void UpdateDbContext(StationModel station)
        {
            lock (lockObj)
            {
                _context.Update(station);
            }
            await _notificationService.Notify(GetAirportImage());
        }

        //tables with landing & departures details (list of current processes)
        public AirportImage GetAirportImage() => //shoud I use semaphre/lock?
            _started ? new AirportImageObject(_controlTower.GetAllStatuses(), _controlTower.PrioriryStatus()) : null;

        public void Dispose() => _context.Dispose();

        public string GetMsgStatus() => $"Airport is {(_started ? "activated" : "inactivated")}"; //notify?

        public async Task<List<FlightModel>> GetHistoryData()
        {
            await SemaphoreHistory.WaitAsync();
            var history = await _context.Flights.CountAsync() > 0 ? await _context.Flights.ToListAsync() : null;
            await _notificationService.NotifyHistory(history);
            SemaphoreHistory.Release();
            return history;
        }


        public async Task<AirportImage> GetCurrentProcesses() =>    //notify?  can collape when I ask manually  
             await _context.AirportImages.OrderByDescending(a => a.ImageId).FirstOrDefaultAsync();
    }
}
