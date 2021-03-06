using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
    public static class Data
    {
        public static Competition Competition;
        public static Race CurrentRace;

        public static void Initialize()
        {
            Competition = new Competition();
            AddTracks();
            AddParticipants();

        }

        public static void NextRace()
        {
            if(Competition.NextTrack() != null)
            {
                CurrentRace = new Race(Competition.Tracks.Dequeue(), Competition.Participants);
            }
        }

        public static void AddTracks()
        {
            SectionTypes[] zandvoortSections = { 
                SectionTypes.StartGrid, 
                SectionTypes.StartGrid, 
                SectionTypes.Straight, 
                SectionTypes.RightCorner, 
                SectionTypes.RightCorner, 
                SectionTypes.Straight, 
                SectionTypes.Straight, 
                SectionTypes.Finish };
            Track zandvoortTrack = new Track("Zandvoort", zandvoortSections);

            SectionTypes[] redBullRingSections = { 
                SectionTypes.StartGrid, 
                SectionTypes.StartGrid, 
                SectionTypes.Straight, 
                SectionTypes.RightCorner,
                SectionTypes.RightCorner, 
                SectionTypes.Straight, 
                SectionTypes.LeftCorner, 
                SectionTypes.Straight, 
                SectionTypes.RightCorner, 
                SectionTypes.Straight, 
                SectionTypes.Straight, 
                SectionTypes.Straight,
                SectionTypes.RightCorner, 
                SectionTypes.Straight, 
                SectionTypes.RightCorner, 
                SectionTypes.LeftCorner, 
                SectionTypes.RightCorner, 
                SectionTypes.Finish 
            };
            Track rbrTrack = new Track("Red Bull Ring", redBullRingSections);

            Competition.Tracks.Enqueue(zandvoortTrack);
            Competition.Tracks.Enqueue(rbrTrack);

        }
                   

        public static void AddParticipants()
        {

            Car redCar = new Car() { Performance = 110, Quality = 100, Speed = 90, IsBroken = false };
            Car blueCar = new Car() { Performance = 90, Quality = 100, Speed = 110, IsBroken = false };

            Driver driver1 = new Driver() { Name = "Vax Merstappen", Points = 0, TeamColor = TeamColors.Red, Equipment = redCar };
            Driver driver2 = new Driver() { Name = "Hewis Lamilton", Points = 0, TeamColor = TeamColors.Blue, Equipment = blueCar };
            Driver driver3 = new Driver() { Name = "Nando Lorris", Points = 0, TeamColor = TeamColors.Yellow, Equipment = blueCar };

            Competition.Participants.Add(driver1);
            Competition.Participants.Add(driver2);
            Competition.Participants.Add(driver3);

        }

    }
}
