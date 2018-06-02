using System;
using System.Collections.Generic;
using System.Linq;

namespace ITunesLibraryParser {
    internal class AlbumParser {
        internal IEnumerable<Album> ParseAlbums(IEnumerable<Track> tracks) {
            var albumGroups = tracks.GroupBy(t => $"{t.Album} - {t.AlbumArtist}");
            var albums = new List<Album>();
            foreach (var albumGroup in albumGroups) {
                if (MultipleAlbumsWithSameNameExist(albumGroup)) {
                    SubGroupByArtistToCreateAlbums(albumGroup, albums);
                }
                else {
                    albums.Add(CreateAlbum(albumGroup));
                }
            }
            return albums;
        }

        private static bool MultipleAlbumsWithSameNameExist(IGrouping<string, Track> albumGroup) {
            return albumGroup.Select(t => t.Artist).Distinct().Count() > 1 && albumGroup.All(t => string.IsNullOrWhiteSpace(t.AlbumArtist));
        }

        private static void SubGroupByArtistToCreateAlbums(IEnumerable<Track> albumGroup, List<Album> albums) {
            var groupByArtist = albumGroup.ToList().GroupBy(t => t.Artist);
            albums.AddRange(groupByArtist.Select(CreateAlbum));
        }

        private static Album CreateAlbum(IGrouping<string, Track> grouping) {
            return new Album {
                AlbumName = grouping.First().Album,
                AlbumArtist = grouping.First().AlbumArtist,
                Genre = grouping.First().Genre,
                Artist = grouping.First().Artist,
                Year = grouping.First().Year,
                IsCompilation = grouping.First().PartOfCompilation,
                Tracks = grouping.ToList()
            };
        }
    }
}