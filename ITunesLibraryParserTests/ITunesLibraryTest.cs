using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using ITunesLibraryParser;

namespace ITunesLibraryParserTests {
  [TestFixture]
  public class ITunesLibraryTest {
    [Test]
    public void Parse() {
      var tracks = new ITunesLibrary().Parse(@".\sampleiTunesLibrary.xml");
      Assert.AreEqual(25, tracks.Count());
      var track = tracks.First();
      Assert.AreEqual(17714, track.TrackId);
      Assert.AreEqual("Dream Gypsy", track.Name);
      Assert.AreEqual("Bill Evans & Jim Hall", track.Artist);
      Assert.AreEqual("Judith Veevers", track.Composer);
      Assert.AreEqual("Undercurrent", track.Album);
      Assert.AreEqual("Jazz", track.Genre);
      Assert.AreEqual("AAC audio file", track.Kind);
      Assert.AreEqual(11550486, track.Size);
      Assert.AreEqual(274906, track.TotalTime);
      Assert.AreEqual(3, track.TrackNumber);
      Assert.AreEqual(1962, track.Year);
      Assert.AreEqual(new DateTime(2012, 2, 25), track.DateModified.Value.Date);
      Assert.AreEqual(new DateTime(2012, 2, 25), track.DateAdded.Value.Date);
      Assert.AreEqual(320, track.BitRate);
      Assert.AreEqual(44100, track.SampleRate);
      Assert.AreEqual(11, track.PlayCount);
      Assert.AreEqual(new DateTime(2012, 8, 15), track.PlayDate.Value.Date);
      Assert.IsTrue(track.PartOfCompilation);
    }

    [Test]
    public void Parse_populates_null_values_for_nonexistent_elements() {
      var firstTrack = new ITunesLibrary().Parse(@".\SampleiTunesLibrary.xml").First();
      Assert.IsTrue(String.IsNullOrEmpty(firstTrack.AlbumArtist));
    }

    [Test]
    public void Parse_sets_boolean_properties_to_false_for_nonexistent_boolean_nodes() {
      var tracks = new ITunesLibrary().Parse(@".\SampleiTunesLibrary.xml");
      Assert.AreEqual(2, tracks.Count(t => t.PartOfCompilation));
    }
  }
}
