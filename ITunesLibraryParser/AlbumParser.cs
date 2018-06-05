using System.Collections.Generic;
using System.Linq;

namespace ITunesLibraryParser {
    internal class AlbumParser {
        private static string CompilationArtistName = "Various Artists";

        internal IEnumerable<Album> ParseAlbums(IEnumerable<Track> tracks) {
            var albumGroups = tracks.GroupBy(t => t.Album);
            var albums = new List<Album>();
            foreach (var albumGroup in albumGroups) {
                if (MultipleAlbumsWithSameNameExist(albumGroup) && !IsCompilation(albumGroup))
                    SubGroupByArtistToCreateAlbums(albumGroup, albums);
                else
                    albums.Add(CreateAlbum(albumGroup));
            }
            return albums;
        }

        private static bool MultipleAlbumsWithSameNameExist(IGrouping<string, Track> albumGroup) {
            return albumGroup.Select(t => t.Artist).Distinct().Count() > 1 && 
                   (albumGroup.Select(t => t.AlbumArtist).Distinct().Count() > 1 ||
                    albumGroup.All(t => string.IsNullOrWhiteSpace(t.AlbumArtist)));
        }

        private static bool IsCompilation(IEnumerable<Track> albumGroup) {
            return albumGroup.Any(t => t.PartOfCompilation || t.AlbumArtist == CompilationArtistName);
        }

        private static void SubGroupByArtistToCreateAlbums(IEnumerable<Track> albumGroup, List<Album> albums) {
            var groupByArtist = albumGroup.ToList().GroupBy(t => t.Artist);
            albums.AddRange(groupByArtist.Select(CreateAlbum));
        }

        private static Album CreateAlbum(IEnumerable<Track> tracks) {
            return new Album {
                Name = tracks.First().Album,
                Genre = tracks.First().Genre,
                Artist = GetArtistName(tracks),
                Year = tracks.First().Year,
                IsCompilation = tracks.First().PartOfCompilation,
                Tracks = tracks.ToList()
            };
        }

        private static string GetArtistName(IEnumerable<Track> tracks) {
            if (tracks.Any(t => !string.IsNullOrWhiteSpace(t.AlbumArtist)))
                 return tracks.Select(t => t.AlbumArtist).First(aa => !string.IsNullOrWhiteSpace(aa));
            return tracks.Any(t => t.PartOfCompilation) ? CompilationArtistName : tracks.Select(t => t.Artist).First();
        }
    }
}