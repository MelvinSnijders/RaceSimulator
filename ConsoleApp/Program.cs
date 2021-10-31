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
            Data.CurrentRace.DriversChanged += NewVisualizer.DriversChangedListener;

            Console.WriteLine(Data.CurrentRace.RaceTrack.Name);

            //Visualizer.DrawTrack(Data.CurrentRace, Data.CurrentRace.RaceTrack);
            
            new NewVisualizer(Data.CurrentRace.RaceTrack).DrawTrack();

            for (; ; )
            {
                Thread.Sleep(100);
            }

        }

    }
}
