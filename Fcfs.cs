using System.Collections.Generic;
using System.Linq;

namespace SOLab2
{
    public class Fcfs : ISimulation<Request>
    {
        public void Simulate(List<Request> requests)
        {
            var requestsToTake = new List<Request>(requests);
            var requestsQueue = new Queue<Request>();
            int currentBlock = Program.StartingBlock;
            for (int clock = 0; requests.Any(r => !r.IsCompleted) && clock < Program.ClockTreshold; ++clock)
            {
                requestsToTake.FindAll(r => r.ArrivalTime == clock).ForEach(r =>
                {
                    requestsToTake.Remove(r);
                    requestsQueue.Enqueue(r);
                });
                while (requestsQueue.Any())
                {
                    Request request = requestsQueue.Peek();
                    if (request.Block == currentBlock)
                    {
                        request.Complete(clock);
                        requestsQueue.Dequeue();
                    }
                    else break;
                }
                if(!requestsQueue.Any()) continue;
                //move pointer
                currentBlock += currentBlock - requestsQueue.Peek().Block > 0 ? -1 : 1;
            }
        }
    }
}