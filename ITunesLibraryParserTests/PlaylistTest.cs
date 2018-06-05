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

        [Test]
        public void Equals_Returns_False_When_Null() {
            var result = subject.Equals(null);

            Assert.That(result, Is.False);
        }

        [Test]
        public void Equals_Returns_False_When_Not_Equal() {
            var other = TestPlaylist.Create();
            other.Name = "New Playlist";

            var result = subject.Equals(other);

            Assert.That(result, Is.False);
        }

        [Test]
        public void Equals_Returns_False_When_Not_SameType() {
            var other = TestAlbum.Create();

            var result = subject.Equals(other);

            Assert.That(result, Is.False);
        }

        [Test]
        public void Equals_Returns_True_When_Equal() {
            var other = TestPlaylist.Create();

            var result = subject.Equals(other);

            Assert.That(result, Is.True);
        }
    }
}
