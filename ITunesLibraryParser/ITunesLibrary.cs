using System.Collections.Generic;

namespace ITunesLibraryParser {
    public class ITunesLibrary {

        private readonly string xmlLibraryFileLocation;
        private readonly IFileSystem fileSystem;
        private readonly TrackParser trackParser;
        private readonly AlbumParser albumParser;
        private IEnumerable<Track> tracks;
        private IEnumerable<Playlist> playlists;
        private IEnumerable<Album> albums;

        public ITunesLibrary(string xmlLibraryFileLocation) : this(xmlLibraryFileLocation, new FileSystemWrapper()) { }

        public ITunesLibrary(string xmlLibraryFileLocation, IFileSystem fileSystem) {
            this.xmlLibraryFileLocation = xmlLibraryFileLocation;
            this.fileSystem = fileSystem;
            this.trackParser = new TrackParser();
            this.albumParser = new AlbumParser();
        }

        public IEnumerable<Track> Tracks => tracks ?? (tracks = trackParser.ParseTracks(ReadTextFromLibraryFile()));

        private string ReadTextFromLibraryFile() {
            return fileSystem.ReadTextFromFile(xmlLibraryFileLocation);
        }

        public IEnumerable<Playlist> Playlists => playlists ?? (playlists = new PlaylistParser(Tracks).ParsePlaylists(ReadTextFromLibraryFile()));

        public IEnumerable<Album> Albums => albums ?? (albums = albumParser.ParseAlbums(Tracks));
    }
}