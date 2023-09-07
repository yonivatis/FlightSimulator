using ServerAirportFinal.Models;

namespace ServerAirportFinal.Hubs
{
    public interface INotificationClient
    {
        Task ReceiveAirportImage(AirportImage airportImage);
        Task ActivationMsg(string msgStatus);
        Task ShowHistory(List<FlightModel> flights); //print all db flights history
    }
}
