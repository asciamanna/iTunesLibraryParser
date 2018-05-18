using System.Collections.Generic;

namespace ITunesLibraryParser
{
    public enum FilterKind
    {
        None,
        Album,
        Artist,
        Geneires,
        Year
    }

    public interface IITunesLibrary
    {
        IEnumerable<Track> Tracks { get; }
        IEnumerable<string> Albums { get; }
        IEnumerable<string> Artists { get; }
        IEnumerable<string> Geneires { get; }
        IEnumerable<string> Years { get; }
        IEnumerable<Track> Filter(FilterKind kind, string param);
    }
}
