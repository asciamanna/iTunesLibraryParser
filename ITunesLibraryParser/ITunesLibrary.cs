using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ITunesLibraryParser {
    public class ITunesLibrary {

        private readonly IFileSystem fileSystem;
        private readonly string xmlLibraryFileLocation;
        private IEnumerable<Track> tracks;
        private IEnumerable<Playlist> playlists;
        private Dictionary<int, Track> tracksByIdLookup;

        public ITunesLibrary(string xmlLibraryFileLocation) : this(xmlLibraryFileLocation, new FileSystemWrapper()) { }

        public ITunesLibrary(string xmlLibraryFileLocation, IFileSystem fileSystem) {
            this.xmlLibraryFileLocation = xmlLibraryFileLocation;
            this.fileSystem = fileSystem;
        }

        public IEnumerable<Track> Tracks => tracks ?? (tracks = ParseTracks());

        private IEnumerable<Track> ParseTracks() {
            return ParseTrackElements().Select(CreateTrack);
        }

        private IEnumerable<XElement> ParseTrackElements() {
            return from x in XDocument.Parse(ReadTextFromFile()).Descendants("dict").Descendants("dict").Descendants("dict")
                   where x.Descendants("key").Count() > 1
                   select x;
        }
        private string ReadTextFromFile() {
            return fileSystem.ReadXmlTextFromFile(xmlLibraryFileLocation);
        }

        private static Track CreateTrack(XElement trackElement) {
            return new Track {
                TrackId = Int32.Parse(XElementParser.ParseStringValue(trackElement, "Track ID")),
                Name = XElementParser.ParseStringValue(trackElement, "Name"),
                Artist = XElementParser.ParseStringValue(trackElement, "Artist"),
                AlbumArtist = XElementParser.ParseStringValue(trackElement, "AlbumArtist"),
                Composer = XElementParser.ParseStringValue(trackElement, "Composer"),
                Album = XElementParser.ParseStringValue(trackElement, "Album"),
                Genre = XElementParser.ParseStringValue(trackElement, "Genre"),
                Kind = XElementParser.ParseStringValue(trackElement, "Kind"),
                Size = XElementParser.ParseNullableLongValue(trackElement, "Size"),
                PlayingTime = TimeConvert.MillisecondsToFormattedMinutesAndSeconds((XElementParser.ParseNullableLongValue(trackElement, "Total Time"))),
                TrackNumber = XElementParser.ParseNullableIntValue(trackElement, "Track Number"),
                Year = XElementParser.ParseNullableIntValue(trackElement, "Year"),
                DateModified = XElementParser.ParseNullableDateValue(trackElement, "Date Modified"),
                DateAdded = XElementParser.ParseNullableDateValue(trackElement, "Date Added"),
                BitRate = XElementParser.ParseNullableIntValue(trackElement, "Bit Rate"),
                SampleRate = XElementParser.ParseNullableIntValue(trackElement, "Sample Rate"),
                PlayDate = XElementParser.ParseNullableDateValue(trackElement, "Play Date UTC"),
                PlayCount = XElementParser.ParseNullableIntValue(trackElement, "Play Count"),
                PartOfCompilation = XElementParser.ParseBoolean(trackElement, "Compilation"),
            };
        }

        public IEnumerable<Playlist> Playlists => playlists ?? (playlists = ParsePlaylists());

        private IEnumerable<Playlist> ParsePlaylists() {
            return ParsePlaylistElements().Select(CreatePlaylist);
        }

        public IEnumerable<XElement> ParsePlaylistElements() {
            return XDocument.Parse(ReadTextFromFile()).Descendants("dict").Descendants("array")
                .Descendants("dict").Where(node => node.Descendants("key").Count() > 1);
        }

        private Playlist CreatePlaylist(XElement playlistElement) {
            return new Playlist {
                PlaylistId = Int32.Parse(XElementParser.ParseStringValue(playlistElement, "Playlist ID")),
                Name = XElementParser.ParseStringValue(playlistElement, "Name"),
                Tracks = FindTracksInLibrary(playlistElement)
            };
        }

        private IEnumerable<Track> FindTracksInLibrary(XElement playlistElement) {
            var trackIdElements = playlistElement.Descendants("dict").Descendants("integer");
            var trackIds = trackIdElements.Select(t => t.Value).Select(Int32.Parse);
            return BuildTrackList(trackIds);
        }

        private List<Track> BuildTrackList(IEnumerable<int> trackIds) {
            var playlistTracks = new List<Track>();
            foreach (var trackId in trackIds) {
                TracksByIdLookup.TryGetValue(trackId, out var matchingTrack);
                if (matchingTrack != null)
                    playlistTracks.Add(matchingTrack);
            }
            return playlistTracks;
        }

        private Dictionary<int, Track> TracksByIdLookup {
            get { return tracksByIdLookup ?? (tracksByIdLookup = Tracks.ToDictionary(t => t.TrackId)); }
        }
    }
}