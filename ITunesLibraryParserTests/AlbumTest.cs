using System.Linq;
using ITunesLibraryParser;
using ITunesLibraryParserTests.TestObjects;
using NUnit.Framework;

namespace ITunesLibraryParserTests {
    [TestFixture]
    public class AlbumTest {
        private Album subject;

        [SetUp]
        public void SetUp() {
            subject = TestAlbum.Create();
        }

        [Test]
        public void Album_ToString() {
            Assert.That(subject.ToString(), 
                Is.EqualTo($"{subject.Artist} - {subject.AlbumName} - {subject.Tracks.Count()} tracks"));
        }

        [Test]
        public void Copy() {
            var result = subject.Copy();

            Assert.That(result, Is.EqualTo(subject));
            Assert.That(result, Is.Not.SameAs(subject));
        }
    }
}
