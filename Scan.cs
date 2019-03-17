using System;
using System.Collections.Generic;
using System.Linq;

namespace SOLab2
{
    public class Scan : ISimulation<Request>
    {
        public void Simulate(List<Request> requests)
        {
            var requestsToTake = new List<Request>(requests);
            var requestsList = new List<Request>();
            int currentBlock = Program.StartingBlock;
            bool movingRight = true;
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
                currentBlock += movingRight ? 1 : -1;
                if (currentBlock <= Program.MinBlock || currentBlock >= Program.MaxBlock) movingRight = !movingRight;
            }
            if(requests.Any(r => !r.IsCompleted)) throw new Exception("Scan simulation failed");
        }
    }
}