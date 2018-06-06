using System.Collections.Generic;

namespace ITunesLibraryParser.Tests.TestObjects {
    public static class TestPlaylist {
        public static Playlist Create() {
            return new Playlist {
                PlaylistId = 456,
                Name = "Jazz Ballads",
                Tracks = new List<Track> {
                    new Track {
                        Album = "Blue Trane",
                        AlbumArtist = "John Coltrane",
                        Artist = "John Coltrane",
                        Genre = "Jazz", 
                        Name = "I'm Old Fashioned"
                    },
                    new Track {
                        Album = "Idle Moments",
                        AlbumArtist = "Grant Green",
                        Artist = "Grant Green",
                        Genre = "Jazz",
                        Name = "Idle Moments"
                    }
                }
            };
        }
    }
}