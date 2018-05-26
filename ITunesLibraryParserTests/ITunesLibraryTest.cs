using System;
using System.Linq;
using ITunesLibraryParser;
using ITunesLibraryParserTests.TestObjects;
using Moq;
using NUnit.Framework;

namespace ITunesLibraryParserTests {
    [TestFixture]
    public class ITunesLibraryTest {

        private Mock<IFileSystem> fileSystem;
        private ITunesLibrary subject;
        private const string Filepath = "itunes-library-file-location";

        [SetUp]
        public void Setup() {
            fileSystem = new Mock<IFileSystem>();
            subject = new ITunesLibrary(fileSystem.Object);
        }

        [Test]
        public void Parse_Parses_All_Results() {
            fileSystem.Setup(fs => fs.ReadTextFromFile(Filepath)).Returns(TestLibraryData.Create());

            var result = subject.Parse(Filepath);

            Assert.That(result.Count(), Is.EqualTo(25));
        }

        [Test]
        public void Parse_Parses_All_Fields_On_Track() {
            fileSystem.Setup(fs => fs.ReadTextFromFile(Filepath)).Returns(TestLibraryData.Create());

            var result = subject.Parse(Filepath).First();

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
            Assert.That(result.PlayDate.Value.Date, Is.EqualTo(new DateTime(2012, 8, 15)));
            Assert.That(result.PartOfCompilation, Is.True);
        }

        [Test]
        public void Parse_populates_null_values_for_nonexistent_elements() {
            fileSystem.Setup(fs => fs.ReadTextFromFile(Filepath)).Returns(TestLibraryData.Create());

            var result = subject.Parse(Filepath).First();

            Assert.That(result.AlbumArtist, Is.Null.Or.Empty);
        }

        [Test]
        public void Parse_sets_boolean_properties_to_false_for_nonexistent_boolean_nodes() {
            fileSystem.Setup(fs => fs.ReadTextFromFile(Filepath)).Returns(TestLibraryData.Create());

            Assert.That(subject.Parse(Filepath).Count(t => t.PartOfCompilation), Is.EqualTo(2));
        }

        [Test]
        public void Parse_Converts_Milliseconds_TotalTime_To_String_Playing_Time_Minutes_And_Seconds() {
            fileSystem.Setup(fs => fs.ReadTextFromFile(Filepath)).Returns(TestLibraryData.Create());

            var result = subject.Parse(Filepath).First();
            Assert.That(result.PlayingTime, Is.EqualTo("4:35"));
        }
    }
}
