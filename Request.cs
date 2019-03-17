namespace SOLab2
{
    public class Request
    {
        public readonly int Id;
        public readonly int ArrivalTime;
        public readonly int Block;
        
        public int CompletionTime { get; private set; }
        public int WaitingTime => CompletionTime - ArrivalTime;
        public bool IsCompleted => CompletionTime != 0;

        public Request(int id, int block, int arrivalTime)
        {
            Id = id;
            ArrivalTime = arrivalTime;
            Block = block;
        }

        public void Complete(int time) => CompletionTime = time;
        public void Reset() => CompletionTime = 0;
    }
}