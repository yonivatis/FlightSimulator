namespace TakingOffSimulator
{
    public interface ISimulatorB<T>
    {
        void GenerateTakingOff(List<T> processes);
    }
}