using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ITunesLibraryParser
{
    public class ITunesLibrary : IITunesLibrary
    {

        private XDocument _xml;

        private Track CreateTrack(XElement trackElement)
        {
            return new Track
            {
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

        private IEnumerable<XElement> LoadTrackElements()
        {
            return from x in _xml.Descendants("dict")
                   .Descendants("dict")
                   .Descendants("dict")
                   where x.Descendants("key").Count() > 1
                   select x;
        }

        public ITunesLibrary(string fileLocation)
        {
            _xml = XDocument.Load(fileLocation);
        }

        public IEnumerable<Track> Tracks
        {
            get
            {
                var trackElements = LoadTrackElements();
                return trackElements.Select(CreateTrack);
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

        public IEnumerable<string> Geneires
        {
            get { return Tracks.Select(t => t.Genre).OrderBy(t => t).Distinct(); }
        }

        public IEnumerable<string> Years
        {
            get { return Tracks.Select(t => t.Year.ToString()).OrderBy(t => t).Distinct(); }
        }



        public IEnumerable<Track> Filter(FilterKind kind, string param)
        {
            switch (kind)
            {
                case FilterKind.Album:
                    return Tracks.Where(t => t.Album == param);
                case FilterKind.Artist:
                    return Tracks.Where(t => t.Artist == param);
                case FilterKind.Geneires:
                    return Tracks.Where(t => t.Genre == param);
                case FilterKind.Year:
                    return Tracks.Where(t => t.Year == int.Parse(param));
                case FilterKind.None:
                default:
                    return Tracks;
            }
        }
    }
}
