using ServerAirportFinal.BL.AirplaneBL;
using ServerAirportFinal.Models;

namespace ServerAirportFinal.BL.StationBL
{
    public interface IStationManager
    {
        bool isAvailable();
        int GetStationId();
        StationType GetTypeOfStation();
        void EnterToStation(AirplaneObject airplane);
        void ExitFromStation();
        AirplaneObject GetAirplane();
        StationModel GetStation();
    }
}
