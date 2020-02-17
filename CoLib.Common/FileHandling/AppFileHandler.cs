using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using CoLib.Common.Interfaces;

namespace CoLib.Common.FileHandling {

  /// <inheritdoc />
  public class AppFileHandler : IAppFileHandler {

    private readonly string _tempDirectory;
    private IList<string> _files;

    /// <summary>
    ///   Creates a new instance using default temp-path.
    /// </summary>
    public AppFileHandler() : this(Path.GetTempPath()) {
    }

    /// <summary>
    ///   Creates a new instance using the given temp-path.
    /// </summary>
    /// <param name="tempDirectory">The root directory which should be used for tempfiles.</param>
    public AppFileHandler(string tempDirectory) {
      _tempDirectory = tempDirectory;
      _files = new List<string>();
    }

    /// <inheritdoc />
    public bool CanWriteInDirectory() {
      string tempFile = GetTempFileName(false);

      try {
        File.Create(tempFile).Dispose();
        File.Delete(tempFile);
      } catch (Exception ex) {
        // Application was unable to create and delete a temp-file. 
        return false;
      }

      return true;
    }

    /// <inheritdoc />
    public string GetTempFileName() {
      return GetTempFileName(true);
    }

    /// <inheritdoc />
    public string GetTempFileName(bool createEmptyFile) {
      string fileName = Path.GetRandomFileName();
      string filePath = Path.Combine(_tempDirectory, fileName);

      _files.Add(filePath);
      
      if (createEmptyFile) {
        File.WriteAllBytes(filePath, new byte[0]);
      }

      return filePath;
    }

    /// <inheritdoc />
    public void DeleteTempFiles() {
      IList<string> notDeletedFiles = new List<string>();

      foreach (string filePath in _files) {
        if (TryDeleteFile(filePath) == false) {
          notDeletedFiles.Add(filePath);
        }
      }

      _files = notDeletedFiles;
    }


    private static bool TryDeleteFile(string filePath) {
      try {
        File.Delete(filePath);
      } catch (Exception ex) {
        // Unable to delete temp-file
        return false;
      }

      return true;
    }

  }

}
