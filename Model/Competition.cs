using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class Competition
    {

        public List<IParticipant> Participants = new List<IParticipant>();
        public Queue<Track> Tracks = new Queue<Track>();

        public Track NextTrack()
        {
            if (Tracks.Count > 0)
            {
                return Tracks.Dequeue();
            }
            return null;
        }

    }
}
