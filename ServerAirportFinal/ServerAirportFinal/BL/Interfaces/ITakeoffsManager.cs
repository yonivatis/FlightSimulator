using ServerAirportFinal.BL.StationBL;

namespace ServerAirportFinal.BL.AirportBL
{
    public interface ITakeoffsManager
    {
        void Start();
        DepartureRoute GetDepartureRoute(List<IStationManager> stations);
    }
}
