using System;

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
      return MemberwiseClone() as Track;
    }

    protected bool Equals(Track other) {
      return TrackId == other.TrackId && string.Equals(Name, other.Name) && string.Equals(Artist, other.Artist) && 
        string.Equals(AlbumArtist, other.AlbumArtist) && string.Equals(Composer, other.Composer) && 
        string.Equals(Album, other.Album) && string.Equals(Genre, other.Genre) && 
        string.Equals(Kind, other.Kind) && Size == other.Size && string.Equals(PlayingTime, other.PlayingTime) && 
        TrackNumber == other.TrackNumber && Year == other.Year && DateModified.Equals(other.DateModified) && 
        DateAdded.Equals(other.DateAdded) && BitRate == other.BitRate && SampleRate == other.SampleRate && 
        PlayCount == other.PlayCount && PlayDate.Equals(other.PlayDate) && PartOfCompilation == other.PartOfCompilation;
    }

    public override bool Equals(object obj) {
      if (ReferenceEquals(null, obj)) return false;
      if (ReferenceEquals(this, obj)) return true;
      if (obj.GetType() != this.GetType()) return false;
      return Equals((Track)obj);
    }

    public override int GetHashCode() {
      unchecked {
        var hashCode = TrackId;
        hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ (Artist != null ? Artist.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ (AlbumArtist != null ? AlbumArtist.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ (Composer != null ? Composer.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ (Album != null ? Album.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ (Genre != null ? Genre.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ (Kind != null ? Kind.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ Size.GetHashCode();
        hashCode = (hashCode * 397) ^ (PlayingTime != null ? PlayingTime.GetHashCode() : 0);
        hashCode = (hashCode * 397) ^ TrackNumber.GetHashCode();
        hashCode = (hashCode * 397) ^ Year.GetHashCode();
        hashCode = (hashCode * 397) ^ DateModified.GetHashCode();
        hashCode = (hashCode * 397) ^ DateAdded.GetHashCode();
        hashCode = (hashCode * 397) ^ BitRate.GetHashCode();
        hashCode = (hashCode * 397) ^ SampleRate.GetHashCode();
        hashCode = (hashCode * 397) ^ PlayCount.GetHashCode();
        hashCode = (hashCode * 397) ^ PlayDate.GetHashCode();
        hashCode = (hashCode * 397) ^ PartOfCompilation.GetHashCode();
        return hashCode;
      }
    }
  }

}
