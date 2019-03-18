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
        private const int TestSeries = 100;
        private const int RequestsPerSimulation = 50;
        private static readonly Random Random = new Random();
        public static void Main()
        {
            var fcfsTimes = new List<double>();
            var sstfTimes = new List<double>();
            var scanTimes = new List<double>();
            var cscanTimes = new List<double>();
            var edfTimes = new List<double>();
            try
            {
                var fcfs = new Fcfs();
                var sstf = new Sstf();
                var scan = new Scan();
                var cscan = new Cscan();
                var edf = new Edf();
                for (int i = 0; i < TestSeries; ++i)
                {
                    List<Request> requests = GenerateRequests(RequestsPerSimulation);
                    
                    fcfs.Simulate(requests);
                    fcfsTimes.Add(GetAverageWaitingTime(requests));
                    //Print(requests.OrderBy(r => r.ArrivalTime).ToList());
                    Reset(requests);
                    
                    sstf.Simulate(requests);
                    sstfTimes.Add(GetAverageWaitingTime(requests));
                    //Print(requests.OrderBy(r => r.CompletionTime).ToList());
                    Reset(requests);
                    
                    scan.Simulate(requests);
                    scanTimes.Add(GetAverageWaitingTime(requests));
                    //Print(requests.OrderBy(r => r.CompletionTime).ToList());
                    Reset(requests);
                    
                    cscan.Simulate(requests);
                    cscanTimes.Add(GetAverageWaitingTime(requests));
                    //Print(requests.OrderBy(r => r.CompletionTime).ToList());
                    Reset(requests);
                    
                    cscan.Simulate(requests);
                    cscanTimes.Add(GetAverageWaitingTime(requests));
                    //Print(requests.OrderBy(r => r.CompletionTime).ToList());
                    Reset(requests);
                    
                    edf.Simulate(requests);
                    edfTimes.Add(GetAverageWaitingTime(requests));
                    //Print(requests.OrderBy(r => r.CompletionTime).ToList());
                    Reset(requests);
                }
                Console.WriteLine($"FCFS average waiting time: {fcfsTimes.Average()}");
                Console.WriteLine($"SSTF average waiting time: {sstfTimes.Average()}");
                Console.WriteLine($"SCAN average waiting time: {scanTimes.Average()}");
                Console.WriteLine($"CSCAN average waiting time: {cscanTimes.Average()}");
                Console.WriteLine($"EDF average waiting time: {edfTimes.Average()}");

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
                    new Request(IdGenerator.GetNext(), Random.Next(MinBlock, MaxBlock + 1), Random.Next(1, 10000), Random.Next(1, 10000)))
                .ToList();

        private static void Reset(List<Request> requests) => requests.ForEach(r => r.Reset());
        private static void Print(List<Request> requests)
        {
            const string formatString = "|{0,3}|{1,5}|{2,7}|{3,10}|{4,7}";
            
            Console.WriteLine(formatString, "Id", "Block", "Arrival", "Completion", "Waiting");
            requests.ForEach(p => Console.WriteLine(formatString, p.Id, p.Block, p.ArrivalTime, p.CompletionTime, p.WaitingTime));
        } 
    }
}
