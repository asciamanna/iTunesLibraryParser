using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

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
          Name = (from key in track.Descendants("key")
                  where key.Value == "Name"
                  select (key.NextNode as XElement).Value).FirstOrDefault(),
          Artist = (from key in track.Descendants("key")
                    where key.Value == "Artist"
                    select (key.NextNode as XElement).Value).FirstOrDefault(),
          AlbumArtist = (from key in track.Descendants("key")
                         where key.Value == "Album Artist"
                         select (key.NextNode as XElement).Value).FirstOrDefault(),
          Composer = (from key in track.Descendants("key")
                      where key.Value == "Composer"
                      select (key.NextNode as XElement).Value).FirstOrDefault(),
          Album = (from key in track.Descendants("key")
                   where key.Value == "Album"
                   select (key.NextNode as XElement).Value).FirstOrDefault(),
          Genre = (from key in track.Descendants("key")
                   where key.Value == "Genre"
                   select (key.NextNode as XElement).Value).FirstOrDefault(),
          Kind = (from key in track.Descendants("key")
                  where key.Value == "Kind"
                  select (key.NextNode as XElement).Value).FirstOrDefault(),
          //Size = Int64.Parse((from key in track.Descendants("key")
          //        where key.Value == "Size"
          //        select (key.NextNode as XElement).Value).FirstOrDefault()),
          //TotalTime = Int64.Parse((from key in track.Descendants("key")
          //              where key.Value == "Total Time"
          //        select (key.NextNode as XElement).Value).FirstOrDefault()),
          //TrackNumber = Int32.Parse((from key in track.Descendants("key")
          //                where key.Value == "Track Number"
          //                select (key.NextNode as XElement).Value).FirstOrDefault()),
          //Year = Int32.Parse((from key in track.Descendants("key")
          //        where key.Value == "Year"
          //        select (key.NextNode as XElement).Value).FirstOrDefault()),
          //DateModified = DateTime.Parse((from key in track.Descendants("key")
          //                where key.Value == "Date Modified"
          //                select (key.NextNode as XElement).Value).FirstOrDefault()),
          //DateAdded = DateTime.Parse((from key in track.Descendants("key")
          //              where key.Value == "Date Added" 
          //              select (key.NextNode as XElement).Value).FirstOrDefault()),
          //BitRate = Int32.Parse((from key in track.Descendants("key")
          //            where key.Value == "Bit Rate"
          //            select (key.NextNode as XElement).Value).FirstOrDefault()),
          //SampleRate = Int32.Parse((from key in track.Descendants("key")
          //              where key.Value == "Sample Rate"
          //              select (key.NextNode as XElement).Value).FirstOrDefault()),
          //PlayDate = Int64.Parse((from key in track.Descendants("key")
          //              where key.Value == "Play Date"
          //              select (key.NextNode as XElement).Value).FirstOrDefault()),
          //PlayDateUTC = DateTime.Parse((from key in track.Descendants("key")
                          //where key.Value == "Play Date UTC"
                          //select (key.NextNode as XElement).Value).FirstOrDefault())
          //PlayCount = Int32.Parse((from key in track.Descendants("key")
          //              where key.Value == "Play Count"
          //              select (key.NextNode as XElement).Value).FirstOrDefault())

        });
      }
      for (int i = 0; i < 10; i++) {
        Console.WriteLine(tracks[i].ToString());
      }
      return tracks;
    }
  }
}
