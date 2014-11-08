using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Globalization;

namespace ITunesLibraryParser {
  public interface IITunesLibrary {
    IEnumerable<Track> Parse(string xmlFileLocation);
  }

  public class ITunesLibrary : IITunesLibrary {
    public IEnumerable<Track> Parse(string fileLocation) {
      var trackElements = LoadTrackElements(fileLocation);
      return trackElements.Select(te => CreateTrack(te));
    }

    private static IEnumerable<XElement> LoadTrackElements(string fileLocation) {
      return from x in XDocument.Load(fileLocation).Descendants("dict").Descendants("dict").Descendants("dict")
                          where x.Descendants("key").Count() > 1
                          select x;
    }

    private Track CreateTrack(XElement trackElement) {
      return new Track {
        TrackId = Int32.Parse(ParseStringValue(trackElement, "Track ID")),
        Name = ParseStringValue(trackElement, "Name"),
        Artist = ParseStringValue(trackElement, "Artist"),
        AlbumArtist = ParseStringValue(trackElement, "AlbumArtist"),
        Composer = ParseStringValue(trackElement, "Composer"),
        Album = ParseStringValue(trackElement, "Album"),
        Genre = ParseStringValue(trackElement, "Genre"),
        Kind = ParseStringValue(trackElement, "Kind"),
        Size = ParseLongValue(trackElement, "Size"),
        PlayingTime = ConvertMillisecondsToFormattedMinutesAndSeconds((ParseLongValue(trackElement, "Total Time"))),
        TrackNumber = ParseNullableIntValue(trackElement, "Track Number"),
        Year = ParseNullableIntValue(trackElement, "Year"),
        DateModified = ParseNullableDateValue(trackElement, "Date Modified"),
        DateAdded = ParseNullableDateValue(trackElement, "Date Added"),
        BitRate = ParseNullableIntValue(trackElement, "Bit Rate"),
        SampleRate = ParseNullableIntValue(trackElement, "Sample Rate"),
        PlayDate = ParseNullableDateValue(trackElement, "Play Date UTC"),
        PlayCount = ParseNullableIntValue(trackElement, "Play Count"),
        PartOfCompilation = ParseBoolean(trackElement, "Compilation"),
      };
    }

    bool ParseBoolean(XElement track, string keyValue) {
      return (from keyNode in track.Descendants("key")
              where keyNode.Value == keyValue
              select (keyNode.NextNode as XElement).Name).FirstOrDefault() == "true";
    }

    string ParseStringValue(XElement track, string keyValue) {
      return (from key in track.Descendants("key")
              where key.Value == keyValue
              select (key.NextNode as XElement).Value).FirstOrDefault();
    }

    long ParseLongValue(XElement track, string keyValue) {
      var stringValue = ParseStringValue(track, keyValue);
      return Int64.Parse(stringValue);
    }

    int? ParseNullableIntValue(XElement track, string keyValue) {
      var stringValue = ParseStringValue(track, keyValue);
      return String.IsNullOrEmpty(stringValue) ? (int?)null : Int32.Parse(stringValue);
    }

    DateTime? ParseNullableDateValue(XElement track, string keyValue) {
      var stringValue = ParseStringValue(track, keyValue);
      return String.IsNullOrEmpty(stringValue) ? (DateTime?)null : DateTime.SpecifyKind(DateTime.Parse(stringValue, CultureInfo.InvariantCulture), DateTimeKind.Utc).ToLocalTime();
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
