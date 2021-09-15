using System;
using System.Threading;
using Controller;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Data.Initialize();
            Data.NextRace();
            Console.WriteLine(Data.CurrentRace.RaceTrack.Name);

            for (; ; )
            {
                Thread.Sleep(100);
            }

        }
    }
}
