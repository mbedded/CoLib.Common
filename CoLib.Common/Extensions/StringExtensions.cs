﻿using System;
using System.Security.Cryptography;
using System.Text;

namespace CoLib.Common.Extensions {

  /// <summary>
  ///   This class contains several methods for strings.
  /// </summary>
  public static class StringExtensions {

    /// <summary>
    ///   This method tries to Trim a string.
    ///   If the given parameter is null, an empty string "" will be returned.
    /// </summary>
    /// <param name="input">The string which should be trimmed</param>
    /// <returns>xString.Trim() or "" if xString is null.</returns>
    public static string TrimOrEmpty(this string input) {
      return string.IsNullOrEmpty(input) ? "" : input.Trim();
    }

    /// <summary>
    ///   Converts a given string to a Base64 string by given encoding.
    /// </summary>
    /// <param name="input">String to change to Base64</param>
    /// <param name="encoding">Encoding for transformation</param>
    /// <returns>Returns a string containing the Base64 converted result</returns>
    public static string ToBase64(this string input, Encoding encoding) {
      byte[] inputAsBytes = encoding.GetBytes(input);
      return Convert.ToBase64String(inputAsBytes);
    }

    /// <summary>
    ///   Converts a given Base64 string back to normal by given encoding.
    /// </summary>
    /// <param name="base64String">String to change back</param>
    /// <param name="encoding">Encoding for transformation</param>
    /// <returns>Returns the normal string by given Base64 input</returns>
    public static string FromBase64(this string base64String, Encoding encoding) {
      byte[] decodedAsBytes = Convert.FromBase64String(base64String);
      return encoding.GetString(decodedAsBytes);
    }

    /// <summary>
    ///   Creates an <see cref="MD5"/> hash based on the input.
    /// </summary>
    /// <param name="input">The string to hash.</param>
    /// <returns>Returns the hex-string of the calculated hash.</returns>
    public static string ToMD5(this string input) {
      using MD5 hash = MD5.Create();
      return input.CreateHash(hash);
    }

    /// <summary>
    ///   Creates an <see cref="SHA256"/> hash based on the input.
    /// </summary>
    /// <param name="input">The string to hash.</param>
    /// <returns>Returns the hex-string of the calculated hash.</returns>
    public static string ToSHA256(this string input) {
      using SHA256 hash = SHA256.Create();
      return input.CreateHash(hash);
    }

    /// <summary>
    ///   Creates an <see cref="SHA512"/> hash based on the input.
    /// </summary>
    /// <param name="input">The string to hash.</param>
    /// <returns>Returns the hex-string of the calculated hash.</returns>
    public static string ToSHA512(this string input) {
      using SHA512 hash = SHA512.Create();
      return input.CreateHash(hash);
    }

    private static string CreateHash(this string input, HashAlgorithm algorithm) {
      if (string.IsNullOrEmpty(input)) {
        return string.Empty;
      }

      StringBuilder builder = new StringBuilder();
      byte[] bytes = Encoding.UTF8.GetBytes(input);
      byte[] res = algorithm.ComputeHash(bytes);

      foreach (byte i in res) {
        builder.Append(i.ToString("x2"));
      }

      return builder.ToString();
    }

  }

}
