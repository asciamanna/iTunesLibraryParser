using System;
using System.Collections.Generic;
using System.Linq;

namespace ITunesLibraryParser {
    public class Playlist : IEquatable<Playlist> {
        public int PlaylistId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Track> Tracks { get; set; }

        public override string ToString() {
            return $"{Name} - {Tracks.Count()} tracks";
        }

        public bool Equals(Playlist other) {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return PlaylistId == other.PlaylistId &&
                   string.Equals(Name, other.Name) &&
                   Equals(Tracks.Count(), other.Tracks.Count());
        }

        public override bool Equals(object obj) {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Playlist)obj);
        }

        public override int GetHashCode() {
            unchecked {
                var hashCode = PlaylistId;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Tracks != null ? Tracks.GetHashCode() : 0);
                return hashCode;
            }
        }

        public Playlist Copy() {
            return MemberwiseClone() as Playlist;
        }
    }
}
