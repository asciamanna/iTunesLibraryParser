using System;

namespace ITunesLibraryParser.Tests.TestObjects {
    public static class TrackMother {
        public static Track Create() {
            return new Track {
                TrackId = 456,
                Name = "Witch Hunt",
                Artist = "Wayne Shorter",
                AlbumArtist = "Wayne Shorter",
                Composer = "Wayne Shorter",
                Album = "Speak No Evil",
                Genre = "Jazz",
                Kind = "AAC Audio File",
                Size = 5768594,
                PlayingTime = "4:35",
                TrackNumber = 1,
                Year = 1964,
                DateModified = DateTime.Today,
                DateAdded = DateTime.Today,
                BitRate = 330,
                SampleRate = 44100,
                PlayCount = 55,
                PlayDate = DateTime.Today,
                PartOfCompilation = false,
                Location = "file://localhost/C:/Users/anthony/Music/iTunes/iTunes%20Music/Wayne%20Shorter/Speak%20No%20Evil/01%20Witch%20Hunt.m4a",
                Rating = 50,
                AlbumRatingComputed = true,
                AlbumRating = 60
            };
        }
    }
}
