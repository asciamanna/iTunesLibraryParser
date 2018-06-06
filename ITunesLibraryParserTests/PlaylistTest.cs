using System.Linq;
using ITunesLibraryParser.Tests.TestObjects;
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
        public void Equals_Returns_True_When_Equal() {
            var other = TestPlaylist.Create();

            var result = subject.Equals(other);

            Assert.That(result, Is.True);
        }

        [Test]
        public void Equals_Returns_True_When_Same_Reference() {
            var result = subject.Equals(subject);

            Assert.That(result, Is.True);
        }

        [Test]
        public void Object_Equals_Returns_False_When_Not_SameType() {
            var other = TestAlbum.Create();

            var result = subject.Equals(other);

            Assert.That(result, Is.False);
        }

        [Test]
        public void Object_Equals_Returns_False_When_Null() {
            var result = subject.Equals((object)null);

            Assert.That(result, Is.False);
        }

        [Test]
        public void Object_Equals_Returns_True_When_Equal() {
            var other = TestPlaylist.Create() as object;

            var result = subject.Equals(other);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GetHashCode_Returns_The_Same_HashCode_For_Equal_Playlists() {
            var expectedHashCode = subject.Copy().GetHashCode();

            var result = subject.GetHashCode();

            Assert.That(result, Is.EqualTo(expectedHashCode));
        }

        [Test]
        public void GetHashCode_Returns_A_Different_HashCode_For_Playlists_Not_Equal() {
            var other = subject.Copy();
            other.Name = "Jazz for the Dark Hours";
            var expectedHashCode = other.GetHashCode();

            var result = subject.GetHashCode();

            Assert.That(result, Is.Not.EqualTo(expectedHashCode));
        }
    }
}
