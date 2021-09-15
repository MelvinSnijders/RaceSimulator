using System;
using System.Collections.Generic;
using System.Text;
using Model;

namespace Controller
{
    public class Race
    {
        public Track RaceTrack;
        public List<IParticipant> Participants;
        public DateTime StartTime;
        private Random _random;
        private Dictionary<Section, SectionData> _positions;

        public Race(Track track, List<IParticipant> participants)
        {
            RaceTrack = track;
            Participants = participants;
            _random = new Random(DateTime.Now.Millisecond);
        }


        public SectionData GetSectionData(Section section)
        {
            SectionData sectionData = new SectionData();
            if (!_positions.TryGetValue(section, out sectionData))
            {
                _positions.Add(section, sectionData);
            }
            return sectionData;
        }

        public void RandomizeEquipment()
        {

            foreach (IParticipant participant in Participants)
            {
                participant.Equipment.Quality = _random.Next();
                participant.Equipment.Performance = _random.Next();
            }

        }

    }


}
