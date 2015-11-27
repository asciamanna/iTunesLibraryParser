using System;
using ITunesLibraryParser;

namespace ITunesLibraryParserTests.TestObjects {
  public static class TestTrack {
    public static Track Create() {
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