using System.IO;
using System.Net;
using CoLib.Common.FileHandling;
using FluentAssertions;
using Xunit;

namespace CoLib.Common.Test.FileHandlinhg {

  public class AppFileHandlerTest {

    private AppFileHandler GetInstance(string tempPath = "") {
      return string.IsNullOrEmpty(tempPath)
               ? new AppFileHandler()
               : new AppFileHandler(tempPath);
    }

    [Fact]
    public void CreateInstance() {
      var target = GetInstance();

      target.Should().NotBeNull();
    }

    [Fact]
    public void GetTempFileName_ExpectFileIsExisting() {
      var target = GetInstance();

      var result = target.GetTempFileName();

      File.Exists(result).Should().BeTrue("because the temp file should be created");
    }

    [Fact]
    public void GetTempFileName_UseParameter_GiveTrue_ExpectFileIsExisting() {
      var target = GetInstance();

      var result = target.GetTempFileName(true);

      File.Exists(result).Should().BeTrue("because the temp file should be created");
    }

    [Fact]
    public void GetTempFileName_UseParameter_GiveFalse_ExpectFileIsNotExisting() {
      var target = GetInstance();

      var result = target.GetTempFileName(false);

      File.Exists(result).Should().BeFalse("because the temp file shouldn't be created");
    }

    [Fact]
    public void CreateTempFileAndCleanUp() {
      var target = GetInstance();

      var tempFile = target.GetTempFileName(true);

      File.Exists(tempFile).Should().BeTrue("because we need a file we can delete");

      target.DeleteTempFiles();

      File.Exists(tempFile).Should().BeFalse("because the class should cleanup files");
    }

    [Fact]
    public void CreateDefaultInstance_VerifyCurrentPathCanBeReadAndWrite() {
      var target = GetInstance();

      var result = target.CanWriteInDirectory();

      result.Should().BeTrue("because the default temp-path is used");
    }

    [Fact]
    public void CreateInstanceWithTempPath_VerifyCurrentPathCanBeReadAndWrite() {
      var target = GetInstance("/mnt/missing/notExisting/temp/");

      var result = target.CanWriteInDirectory();

      result.Should().BeFalse("because a strange not existing path was used where you have no read/write access");
    }

  }

}
