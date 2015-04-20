using System;
using ITunesLibraryParser;
using NUnit.Framework;

namespace ITunesLibraryParserTests {
  [TestFixture]
  public class TrackTests {
    [Test]
    public void ToString_Track() {
      var track = CreateTrack();
      Assert.That(track.ToString(), Is.EqualTo(string.Format("Artist: {0} - Track: {1} - Album: {2}", track.Artist, track.Name, track.Album)));
    }

    private static Track CreateTrack() {
      return new Track {
        TrackId = 456,
        Name = "Witch Hunt",
        Artist = "Wayne Shorter",
        AlbumArtist = "Wayne Shorter",
        Composer = "Wayne Shorter",
        Album = "Speak No Evil",
        Genre = "Jazz",
        Kind = "AAC Audio File",
        Size = 5768594,
        PlayingTime = "4:35",
        TrackNumber = 1,
        Year = 1964,
        DateModified = DateTime.Today,
        DateAdded = DateTime.Today,
        BitRate = 330,
        SampleRate = 44100,
        PlayCount = 55,
        PlayDate = DateTime.Today,
        PartOfCompilation = false
      };
    }
  }
}
