using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ITunesLibraryParser {
  public interface IITunesLibrary {
    IEnumerable<Track> Parse(string xmlFileLocation);
  }

  public class ITunesLibrary : IITunesLibrary {

    public IEnumerable<Track> Parse(string fileLocation) {
      var trackElements = LoadTrackElements(fileLocation);
      return trackElements.Select(CreateTrack);
    }

    private static IEnumerable<XElement> LoadTrackElements(string fileLocation) {
      return from x in XDocument.Load(fileLocation).Descendants("dict").Descendants("dict").Descendants("dict")
                          where x.Descendants("key").Count() > 1
                          select x;
    }

    private Track CreateTrack(XElement trackElement) {
      return new Track {
        TrackId = Int32.Parse(XElementParse.ParseStringValue(trackElement, "Track ID")),
        Name = XElementParse.ParseStringValue(trackElement, "Name"),
        Artist = XElementParse.ParseStringValue(trackElement, "Artist"),
        AlbumArtist = XElementParse.ParseStringValue(trackElement, "AlbumArtist"),
        Composer = XElementParse.ParseStringValue(trackElement, "Composer"),
        Album = XElementParse.ParseStringValue(trackElement, "Album"),
        Genre = XElementParse.ParseStringValue(trackElement, "Genre"),
        Kind = XElementParse.ParseStringValue(trackElement, "Kind"),
        Size = XElementParse.ParseLongValue(trackElement, "Size"),
        PlayingTime = ConvertMillisecondsToFormattedMinutesAndSeconds((XElementParse.ParseLongValue(trackElement, "Total Time"))),
        TrackNumber = XElementParse.ParseNullableIntValue(trackElement, "Track Number"),
        Year = XElementParse.ParseNullableIntValue(trackElement, "Year"),
        DateModified = XElementParse.ParseNullableDateValue(trackElement, "Date Modified"),
        DateAdded = XElementParse.ParseNullableDateValue(trackElement, "Date Added"),
        BitRate = XElementParse.ParseNullableIntValue(trackElement, "Bit Rate"),
        SampleRate = XElementParse.ParseNullableIntValue(trackElement, "Sample Rate"),
        PlayDate = XElementParse.ParseNullableDateValue(trackElement, "Play Date UTC"),
        PlayCount = XElementParse.ParseNullableIntValue(trackElement, "Play Count"),
        PartOfCompilation = XElementParse.ParseBoolean(trackElement, "Compilation"),
      };
    }

    static string ConvertMillisecondsToFormattedMinutesAndSeconds(long milliseconds) {
      var totalSeconds = Math.Round(TimeSpan.FromMilliseconds(milliseconds).TotalSeconds);
      var minutes = (int)(totalSeconds / 60);
      var seconds = (int)(totalSeconds - (minutes * 60));
      var timespan = new TimeSpan(0, minutes, seconds);
      return timespan.ToString("m\\:ss");
    }
  }
}
