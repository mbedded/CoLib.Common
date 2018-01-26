using Mbedded.Utilities.Extensions;
using Xunit;

namespace Mbedded.Utilities.Extensions_Test.Extensions {

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


  }

}