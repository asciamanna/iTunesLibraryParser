using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace ITunesLibraryParser {
    internal abstract class LibraryItemParserBase {
        protected IEnumerable<XElement> ParseElements(string libraryContents) {
            return from x in XDocument.Parse(libraryContents).Descendants("dict").Descendants(GetCollectionNodeName()).Descendants("dict")
                where x.Descendants("key").Count() > 1
                select x;
        }

        protected abstract string GetCollectionNodeName();
    }
}