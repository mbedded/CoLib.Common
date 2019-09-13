using System;
using CoLib.Common.Generator;
using CoLib.Common.Interfaces;
using Xunit;

namespace CoLib.Common.Test.Generator {

  public class TokenGeneratorTest {

    private ITokenGenerator GetInstance() {
      return new TokenGenerator();
    }

    [Fact]
    private void CreateInstance() {
      var target = GetInstance();

      Assert.NotNull(target);
    }

    [Fact]
    private void GetDefaultToken() {
      var expectedLength = 8;
      var target = GetInstance();

      var result = target.GenerateToken();

      Assert.Equal(expectedLength, result.Length);
      Assert.DoesNotContain(" ", result);
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(-1)]
    [InlineData(0)]
    private void GetToken_GiveInvalidLength_ExpectArgumentException(int length) {
      var target = GetInstance();

      Assert.Throws<ArgumentException>(() => { target.GenerateToken(length); });
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(100)]
    private void GetToken_GiveValidLength_ExpectStringHasGivenLength(int length) {
      var target = GetInstance();

      var result = target.GenerateToken(length);

      Assert.Equal(length, result.Length);
      Assert.DoesNotContain(" ", result);
    }

  }

}
