using System.Collections.Generic;
using System.Linq;

namespace ITunesLibraryParser {
    internal class AlbumParser {
        internal IEnumerable<Album> ParseAlbums(IEnumerable<Track> tracks) {
            var albumGroups = tracks.GroupBy(t => $"{t.Album} - {t.AlbumArtist}");
            var albums = new List<Album>();
            foreach (var albumGroup in albumGroups) {
                if (albumGroup.Select(t => t.Artist).Distinct().Count() > 1 && albumGroup.All(t => string.IsNullOrWhiteSpace(t.AlbumArtist))) {
                    var groupedByAlbumAndArtist = albumGroup.ToList().GroupBy(t => t.Artist);
                    foreach (var group in groupedByAlbumAndArtist) {
                        albums.Add(new Album {
                            AlbumName = group.First().Album,
                            AlbumArtist = group.First().AlbumArtist,
                            Genre = group.First().Genre,
                            Artist = group.First().Artist,
                            Year = group.First().Year,
                            IsCompilation = group.First().PartOfCompilation,
                            Tracks = group.ToList()
                        });
                    }
                }
                else {
                    albums.Add(new Album {
                        AlbumName = albumGroup.First().Album,
                        AlbumArtist = albumGroup.First().AlbumArtist,
                        Artist = albumGroup.First().Artist,
                        Genre = albumGroup.First().Genre,
                        IsCompilation = albumGroup.First().PartOfCompilation,
                        Year = albumGroup.First().Year,
                        Tracks = albumGroup.ToList()
                    });
                }
            }
            return albums;
        }
    }
}