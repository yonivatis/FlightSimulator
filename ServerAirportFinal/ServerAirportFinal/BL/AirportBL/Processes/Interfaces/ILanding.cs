namespace ServerAirportFinal.BL.AirportBL
{
    public interface ILanding : IProcess, IGenerateFlight
    {
        void Land();
    }
}
