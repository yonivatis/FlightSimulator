using Microsoft.AspNetCore.SignalR;
using ServerAirportFinal.Models;

namespace ServerAirportFinal.Hubs
{
    public class NotificationAdapter : INotificationService<AirportImage>
    {
        private readonly IHubContext<AirportNotifications, INotificationClient> _hub;
        public NotificationAdapter(IHubContext<AirportNotifications, INotificationClient> hub) => _hub = hub;

        public Task Notify(AirportImage args) => _hub.Clients.All.ReceiveAirportImage(args);

        public Task NotifyHistory(List<FlightModel> history) => _hub.Clients.All.ShowHistory(history.ToList());
    }
}
