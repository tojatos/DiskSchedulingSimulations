using System.Collections.Generic;

namespace SOLab2
{
    public interface ISimulation<T>
    {
        void Simulate(List<T> items);
    }
}