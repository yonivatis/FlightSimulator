using Microsoft.AspNetCore.SignalR;
using ServerAirportFinal.BL.AirportBL;

namespace ServerAirportFinal.Hubs
{
    public class AirportNotifications : Hub<INotificationClient>
    {
        IAirport _airport;
        public AirportNotifications(IAirport airport) => _airport = airport;
    }
}
