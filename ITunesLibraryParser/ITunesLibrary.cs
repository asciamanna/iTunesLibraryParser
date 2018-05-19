using ITunesLibraryParser.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ITunesLibraryParser
{
    public class ITunesLibrary : IITunesLibrary
    {

        private XDocument _xml;
        private List<Track> _tracks;

        public ITunesLibrary(string fileLocation)
        {
            _xml = XDocument.Load(fileLocation);
        }

        private IEnumerable<XElement> LoadTrackElements()
        {
            return from x in _xml.Descendants("dict")
                   .Descendants("dict")
                   .Descendants("dict")
                   where x.Descendants("key").Count() > 1
                   select x;
        }

        private IEnumerable<XElement> LoadPlaylists()
        {
            return from x in _xml.Descendants("dict")
                   .Descendants("array")
                   .Descendants("dict")
                   where x.Descendants("key").Count() > 1
                   select x;
        }

        public IEnumerable<Track> Tracks
        {
            get
            {
                if (_tracks == null)
                {
                    var trackElements = LoadTrackElements();
                    _tracks = trackElements.Select(TrackFactory.CreateTrack).ToList();
                }
                return _tracks;
            }
        }

        public IEnumerable<string> Albums
        {
            get { return Tracks.Select(t => t.Album).OrderBy(t => t).Distinct(); }
        }

        public IEnumerable<string> Artists
        {
            get { return Tracks.Select(t => t.Artist).OrderBy(t => t).Distinct(); }
        }

        public IEnumerable<string> Genres
        {
            get { return Tracks.Select(t => t.Genre).OrderBy(t => t).Distinct(); }
        }

        public IEnumerable<string> Years
        {
            get { return Tracks.Select(t => t.Year.ToString()).OrderBy(t => t).Distinct(); }
        }

        public IEnumerable<string> Playlists
        {
            get
            {
                var playlistNodes = LoadPlaylists();
                foreach (var item in playlistNodes)
                {
                    yield return XElementParser.ParseStringValue(item, "Name");
                }
             }
        }

        public IEnumerable<Track> Filter(FilterKind kind, string param)
        {
            switch (kind)
            {
                case FilterKind.Album:
                    return Tracks.Where(t => t.Album == param);
                case FilterKind.Artist:
                    return Tracks.Where(t => t.Artist == param);
                case FilterKind.Genres:
                    return Tracks.Where(t => t.Genre == param);
                case FilterKind.Year:
                    return Tracks.Where(t => t.Year == int.Parse(param));
                case FilterKind.None:
                default:
                    return Tracks;
            }
        }

        public IEnumerable<Track> ReadPlaylist(string id)
        {
            var playlistNodes = LoadPlaylists();

            var query = from node in playlistNodes
                    where XElementParser.ParseStringValue(node, "Name") == id
                    select node.Descendants("array").Descendants("dict");

            foreach (var item in query)
            {
                foreach (var subitem in item)
                {
                    var trackid = Int32.Parse(XElementParser.ParseStringValue(subitem, "Track ID"));
                    var track = Tracks.Where(t => t.TrackId == trackid).FirstOrDefault();
                    yield return track;
                }
            }
        }
    }
}
