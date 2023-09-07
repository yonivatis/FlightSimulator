using ServerAirportFinal.BL.StationBL;

namespace ServerAirportFinal.BL.AirportBL
{
    public interface IRoute
    {
        List<IStationManager> Route { get; }
        bool IsNextStationFree(int Loaction);
        int GetNextStation(int Loaction);
        void ProceedToNextStaion(IProcess process);
    }
}
