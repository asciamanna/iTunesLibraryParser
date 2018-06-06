using System.Collections.Generic;

namespace ITunesLibraryParser.Tests.TestObjects {
    public static class TestAlbum {
        public static Album Create() {
            return new Album {
                Name = "Bags Meets Wes",
                Artist = "Milt Jackson & Wes Montgomery",
                Tracks = new List<Track> {
                    new Track {
                        Name = "Jingles"
                    }
                },
                Genre = "Jazz",
                IsCompilation = true
            };
        }
    }
}