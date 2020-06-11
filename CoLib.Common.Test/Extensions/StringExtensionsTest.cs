using System.Text;
using CoLib.Common.Extensions;
using FluentAssertions;
using Xunit;

namespace CoLib.Common.Test.Extensions {

  public class StringExtensionsTest {

    [Fact]
    public void TrimOrEmpty_GiveNull() {
      string target = null;

      string result = target.TrimOrEmpty();

      result.Should().Be("");
    }

    [Fact]
    public void TrimOrEmpty_GiveEmpty() {
      var target = "";

      string result = target.TrimOrEmpty();

      result.Should().Be("");
    }

    [Fact]
    public void TrimOrEmpty_GiveSpace() {
      var target = "   ";

      var result = target.TrimOrEmpty();

      result.Should().Be("");
    }


    [Fact]
    public void TrimOrEmpty_GiveSpaceBeginning() {
      var target = "     Hello world";
      var expected = "Hello world";

      var result = target.TrimOrEmpty();

      result.Should().Be(expected);
    }

    [Fact]
    public void TrimOrEmpty_GiveSpaceEnd() {
      var target = "Hello world     ";
      var expected = "Hello world";

      var result = target.TrimOrEmpty();

      result.Should().Be(expected);
    }

    [Fact]
    public void TrimOrEmpty_GiveSpaceBoth() {
      var target = "     Hello world     ";
      var expected = "Hello world";

      var result = target.TrimOrEmpty();

      result.Should().Be(expected);
    }

    [Fact]
    public void ToBase64String_UseUTF8Encoding() {
      var input = "Hello world!";
      var expected = "SGVsbG8gd29ybGQh";

      var result = input.ToBase64(Encoding.UTF8);

      result.Should().Be(expected);
    }

    [Fact]
    public void FromBase64String_UseUTF8Encoding() {
      var input = "SGVsbG8gd29ybGQh";
      var expected = "Hello world!";

      var result = input.FromBase64(Encoding.UTF8);

      result.Should().Be(expected);
    }


    [Theory]
    [InlineData("Hello World!", "ed076287532e86365e841e92bfc50d8c")]
    [InlineData("abcABC0123", "33c3a6a3b6092d9a776329ff0893b880")]
    public void ToMD5(string input, string expected) {
      var result = input.ToMD5();

      result.Should().Be(expected);
    }

    [Theory]
    [InlineData("Hello World!", "7f83b1657ff1fc53b92dc18148a1d65dfc2d4b1fa3d677284addd200126d9069")]
    [InlineData("abcABC0123", "85a94e7f8b0ea8a233d33099503e0dbf7b855c23fd9d7ed9d9d3966e67790bae")]
    public void ToSHA256(string input, string expected) {
      var result = input.ToSHA256();

      result.Should().Be(expected);
    }

    [Theory]
    [InlineData("Hello World!",
                 "861844d6704e8573fec34d967e20bcfef3d424cf48be04e6dc08f2bd58c729743371015ead891cc3cf1c9d34b49264b510751b1ff9e537937bc46b5d6ff4ecc8")]
    [InlineData("abcABC0123",
                 "8ccc9f4f6ba6f2a070c529a3babf66f74f2b318ed984da04045802a54345995208b8bae260973f0053349e7c6148de794fabe25d65e891599b7ad0a21b2561bd")]
    public void ToSHa512(string input, string expected) {
      var result = input.ToSHA512();

      result.Should().Be(expected);
    }

  }

}
