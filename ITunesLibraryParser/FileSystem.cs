using System.IO;

namespace ITunesLibraryParser {
    public class FileSystem {
        public static string ReadTextFromFile(string fileLocation) {
            return File.ReadAllText(fileLocation);
        }
    }
}