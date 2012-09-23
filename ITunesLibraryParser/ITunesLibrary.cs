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
      var trackElements = XDocument.Load(fileLocation).Descendants("dict").Descendants("dict").Descendants("dict");
      foreach (var track in trackElements) {
        tracks.Add(new Track {
          TrackId = Int32.Parse((from key in track.Descendants("key")
                                 where key.Value == "Track ID"
                                 select (key.NextNode as XElement).Value).FirstOrDefault()),
          Name = ParseStringValue(track, "Name"),
          Artist = ParseStringValue(track, "Artist"),
          AlbumArtist = ParseStringValue(track, "AlbumArtist"),
          Composer = ParseStringValue(track, "Composer"),
          Album = ParseStringValue(track, "Album"),
          Genre = ParseStringValue(track, "Genre"),
          Kind = ParseStringValue(track, "Kind"),
          Size = ParseLongValue(track, "Size"),
          TotalTime = ParseLongValue(track, "Total Time"),
          TrackNumber = ParseIntValue(track, "Track Number"),
          Year = ParseIntValue(track, "Year"),
          DateModified = ParseDateValue(track, "Date Modified"),
          DateAdded = ParseDateValue(track, "Date Added"),
          BitRate = ParseIntValue(track, "Bit Rate"),
          SampleRate = ParseIntValue(track, "Sample Rate"),
          PlayDate = ConvertLongToDate(ParseLongValue(track, "Play Date")),
          PlayDateUTC = ParseDateValue(track, "Play Date UTC"),
          PlayCount = ParseIntValue(track, "Play Count")
        });
      }
      for (int i = 0; i < 10; i++) {
        Console.WriteLine(tracks[i].ToString());
      }
      return tracks;
    }

    static DateTime? ConvertLongToDate(long? ticks) {
      return ticks.HasValue ? new DateTime(ticks.Value) : (DateTime?)null;
    }

    static long? ParseLongValue(XElement track, string keyValue) {
      var stringValue = ParseStringValue(track, keyValue);
      return String.IsNullOrEmpty(stringValue) ? (long?)null : Int64.Parse(stringValue);
    }

    static int? ParseIntValue(XElement track, string keyValue) {
      var stringValue = ParseStringValue(track, keyValue);
      return String.IsNullOrEmpty(stringValue) ? (int?)null : Int32.Parse(stringValue);
    }

    static DateTime? ParseDateValue(XElement track, string keyValue) {
      var stringValue = ParseStringValue(track, keyValue);
      //DateTime.ParseExact(keyValue, "yyyy-MM-ddTHH:mm:ssz", CultureInfo.InvariantCulture);
      return String.IsNullOrEmpty(stringValue) ? (DateTime?)null : DateTime.Parse(stringValue, CultureInfo.InvariantCulture);
    }

    static string ParseStringValue(XElement track, string keyValue) {
      return (from key in track.Descendants("key")
              where key.Value == keyValue
              select (key.NextNode as XElement).Value).FirstOrDefault();
    }
  }
}
