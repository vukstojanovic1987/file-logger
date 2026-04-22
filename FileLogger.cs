using System;
using System.IO;
using System.Text;

namespace FileLoggerLib
{
    /// <summary>
    /// Lightweight file logger for writing timestamped messages to a text file.
    /// Supports basic log levels and optional daily log file naming.
    /// </summary>
    public class FileLogger
    {
        private readonly string _filePath;
        private readonly bool _useDailyFiles;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileLogger"/> class.
        /// </summary>
        /// <param name="filePath">
        /// Full path to the log file, or directory path if daily files are enabled.
        /// </param>
        /// <param name="useDailyFiles">
        /// If set to <c>true</c>, a separate log file is created for each day.
        /// </param>
        public FileLogger(string filePath, bool useDailyFiles = false)
        {
            _filePath = filePath;
            _useDailyFiles = useDailyFiles;

            EnsureDirectoryExists();
        }

        /// <summary>
        /// Writes an informational message to the log file.
        /// </summary>
        /// <param name="message">Message to write.</param>
        public void Log(string message)
        {
            Write("INFO", message);
        }

        /// <summary>
        /// Writes a warning message to the log file.
        /// </summary>
        /// <param name="message">Warning message to write.</param>
        public void LogWarning(string message)
        {
            Write("WARNING", message);
        }

        /// <summary>
        /// Writes an error message to the log file.
        /// </summary>
        /// <param name="message">Error message to write.</param>
        public void LogError(string message)
        {
            Write("ERROR", message);
        }

        /// <summary>
        /// Writes exception details to the log file.
        /// </summary>
        /// <param name="exception">Exception to write.</param>
        public void LogError(Exception exception)
        {
            if (exception == null)
            {
                Write("ERROR", "Unknown exception.");
                return;
            }

            Write("ERROR", $"{exception.Message}{Environment.NewLine}{exception}");
        }

        private void Write(string level, string message)
        {
            string logLine = FormatMessage(level, message);
            string targetFile = GetTargetFilePath();

            File.AppendAllText(targetFile, logLine + Environment.NewLine, Encoding.UTF8);
        }

        private string FormatMessage(string level, string message)
        {
            return $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";
        }

        private string GetTargetFilePath()
        {
            if (!_useDailyFiles)
            {
                return _filePath;
            }

            if (Path.HasExtension(_filePath))
            {
                string? directory = Path.GetDirectoryName(_filePath);
                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(_filePath);
                string extension = Path.GetExtension(_filePath);

                string dailyFileName = $"{fileNameWithoutExtension}_{DateTime.Now:yyyy-MM-dd}{extension}";
                return Path.Combine(directory ?? string.Empty, dailyFileName);
            }

            return Path.Combine(_filePath, $"log_{DateTime.Now:yyyy-MM-dd}.txt");
        }

        private void EnsureDirectoryExists()
        {
            string targetPath = _useDailyFiles && !Path.HasExtension(_filePath)
                ? _filePath
                : Path.GetDirectoryName(_filePath) ?? string.Empty;

            if (!string.IsNullOrWhiteSpace(targetPath) && !Directory.Exists(targetPath))
            {
                Directory.CreateDirectory(targetPath);
            }
        }
    }
}