namespace CoLib.Common.Interfaces {

  /// <summary>
  ///   THis interface defines methods for file-handling.
  /// </summary>
  public interface IAppFileHandler {

    /// <summary>
    ///   This method verifies if the class
    ///   has rights to create and delete temp files in the
    ///   given directory.
    /// </summary>
    /// <returns>
    ///    Returns true if the application was able to create and delete a temp file.
    /// </returns>
    bool CanWriteInDirectory();

    /// <summary>
    ///   Creates an empty temp-file and returns the path to this file.
    ///   This file is tracked as 'created'.
    /// </summary>
    /// <returns>
    ///   Returns the filepath to an empty-temp file
    /// </returns>
    string GetTempFileName();

    /// <inheritdoc cref="GetTempFileName()" />
    /// <param name="createEmptyFile">Determines if an empty file should be created.</param>
    /// <returns>
    ///   Returns the filepath to a file. If the file is existing
    ///   depends on the parameter.
    /// </returns>
    string GetTempFileName(bool createEmptyFile);

    /// <summary>
    ///   This method
    /// </summary>
    void DeleteTempFiles();

  }

}
