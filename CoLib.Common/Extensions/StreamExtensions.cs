using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CoLib.Common.Extensions {

  /// <summary>
  ///   This class contains several methods for strings.
  /// </summary>
  public static class StreamExtensions {

    /// <summary>
    ///   Creates an <see cref="MD5"/> hash based on the input.
    /// </summary>
    /// <param name="input">The stream to hash.</param>
    /// <returns>Returns the hex-string of the calculated hash.</returns>
    public static string ToMD5(this Stream input) {
      using MD5 hash = MD5.Create();
      return input.CreateHash(hash);
    }

    /// <summary>
    ///   Creates an <see cref="SHA256"/> hash based on the input.
    /// </summary>
    /// <param name="input">The stream to hash.</param>
    /// <returns>Returns the hex-string of the calculated hash.</returns>
    public static string ToSHA256(this Stream input) {
      using SHA256 hash = SHA256.Create();
      return input.CreateHash(hash);
    }

    /// <summary>
    ///   Creates an <see cref="SHA512"/> hash based on the input.
    /// </summary>
    /// <param name="input">The stream to hash.</param>
    /// <returns>Returns the hex-string of the calculated hash.</returns>
    public static string ToSHA512(this Stream input) {
      using SHA512 hash = SHA512.Create();
      return input.CreateHash(hash);
    }

    private static string CreateHash(this Stream input, HashAlgorithm algorithm) {
      if (input == null) {
        return string.Empty;
      }

      long position = input.Position;
      input.Position = 0;

      StringBuilder builder = new StringBuilder();
      byte[] res = algorithm.ComputeHash(input);

      foreach (byte i in res) {
        builder.Append(i.ToString("x2"));
      }

      input.Position = position;

      return builder.ToString();
    }

  }

}
