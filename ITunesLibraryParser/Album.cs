using System;
using System.Collections.Generic;
using System.Linq;

namespace ITunesLibraryParser {
    public class Album : IEquatable<Album> {
        public string Artist { get; set; }
        public string AlbumName { get; set; }
        public string Genre { get; set; }
        public int? Year { get; set; }
        public bool IsCompilation { get; set; }
        public IEnumerable<Track> Tracks { get; set; }

        public override string ToString() {
            return $"{Artist} - {AlbumName} - {Tracks.Count()} tracks";
        }

        public Album Copy() {
            return MemberwiseClone() as Album;
        }

        public bool Equals(Album other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Artist, other.Artist) && 
                   string.Equals(AlbumName, other.AlbumName) && 
                   string.Equals(Genre, other.Genre) && 
                   Year == other.Year && 
                   IsCompilation == other.IsCompilation && 
                   Equals(Tracks, other.Tracks);
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Album) obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = (Artist != null ? Artist.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (AlbumName != null ? AlbumName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Genre != null ? Genre.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Year.GetHashCode();
                hashCode = (hashCode * 397) ^ IsCompilation.GetHashCode();
                hashCode = (hashCode * 397) ^ (Tracks != null ? Tracks.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}