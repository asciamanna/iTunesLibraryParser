using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Xml.Linq;

namespace ITunesLibraryParser {
    public interface IITunesLibrary {
        IEnumerable<Track> Tracks { get; }
        IEnumerable<Playlist> Playlists { get; }
    }

    public class ITunesLibrary : IITunesLibrary {
        private readonly IFileSystem fileSystem;
        private readonly string xmlFileLocation;
        private string fileContents;
        private IEnumerable<Track> tracks;
        private IEnumerable<Playlist> playlists;

        public ITunesLibrary(string xmlFileLocation) : this(xmlFileLocation, new FileSystem()) { }

        public ITunesLibrary(string xmlFileLocation, IFileSystem fileSystem) {
            this.xmlFileLocation = xmlFileLocation;
            this.fileSystem = fileSystem;
        }

        public IEnumerable<Track> Tracks => tracks ?? (tracks = ParseTracks());

        private string FileContents => fileContents ?? (fileContents = fileSystem.ReadTextFromFile(xmlFileLocation));

        private IEnumerable<Track> ParseTracks() {
            var trackElements = ParseTrackElements(FileContents);
            return trackElements.Select(CreateTrack);
        }

        private IEnumerable<XElement> ParseTrackElements(string libraryContents) {
            return from x in XDocument.Parse(FileContents).Descendants("dict").Descendants("dict").Descendants("dict")
                   where x.Descendants("key").Count() > 1
                   select x;
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
                Size = XElementParser.ParseLongValue(trackElement, "Size"),
                PlayingTime = TimeConvert.MillisecondsToFormattedMinutesAndSeconds((XElementParser.ParseLongValue(trackElement, "Total Time"))),
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
            var playlistElements = ParsePlaylistElements(FileContents);
            return playlistElements.Select(CreatePlaylist);
        }

        private Playlist CreatePlaylist(XElement playlistElement) {
            return new Playlist {
                Name = XElementParser.ParseStringValue(playlistElement, "Name"),
                Tracks = FindTracksInLibrary(playlistElement)
            };
        }

        public IEnumerable<XElement> ParsePlaylistElements(string libraryContents) {
            return XDocument.Parse(libraryContents).Descendants("dict").Descendants("array")
                .Descendants("dict").Where(node => node.Descendants("key").Count() > 1);
        }

        private IEnumerable<Track> FindTracksInLibrary(XElement playlistElement) {
            var trackIdElements = playlistElement.Descendants("dict").Descendants("integer");
            var trackIds = trackIdElements.Select(t => t.Value).Select(Int32.Parse);
            return FindTrackList(trackIds);
        }

        private List<Track> FindTrackList(IEnumerable<int> trackIds) {
            var tracks = new List<Track>();
            foreach (var trackId in trackIds) {
                var matchingTrack = Tracks.FirstOrDefault(t => t.TrackId == trackId);
                if (matchingTrack != null)
                    tracks.Add(matchingTrack);
            }
            return tracks;
        }
    }
}