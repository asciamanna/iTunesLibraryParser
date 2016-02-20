using System;

namespace ITunesLibraryParser {
  public static class TimeConvert {
    public static string MillisecondsToFormattedMinutesAndSeconds(long milliseconds) {
      var totalSeconds = ConvertToSeconds(milliseconds);
      var minutes = CalculateTotalMinutes(totalSeconds);
      var seconds = CalculateRemainingSeconds(totalSeconds, minutes);
      return CreateFormattedTime(minutes, seconds);
    }

    private static double ConvertToSeconds(long milliseconds) {
      return Math.Round(TimeSpan.FromMilliseconds(milliseconds).TotalSeconds);
    }

    private static int CalculateTotalMinutes(double totalSeconds) {
      return (int)(totalSeconds / 60);
    }

    private static int CalculateRemainingSeconds(double totalSeconds, int minutes) {
      return (int)(totalSeconds - (minutes * 60));
    }

    private static string CreateFormattedTime(int minutes, int seconds) {
      return new TimeSpan(0, minutes, seconds).ToString("m\\:ss");
    }
  }
}