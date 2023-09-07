using ServerAirportFinal.Models;

namespace ServerAirportFinal.BL.AirportBL
{
    public interface IControlTower
    {
        event EventHandler OnChanged;
        event EventHandler<StationModel> OnStationStateChanged;
        event EventHandler<FlightObject> OnAddFlight;
        void Start();
        void CheckFlightsAsync();
        bool UpdateStations();
        List<ProcessStatusModel> GetAllStatuses(); //of flights    
        bool PrioriryStatus();
        bool ItertaionIsOver();
    }
}
