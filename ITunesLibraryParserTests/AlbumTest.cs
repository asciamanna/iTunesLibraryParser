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
        public void Album_ToString_Uses_AlbumArtist_If_Exists() {
            subject.AlbumArtist = "Milt Jackson & Wes Montgomery";

            Assert.That(subject.ToString(), 
                Is.EqualTo($"{subject.AlbumArtist} - {subject.AlbumName} - {subject.Tracks.Count()} tracks"));
        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase(null)]
        public void Album_ToString_Uses_Artist_If_AlbumArtist_Doesnt_Exist(string albumArtist) {
            subject.AlbumArtist = albumArtist;

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
