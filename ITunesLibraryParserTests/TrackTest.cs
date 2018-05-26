using ITunesLibraryParser;
using ITunesLibraryParserTests.TestObjects;
using NUnit.Framework;

namespace ITunesLibraryParserTests {
    [TestFixture]
    public class TrackTest {
        private Track subject;

        [SetUp]
        public void Setup() {
            subject = TestTrack.Create();
        }

        [Test]
        public void ToString_Track() {
            Assert.That(subject.ToString(), Is.EqualTo(string.Format("Artist: {0} - Track: {1} - Album: {2}", subject.Artist, subject.Name, subject.Album)));
        }

        [Test]
        public void Copy() {
            var result = subject.Copy();
            Assert.That(result, Is.EqualTo(subject));
            Assert.That(result, Is.Not.SameAs(subject));
        }
    }
}
