using System.Collections.Generic;

namespace ITunesLibraryParser {
    public class ITunesLibrary {

        private readonly IFileSystem fileSystem;
        private readonly string xmlLibraryFileLocation;
        private IEnumerable<Track> tracks;
        private IEnumerable<Playlist> playlists;
        private readonly TrackParser trackParser;

        public ITunesLibrary(string xmlLibraryFileLocation) : this(xmlLibraryFileLocation, new FileSystemWrapper()) { }

        public ITunesLibrary(string xmlLibraryFileLocation, IFileSystem fileSystem) {
            this.trackParser = new TrackParser();
            this.xmlLibraryFileLocation = xmlLibraryFileLocation;
            this.fileSystem = fileSystem;
        }

        public IEnumerable<Track> Tracks => tracks ?? (tracks = trackParser.ParseTracks(ReadTextFromLibraryFile()));

        private string ReadTextFromLibraryFile() {
            return fileSystem.ReadTextFromFile(xmlLibraryFileLocation);
        }

        public IEnumerable<Playlist> Playlists => playlists ?? (playlists = new PlaylistParser(Tracks).ParsePlaylists(ReadTextFromLibraryFile()));
    }
}