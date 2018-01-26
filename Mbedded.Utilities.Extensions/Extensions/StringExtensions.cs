namespace Mbedded.Utilities.Extensions {

  public static class StringExtensions {

    /// <summary>
    ///   This method tries to Trim a srtring.
    ///   If the given parameter is null, an empty string "" will be returned.
    /// </summary>
    /// <param name="xString">The string which should be trimmed</param>
    /// <returns>xString.Trim() or "" if xString is null.</returns>
    public static string TrimOrEmpty(this string xString) {
      return string.IsNullOrEmpty(xString) ? "" : xString.Trim();
    }

  }

}