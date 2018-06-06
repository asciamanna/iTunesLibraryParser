using ITunesLibraryParser.Tests.TestObjects;
using NUnit.Framework;

namespace ITunesLibraryParser.Tests {
    [TestFixture]
    public class TrackTest {
        private Track subject;

        [SetUp]
        public void Setup() {
            subject = TestTrack.Create();
        }

        [Test]
        public void ToString_Track() {
            Assert.That(subject.ToString(), Is.EqualTo(
                $"Artist: {subject.Artist} - Track: {subject.Name} - Album: {subject.Album}"));
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
            var other = TestTrack.Create();
            other.Name = "Maiden Voyage";

            var result = subject.Equals(other);

            Assert.That(result, Is.False);
        }

        [Test]
        public void Equals_Returns_True_When_Equal() {
            var other = TestTrack.Create();

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
            var other = TestTrack.Create() as object;

            var result = subject.Equals(other);

            Assert.That(result, Is.True);
        }

        [Test]
        public void GetHashCode_Returns_The_Same_HashCode_For_Equal_Tracks() {
            var expectedHashCode = subject.Copy().GetHashCode();
            
            var result = subject.GetHashCode();

            Assert.That(result, Is.EqualTo(expectedHashCode));
        }

        [Test]
        public void GetHashCode_Returns_A_Different_HashCode_For_Tracks_Not_Equal() {
            var other = subject.Copy();
            other.AlbumArtist = "John Coltrane";
            var expectedHashCode = other.GetHashCode();

            var result = subject.GetHashCode();

            Assert.That(result, Is.Not.EqualTo(expectedHashCode));
        }
    }
}
