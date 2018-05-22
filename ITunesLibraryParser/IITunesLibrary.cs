using System.Collections.Generic;

namespace ITunesLibraryParser
{
    public interface IITunesLibrary
    {
        IEnumerable<Track> Tracks { get; }
        IEnumerable<string> Albums { get; }
        IEnumerable<string> Artists { get; }
        IEnumerable<string> Genres { get; }
        IEnumerable<string> Years { get; }
        IEnumerable<string> Playlists { get; }
        IEnumerable<Track> Filter(FilterKind kind, string param);
        IEnumerable<Track> ReadPlaylist(string id);
    }
}
