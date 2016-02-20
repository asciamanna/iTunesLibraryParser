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
        TrackId = Int32.Parse(XElementParser.ParseStringValue(trackElement, "Track ID")),
        Name = XElementParser.ParseStringValue(trackElement, "Name"),
        Artist = XElementParser.ParseStringValue(trackElement, "Artist"),
        AlbumArtist = XElementParser.ParseStringValue(trackElement, "AlbumArtist"),
        Composer = XElementParser.ParseStringValue(trackElement, "Composer"),
        Album = XElementParser.ParseStringValue(trackElement, "Album"),
        Genre = XElementParser.ParseStringValue(trackElement, "Genre"),
        Kind = XElementParser.ParseStringValue(trackElement, "Kind"),
        Size = XElementParser.ParseLongValue(trackElement, "Size"),
        PlayingTime = TimeConvert.MillisecondsToFormattedMinutesAndSeconds((XElementParser.ParseLongValue(trackElement, "Total Time"))),
        TrackNumber = XElementParser.ParseNullableIntValue(trackElement, "Track Number"),
        Year = XElementParser.ParseNullableIntValue(trackElement, "Year"),
        DateModified = XElementParser.ParseNullableDateValue(trackElement, "Date Modified"),
        DateAdded = XElementParser.ParseNullableDateValue(trackElement, "Date Added"),
        BitRate = XElementParser.ParseNullableIntValue(trackElement, "Bit Rate"),
        SampleRate = XElementParser.ParseNullableIntValue(trackElement, "Sample Rate"),
        PlayDate = XElementParser.ParseNullableDateValue(trackElement, "Play Date UTC"),
        PlayCount = XElementParser.ParseNullableIntValue(trackElement, "Play Count"),
        PartOfCompilation = XElementParser.ParseBoolean(trackElement, "Compilation"),
      };
    }
  }
}
