using Model;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace ControllerTest
{

    [TestFixture]
    public class Model_Competition_NextTrackShould
    {

        private Competition _competiton;

        [SetUp]
        public void SetUp()
        {
            _competiton = new Competition();
        }

        [Test]
        public void NextTrack_EmptyQueue_ReturnNull()
        {
            Track result = _competiton.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_OneInQueue_ReturnTrack()
        {
            Track track = new Track("TestTrack", new SectionTypes[] { SectionTypes.Straight });
            _competiton.Tracks.Enqueue(track);
            Track result = _competiton.NextTrack();
            Assert.AreEqual(track, result);
        }

        [Test]
        public void NextTrack_OneInQueue_RemoveTrackFromQueue()
        {
            Track track = new Track("TestTrack", new SectionTypes[] { SectionTypes.Straight });
            _competiton.Tracks.Enqueue(track);
            Track result = _competiton.NextTrack();
            result = _competiton.NextTrack();
            Assert.IsNull(result);
        }

        [Test]
        public void NextTrack_TwoInQueue_ReturnNextTrack()
        {
            Track track = new Track("TestTrack1", new SectionTypes[] { SectionTypes.Straight });
            Track track2 = new Track("TestTrack2", new SectionTypes[] { SectionTypes.Straight });
            _competiton.Tracks.Enqueue(track);
            _competiton.Tracks.Enqueue(track2);
            Track result = _competiton.NextTrack();
            Assert.AreEqual(track, result);
            result = _competiton.NextTrack();
            Assert.AreEqual(track2, result);
        }

    }
}
