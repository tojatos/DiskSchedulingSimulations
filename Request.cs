using System;

namespace SOLab2
{
    public class Request
    {
        public readonly int Id;
        public readonly int ArrivalTime;
        public readonly int Block;
        public readonly int Deadline;
        
        public int CompletionTime { get; private set; }
        public bool WasAbleToComplete { get; private set; }
        public int WaitingTime => CompletionTime - ArrivalTime;
        public bool IsCompleted => CompletionTime != 0;
        public int SeekTime(int currentBlock) => Math.Abs(currentBlock - Block);
        public int TimeUntilDeadline(int currentTime) => ArrivalTime + Deadline - currentTime;
        public bool CanMeetDeadline(int currentTime, int currentBlock)
            => TimeUntilDeadline(currentTime) - SeekTime(currentBlock) > 0;

        public Request(int id, int block, int arrivalTime, int deadline)
        {
            Id = id;
            ArrivalTime = arrivalTime;
            Block = block;
            Deadline = deadline;
        }

        public void Complete(int time, bool wasAbleToComplete = true)
        {
            CompletionTime = time;
            WasAbleToComplete = wasAbleToComplete;
        }

        public void Reset() => CompletionTime = 0;
    }
}