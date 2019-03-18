using System;
using System.Collections.Generic;
using System.Linq;

namespace SOLab2
{
    public class FdScan : ISimulation<Request>
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
                if(!requestsList.Any()) continue;
                requests.FindAll(r => r.Block == currentBlock).ForEach(r => r.Complete(clock));
                requestsList.FindAll(r => r.TimeUntilDeadline(clock) < 0).ForEach(r =>
                {
                    r.Complete(clock, false);
                    requestsList.Remove(r);
                });
                if(!requestsList.Any(r => r.CanMeetDeadline(clock, currentBlock))) continue;
                //move pointer
                currentBlock += currentBlock - requestsList
                                    .FindAll(r => r.CanMeetDeadline(clock, currentBlock))
                                    .OrderBy(r => r.TimeUntilDeadline(clock))
                                    .First().Block > 0 ? -1 : 1;
            }
            if(requests.Any(r => !r.IsCompleted)) throw new Exception("FD-SCAN simulation failed");
        }
    }
}