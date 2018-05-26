﻿using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;

namespace ITunesLibraryParser.EndToEndTests
{
    [TestFixture]
    public class EndToEndTest
    {
        private readonly string FileLocation = Path.Combine(TestContext.CurrentContext.TestDirectory,
            "sampleiTunesLibrary.xml");
        private ITunesLibrary subject;

        [SetUp]
        public void SetUp() {
            subject = new ITunesLibrary();
        }

        [Test]
        public void Parser_Reads_Library_From_Filesystem_And_Parses() {
            var results = subject.Parse(FileLocation);

            var result = results.First();
            Assert.That(result.TrackId, Is.EqualTo(17714));
            Assert.That(result.Name, Is.EqualTo("Dream Gypsy"));
            Assert.That(result.Artist, Is.EqualTo("Bill Evans & Jim Hall"));
        }
    }
}
