using System.IO;

namespace ITunesLibraryParser {
    public interface IFileSystem {
        string ReadTextFromFile(string fileLocation);
    }

    public class FileSystem : IFileSystem {
        public string ReadTextFromFile(string fileLocation) {
            return File.ReadAllText(fileLocation);
        }
    }
}