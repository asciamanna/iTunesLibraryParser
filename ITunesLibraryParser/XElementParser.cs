using System;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

namespace ITunesLibraryParser {
    internal static class XElementParser {
        internal static bool ParseBoolean(XElement track, string keyValue) {
            return (from keyNode in track.Descendants("key")
                    where keyNode.Value == keyValue
                    select (keyNode.NextNode as XElement).Name).FirstOrDefault() == "true";
        }

        internal static string ParseStringValue(XElement track, string keyValue) {
            return (from key in track.Descendants("key")
                    where key.Value == keyValue
                    select (key.NextNode as XElement).Value).FirstOrDefault();
        }

        internal static long? ParseNullableLongValue(XElement track, string keyValue) {
            var stringValue = ParseStringValue(track, keyValue);
            return string.IsNullOrEmpty(stringValue) ? (long?)null : long.Parse(stringValue);
        }

        internal static int? ParseNullableIntValue(XElement track, string keyValue) {
            var stringValue = ParseStringValue(track, keyValue);
            return string.IsNullOrEmpty(stringValue) ? (int?)null : int.Parse(stringValue);
        }

        internal static DateTime? ParseNullableDateValue(XElement track, string keyValue) {
            var stringValue = ParseStringValue(track, keyValue);
            return string.IsNullOrEmpty(stringValue) ? (DateTime?)null : DateTime.SpecifyKind(DateTime.Parse(stringValue, CultureInfo.InvariantCulture), DateTimeKind.Utc).ToLocalTime();
        }
    }
}