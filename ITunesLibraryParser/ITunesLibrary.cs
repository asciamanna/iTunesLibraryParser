using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Globalization;

namespace ITunesLibraryParser {
  public static class ITunesLibrary {
    public static IEnumerable<Track> Parse(string fileLocation) {
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
          TotalTime = ParseLongValue(track, "Total Time"),
          TrackNumber = ParseNullableIntValue(track, "Track Number"),
          Year = ParseNullableIntValue(track, "Year"),
          DateModified = ParseNullableDateValue(track, "Date Modified"),
          DateAdded = ParseNullableDateValue(track, "Date Added"),
          BitRate = ParseNullableIntValue(track, "Bit Rate"),
          SampleRate = ParseNullableIntValue(track, "Sample Rate"),
          PlayDate = ParseNullableDateValue(track, "Play Date UTC"),
          PlayCount = ParseNullableIntValue(track, "Play Count")
        });
      }
      return tracks;
    }

    static string ParseStringValue(XElement track, string keyValue) {
      return (from key in track.Descendants("key")
              where key.Value == keyValue
              select (key.NextNode as XElement).Value).FirstOrDefault();
    }

    static long ParseLongValue(XElement track, string keyValue) {
      var stringValue = ParseStringValue(track, keyValue);
      return Int64.Parse(stringValue);
    }

    static int? ParseNullableIntValue(XElement track, string keyValue) {
      var stringValue = ParseStringValue(track, keyValue);
      return String.IsNullOrEmpty(stringValue) ? (int?)null : Int32.Parse(stringValue);
    }

    static DateTime? ParseNullableDateValue(XElement track, string keyValue) {
      var stringValue = ParseStringValue(track, keyValue);
      //DateTime.ParseExact(keyValue, "yyyy-MM-ddTHH:mm:ssz", CultureInfo.InvariantCulture);
      return String.IsNullOrEmpty(stringValue) ? (DateTime?)null : DateTime.SpecifyKind(DateTime.Parse(stringValue, CultureInfo.InvariantCulture), DateTimeKind.Utc).ToLocalTime();
    }
  }
}
