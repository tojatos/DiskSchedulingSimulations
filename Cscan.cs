using System;
using System.Collections.Generic;
using System.Linq;

namespace SOLab2
{
    public class Cscan : ISimulation<Request>
    {
        public void Simulate(List<Request> requests)
        {
            var requestsToTake = new List<Request>(requests);
            var requestsList = new List<Request>();
            int currentBlock = Program.StartingBlock;
            for (int clock = 0; requests.Any(r => !r.IsCompleted) && clock < Program.ClockTreshold; ++clock)
            {
                requestsToTake.FindAll(r => r.ArrivalTime == clock).ForEach(r =>
                {
                    requestsToTake.Remove(r);
                    requestsList.Add(r);
                });
                requestsList.FindAll(r => r.Block == currentBlock).ForEach(r => r.Complete(clock));
                requestsList.RemoveAll(r => r.Block == currentBlock);
                //move pointer
                currentBlock += 1;
                if (currentBlock > Program.MaxBlock) currentBlock = Program.MinBlock;
            }
            if(requests.Any(r => !r.IsCompleted)) throw new Exception("CSCAN simulation failed");
        }
    }
}