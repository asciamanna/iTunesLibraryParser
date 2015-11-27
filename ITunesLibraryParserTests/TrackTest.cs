using ITunesLibraryParserTests.TestObjects;
using NUnit.Framework;

namespace ITunesLibraryParserTests {
  [TestFixture]
  public class TrackTest {
    [Test]
    public void ToString_Track() {
      var track = TestTrack.Create();
      Assert.That(track.ToString(), Is.EqualTo(string.Format("Artist: {0} - Track: {1} - Album: {2}", track.Artist, track.Name, track.Album)));
    }
  }
}
