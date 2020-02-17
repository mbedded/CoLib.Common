using System;
using CoLib.Common.Interfaces;

namespace CoLib.Common.Generator {

  /// <summary>
  ///   This class returns random strings with default
  ///   or given length.
  /// </summary>
  public class TokenGenerator : ITokenGenerator {

    private const int DEFAULT_LENGTH = 8;
    private const string ALLOWED_CHARACTERS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
    private static readonly int NumberOfAllowedCharacters = ALLOWED_CHARACTERS.Length;

    private readonly Random _random;

    /// <summary>
    ///   Creates a new instance.
    /// </summary>
    public TokenGenerator() {
      _random = new Random();
    }

    /// <inheritdoc cref="ITokenGenerator.GenerateToken()" />
    public string GenerateToken() {
      return GenerateToken(DEFAULT_LENGTH);
    }

    /// <inheritdoc cref="ITokenGenerator.GenerateToken(int)" />
    public string GenerateToken(int length) {
      if (length <= 0) {
        throw new ArgumentException("The requested length is invalid. Length has to be 1 at least.", nameof(length));
      }

      char[] result = new char[length];

      for (int index = 0; index < length; index++) {
        int randomValue = _random.Next(0, NumberOfAllowedCharacters);
        result[index] = ALLOWED_CHARACTERS[randomValue];
      }

      return new string(result);
    }

  }

}
