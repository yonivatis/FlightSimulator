namespace ServerAirportFinal.BL.AirportBL
{
    public interface IProcess
    {
        ProcessStatusObject GetStatus(); //flight num, airplaneId, process, station, task status
        void TimeInStation();
    }
}
