using System;
using System.Linq;
using ITunesLibraryParser;
using NUnit.Framework;

namespace ITunesLibraryParserTests
{
    [TestFixture]
    public class ITunesLibraryTest
    {
        private ITunesLibrary subject;
        private const string Filepath = @"sampleiTunesLibrary.xml";

        [SetUp]
        public void Setup()
        {
            var basedir = AppDomain.CurrentDomain.BaseDirectory;
            var file = System.IO.Path.Combine(basedir, Filepath);
            subject = new ITunesLibrary(file);
        }

        [TearDown]
        public void TearDown()
        {
            subject = null;
        }

        [Test]
        public void Tracks_Parses_All_Results()
        {
            Assert.That(subject.Tracks.Count(), Is.EqualTo(25));
        }

        [Test]
        public void Tracks_Parses_All_Fields_On_Track()
        {
            var result = subject.Tracks.First();
            Assert.That(result.TrackId, Is.EqualTo(17714));
            Assert.That(result.Name, Is.EqualTo("Dream Gypsy"));
            Assert.That(result.Artist, Is.EqualTo("Bill Evans & Jim Hall"));
            Assert.That(result.Composer, Is.EqualTo("Judith Veevers"));
            Assert.That(result.Album, Is.EqualTo("Undercurrent"));
            Assert.That(result.Genre, Is.EqualTo("Jazz"));
            Assert.That(result.Kind, Is.EqualTo("AAC audio file"));
            Assert.That(result.Size, Is.EqualTo(11550486));
            Assert.That(result.TrackNumber, Is.EqualTo(3));
            Assert.That(result.Year, Is.EqualTo(1962));
            Assert.That(result.DateModified.Value.Date, Is.EqualTo(new DateTime(2012, 2, 25)));
            Assert.That(result.DateAdded.Value.Date, Is.EqualTo(new DateTime(2012, 2, 25)));
            Assert.That(result.BitRate, Is.EqualTo(320));
            Assert.That(result.SampleRate, Is.EqualTo(44100));
            Assert.That(result.PlayCount, Is.EqualTo(11));
            Assert.That(result.PlayDate.Value.Date, Is.EqualTo(new DateTime(2012, 8, 16)));
            Assert.That(result.PartOfCompilation, Is.True);
        }

        [Test]
        public void Tracks_populates_null_values_for_nonexistent_elements()
        {
            var result = subject.Tracks.First();
            Assert.That(result.AlbumArtist, Is.Null.Or.Empty);
        }

        [Test]
        public void Tracks_sets_boolean_properties_to_false_for_nonexistent_boolean_nodes()
        {
            Assert.That(subject.Tracks.Count(t => t.PartOfCompilation), Is.EqualTo(2));
        }

        [Test]
        public void Tracks_Converts_Milliseconds_TotalTime_To_String_Playing_Time_Minutes_And_Seconds()
        {
            var result = subject.Tracks.First();
            Assert.That(result.PlayingTime, Is.EqualTo("4:35"));
        }

        [Test]
        public void Albums_FiltersAlbumNames()
        {
            Assert.AreEqual(5, subject.Albums.Count());
        }

        [Test]
        public void Artists_FiltersArtists()
        {
            Assert.AreEqual(6, subject.Artists.Count());
        }

        [Test]
        public void Geneires_FiltersGeneires()
        {
            Assert.AreEqual(1, subject.Geneires.Count());
        }

        [Test]
        public void Years_FiltersYears()
        {
            Assert.AreEqual(5, subject.Years.Count());
        }
    }
}
