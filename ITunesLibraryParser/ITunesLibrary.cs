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
      var tracks = new List<Track>();
      var trackElements = from x in XDocument.Load(fileLocation).Descendants("dict").Descendants("dict").Descendants("dict")
                          where x.Descendants("key").Count() > 1
                          select x;
      foreach (var track in trackElements) {
        tracks.Add(new Track {
          TrackId = Int32.Parse(ParseStringValue(track, "Track ID")),
          Name = ParseStringValue(track, "Name"),
          Artist = ParseStringValue(track, "Artist"),
          AlbumArtist = ParseStringValue(track, "AlbumArtist"),
          Composer = ParseStringValue(track, "Composer"),
          Album = ParseStringValue(track, "Album"),
          Genre = ParseStringValue(track, "Genre"),
          Kind = ParseStringValue(track, "Kind"),
          Size = ParseLongValue(track, "Size"),
          PlayingTime = ConvertMillisecondsToFormattedMinutesAndSeconds((ParseLongValue(track, "Total Time"))),
          TrackNumber = ParseNullableIntValue(track, "Track Number"),
          Year = ParseNullableIntValue(track, "Year"),
          DateModified = ParseNullableDateValue(track, "Date Modified"),
          DateAdded = ParseNullableDateValue(track, "Date Added"),
          BitRate = ParseNullableIntValue(track, "Bit Rate"),
          SampleRate = ParseNullableIntValue(track, "Sample Rate"),
          PlayDate = ParseNullableDateValue(track, "Play Date UTC"),
          PlayCount = ParseNullableIntValue(track, "Play Count"),
          PartOfCompilation = ParseBoolean(track, "Compilation"),
        });
      }
      return tracks;
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
