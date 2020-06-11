using System;
using CoLib.Common.Generator;
using CoLib.Common.Interfaces;
using FluentAssertions;
using Xunit;

namespace CoLib.Common.Test.Generator {

  public class TokenGeneratorTest {

    private ITokenGenerator GetInstance() {
      return new TokenGenerator();
    }

    [Fact]
    private void CreateInstance() {
      var target = GetInstance();

      target.Should().NotBeNull();
    }

    [Fact]
    private void GetDefaultToken() {
      var expectedLength = 8;
      var target = GetInstance();

      var result = target.GenerateToken();

      result.Length.Should().Be(expectedLength);
      result.Should().NotContain(" ", "because space is no valid character");
    }

    [Theory]
    [InlineData(-10)]
    [InlineData(-1)]
    [InlineData(0)]
    private void GetToken_GiveInvalidLength_ExpectArgumentException(int length) {
      var target = GetInstance();

      target.Invoking(x => x.GenerateToken(length))
            .Should().Throw<ArgumentException>("because length must be greater than 0");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(5)]
    [InlineData(100)]
    private void GetToken_GiveValidLength_ExpectStringHasGivenLength(int length) {
      var target = GetInstance();

      var result = target.GenerateToken(length);

      result.Length.Should().Be(length);
      result.Should().NotContain(" ", "because space is no valid character");
    }

  }

}
