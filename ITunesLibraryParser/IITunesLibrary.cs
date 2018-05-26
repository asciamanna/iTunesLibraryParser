using System.Collections.Generic;

namespace ITunesLibraryParser {
    public interface IITunesLibrary {
        IEnumerable<Track> Tracks { get; }
        IEnumerable<Playlist> Playlists { get; }
    }
}