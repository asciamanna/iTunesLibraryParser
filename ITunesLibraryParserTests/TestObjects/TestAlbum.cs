using System.Collections.Generic;
using ITunesLibraryParser;

namespace ITunesLibraryParserTests.TestObjects {
    public class TestAlbum {
        public static Album Create() {
            return new Album {
                AlbumName = "Bags Meets Wes",
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