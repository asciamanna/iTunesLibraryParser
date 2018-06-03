using System.Linq;
using ITunesLibraryParserTests.TestObjects;
using NUnit.Framework;

namespace ITunesLibraryParser.Tests {

    [TestFixture]
    public class PlaylistTest {

        private Playlist subject;

        [SetUp]
        public void SetUp() {
            subject = TestPlaylist.Create();
        }

        [Test]
        public void ToString_Playlist() {
            Assert.That(subject.ToString(), 
                Is.EqualTo($"{subject.Name} - {subject.Tracks.Count()} tracks"));
        }

        [Test]
        public void Copy() {
            var result = subject.Copy();

            Assert.That(result, Is.EqualTo(subject));
            Assert.That(result, Is.Not.SameAs(subject));
        }
    }
}
