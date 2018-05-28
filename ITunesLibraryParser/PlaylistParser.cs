using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ITunesLibraryParser {
    internal class PlaylistParser : ParserBase {
        private readonly Dictionary<int, Track> trackLookup;

        internal PlaylistParser(IEnumerable<Track> tracks) {
            trackLookup = tracks.ToDictionary(t => t.TrackId);
        }
        internal IEnumerable<Playlist> ParsePlaylists(string libraryContents) {
            return ParseElements(libraryContents).Select(CreatePlaylist);
        }

        protected override string GetCollectionNodeName() {
            return "array";
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
                trackLookup.TryGetValue(trackId, out var matchingTrack);
                if (matchingTrack != null)
                    playlistTracks.Add(matchingTrack);
            }
            return playlistTracks;
        }
    }
}