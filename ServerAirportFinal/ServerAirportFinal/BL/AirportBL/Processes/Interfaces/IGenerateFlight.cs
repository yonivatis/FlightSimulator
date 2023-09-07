using ServerAirportFinal.BL.AirplaneBL;

namespace ServerAirportFinal.BL.AirportBL
{
    public interface IGenerateFlight
    {
        FlightObject GenerateFlight(AirplaneObject airplane);
    }
}
