using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ITunesLibraryParser;

namespace ITunesLibraryParserTests {
  [TestFixture]
  public class ITunesLibraryTest {
    IEnumerable<Track> tracks;
   
    [SetUp]
    public void Setup() {
      tracks = new ITunesLibrary().Parse(@".\sampleiTunesLibrary.xml");
    }

    [Test]
    public void Parse() {
      Assert.That(tracks.Count(), Is.EqualTo(25));

      var track = tracks.First();
      Assert.That(track.TrackId, Is.EqualTo(17714));
      Assert.That(track.Name, Is.EqualTo("Dream Gypsy"));
      Assert.That(track.Artist, Is.EqualTo("Bill Evans & Jim Hall"));
      Assert.That(track.Composer, Is.EqualTo("Judith Veevers"));
      Assert.That(track.Album, Is.EqualTo("Undercurrent"));
      Assert.That(track.Genre, Is.EqualTo("Jazz"));
      Assert.That(track.Kind, Is.EqualTo("AAC audio file"));
      Assert.That(track.Size, Is.EqualTo(11550486));
      Assert.That(track.TrackNumber, Is.EqualTo(3));
      Assert.That(track.Year, Is.EqualTo(1962));
      Assert.That(track.DateModified.Value.Date, Is.EqualTo(new DateTime(2012, 2, 25)));
      Assert.That(track.DateAdded.Value.Date, Is.EqualTo(new DateTime(2012, 2, 25)));
      Assert.That(track.BitRate, Is.EqualTo(320));
      Assert.That(track.SampleRate, Is.EqualTo(44100));
      Assert.That(track.PlayCount, Is.EqualTo(11));
      Assert.That(track.PlayDate.Value.Date, Is.EqualTo(new DateTime(2012, 8, 15)));
      Assert.That(track.PartOfCompilation, Is.True);
    }

    [Test]
    public void Parse_populates_null_values_for_nonexistent_elements() {
      var firstTrack = tracks.First();
      Assert.That(String.IsNullOrEmpty(firstTrack.AlbumArtist));
    }

    [Test]
    public void Parse_sets_boolean_properties_to_false_for_nonexistent_boolean_nodes() {
      Assert.That(tracks.Count(t => t.PartOfCompilation), Is.EqualTo(2));
    }

    [Test]
    public void Parse_Converts_Milliseconds_TotalTime_To_String_Playing_Time_Minutes_And_Seconds() {
      var track = tracks.First();
      Assert.That(track.PlayingTime, Is.EqualTo("4:35"));
    }
  }
}
