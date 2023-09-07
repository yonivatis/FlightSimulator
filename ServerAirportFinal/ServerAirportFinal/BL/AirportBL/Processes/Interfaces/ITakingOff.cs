namespace ServerAirportFinal.BL.AirportBL
{
    public interface ITakingOff : IProcess, IGenerateFlight
    {
        void TakeOff();
    }
}
