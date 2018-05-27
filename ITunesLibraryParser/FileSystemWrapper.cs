using System.IO;

namespace ITunesLibraryParser {
    public interface IFileSystem {
        string ReadXmlTextFromFile(string fileLocation);
    }

    public class FileSystemWrapper : IFileSystem {
        public string ReadXmlTextFromFile(string fileLocation) {
            return File.ReadAllText(fileLocation);
        }
    }
}