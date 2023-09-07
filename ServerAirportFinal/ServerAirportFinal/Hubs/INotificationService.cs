using ServerAirportFinal.Models;

namespace ServerAirportFinal.Hubs
{
    public interface INotificationService<T>
    {
        Task Notify(T args); //is the notify suitable for everything

        Task NotifyHistory(List<FlightModel> history);
    }
}
