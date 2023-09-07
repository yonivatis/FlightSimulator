using ServerAirportFinal.Models;

namespace ServerAirportFinal.BL.AirportBL
{
    public interface IAirport
    {
        string GetMsgStatus();
        void StartAsync();
        AirportImage GetAirportImage();
        Task<AirportImage> GetCurrentProcesses();
        Task<List<FlightModel>> GetHistoryData();
    }
}
