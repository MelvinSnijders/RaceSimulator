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
            _positions = new Dictionary<Section, SectionData>();
            PlaceParticipants(track, participants);
        }


        public SectionData GetSectionData(Section section)
        {
            SectionData sectionData;
            if (!_positions.TryGetValue(section, out sectionData))
            {
                sectionData = new SectionData();
                _positions.Add(section, sectionData);
                
            }
            return sectionData;
        }

        public void PlaceParticipants(Track track, List<IParticipant> participants)
        {
            SectionData sectionData = new SectionData();
            LinkedList<Section>.Enumerator enumerator = track.Sections.GetEnumerator();
            enumerator.MoveNext();
            foreach (IParticipant participant in participants)
            {
                if (sectionData.Right == null)
                {
                    sectionData.Right = participant;
                } else if(sectionData.Left == null)
                {
                    sectionData.Left = participant;
                } else
                {

                    _positions.Add(enumerator.Current, sectionData);
                    sectionData = new SectionData();
                    sectionData.Left = participant;
                    enumerator.MoveNext();
                    while (enumerator.Current.SectionType != SectionTypes.StartGrid)
                    {
                        enumerator.MoveNext();
                    }
                }
            }
            _positions.Add(enumerator.Current, sectionData);
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
