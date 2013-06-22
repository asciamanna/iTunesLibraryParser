using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITunesLibraryParser {
  public class Track {
    public int TrackId { get; set; }
    public string Name { get; set; }
    public string Artist { get; set; }
    public string AlbumArtist { get; set; }
    public string Composer { get; set; }
    public string Album { get; set; }
    public string Genre { get; set; }
    public string Kind { get; set; }
    public long Size { get; set; }
    public string PlayingTime { get; set; }
    public int? TrackNumber { get; set; }
    public int? Year { get; set; }
    public DateTime? DateModified { get; set; }
    public DateTime? DateAdded { get; set; }
    public int? BitRate { get; set; }
    public int? SampleRate { get; set; }
    public int? PlayCount { get; set; }
    public DateTime? PlayDate { get; set; }
    public bool PartOfCompilation { get; set; }

    public override string ToString() {
      return string.Format("Artist: {0} - Track: {1} - Album: {2}", Artist, Name, Album);
    }

    public Track Copy() {
     return this.MemberwiseClone() as Track;
    }
  }

}
