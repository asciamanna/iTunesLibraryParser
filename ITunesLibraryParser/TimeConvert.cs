using System;

namespace ITunesLibraryParser {
  public static class TimeConvert {
    public static string MillisecondsToFormattedMinutesAndSeconds(long milliseconds) {
      var totalSeconds = Math.Round(TimeSpan.FromMilliseconds(milliseconds).TotalSeconds);
      var minutes = (int)(totalSeconds / 60);
      var seconds = (int)(totalSeconds - (minutes * 60));
      var timespan = new TimeSpan(0, minutes, seconds);
      return timespan.ToString("m\\:ss");
    }
  }
}