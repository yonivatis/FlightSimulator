using ServerAirportFinal.BL.StationBL;

namespace ServerAirportFinal.BL.AirportBL
{
    public interface ILandingsManager
    {
        void Start();
        LandingRoute GetLandingRoute(List<IStationManager> stations);
    }
}
