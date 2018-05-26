using System.Collections.Generic;

namespace ITunesLibraryParser {
   public class Playlist {
       public string Name { get; set; }
       public IEnumerable<Track> Tracks { get; set; }
    }
}
