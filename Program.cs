using System;
using System.Collections.Generic;
using System.Linq;

namespace SOLab2
{
    internal static class Program
    {
        public const int ClockTreshold = 1000000;
        public const int MinBlock = 1;
        public const int MaxBlock = 10000;
        public const int StartingBlock = MaxBlock / 2;
        private const int TestSeries = 1;
        private const int RequestsPerSimulation = 10;
        private static readonly Random Random = new Random();
        public static void Main()
        {
            try
            {
                var fcfs = new Fcfs();
                for (int i = 0; i < TestSeries; ++i)
                {
                    List<Request> requests = GenerateRequests(RequestsPerSimulation);
                    
                    fcfs.Simulate(requests);
                    Print(requests.OrderBy(r => r.ArrivalTime).ToList());
                    Reset(requests);
                }

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private static double GetAverageWaitingTime(List<Request> requests)
            => (double) requests.Select(r => r.WaitingTime).Sum() / requests.Count;

        private static List<Request> GenerateRequests(int numberOfRequests)
            => Enumerable.Range(0, numberOfRequests).Select(t =>
                    new Request(IdGenerator.GetNext(), Random.Next(MinBlock, MaxBlock + 1), Random.Next(1, 10000)))
                .ToList();

        private static void Reset(List<Request> requests) => requests.ForEach(r => r.Reset());
        private static void Print(List<Request> requests)
        {
            const string formatString = "|{0,2}|{1,5}|{2,7}|{3,7}";
            
            Console.WriteLine(formatString, "Id", "Block", "Arrival", "Waiting");
            requests.ForEach(p => Console.WriteLine(formatString, p.Id, p.Block, p.ArrivalTime, p.WaitingTime));
            
        } 
    }
}