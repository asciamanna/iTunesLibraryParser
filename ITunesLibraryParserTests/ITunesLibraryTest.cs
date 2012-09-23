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
      var results = ITunesLibrary.Parse(@".\iTunesMusicLibrary.xml");

    }
  }
}
