using System.Text;
using CoLib.Common.Extensions;
using Xunit;

namespace CoLib.Common.Test.Extensions {

  public class StringExtensionsTest {

    [Fact]
    public void TrimOrEmpty_GiveNull() {
      string target = null;

      string result = target.TrimOrEmpty();

      Assert.Equal("", result);
    }

    [Fact]
    public void TrimOrEmpty_GiveEmpty() {
      var target = "";

      string result = target.TrimOrEmpty();

      Assert.Equal("", result);
    }

    [Fact]
    public void TrimOrEmpty_GiveSpace() {
      var target = "   ";

      var result = target.TrimOrEmpty();

      Assert.Equal("", result);
    }


    [Fact]
    public void TrimOrEmpty_GiveSpaceBeginning() {
      var target = "     Hello world";
      var expected = "Hello world";

      var result = target.TrimOrEmpty();

      Assert.Equal(expected, result);
    }

    [Fact]
    public void TrimOrEmpty_GiveSpaceEnd() {
      var target = "Hello world     ";
      var expected = "Hello world";

      var result = target.TrimOrEmpty();

      Assert.Equal(expected, result);
    }

    [Fact]
    public void TrimOrEmpty_GiveSpaceBoth() {
      var target = "     Hello world     ";
      var expected = "Hello world";

      var result = target.TrimOrEmpty();

      Assert.Equal(expected, result);
    }

    [Fact]
    public void ToBase64String_UseUTF8Encoding() {
      var input = "Hello world!";
      var expected = "SGVsbG8gd29ybGQh";

      var result = input.ToBase64(Encoding.UTF8);

      Assert.Equal(expected, result);
    }

    [Fact]
    public void FromBase64String_UseUTF8Encoding() {
      var input = "SGVsbG8gd29ybGQh";
      var expected = "Hello world!";

      var result = input.FromBase64(Encoding.UTF8);

      Assert.Equal(expected, result);
    }

  }

}