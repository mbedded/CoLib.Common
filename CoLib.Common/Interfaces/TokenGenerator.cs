using System;

namespace CoLib.Common.Interfaces {

  /// <summary>
  ///   THis interface defines methods for a token generator.
  /// </summary>
  public interface ITokenGenerator {

    /// <summary>
    ///   This method will generate a random string by using a default length.
    /// </summary>
    /// <returns>
    ///   Returns a random string with a default length.
    /// </returns>
    string GenerateToken();

    /// <summary>
    ///   This method will generate a random token by using a given length.
    /// </summary>
    /// <returns>
    ///   Returns a random string with a given length.
    /// </returns>
    /// <exception cref="ArgumentException">
    ///   Exception will be thrown if the length has an invalid value.
    ///   Length has to be bigger than 0.
    /// </exception>
    string GenerateToken(int length);

  }

}
