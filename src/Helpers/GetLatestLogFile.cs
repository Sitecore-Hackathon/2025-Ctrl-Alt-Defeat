using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace ReadLogsAndSolve.Helpers
{
    public class GetLatestLogFile
    {
        public static string GetLatestLogFilePath()
        {
            // Read the log directory from App.config
            string logDirectory = ConfigurationManager.AppSettings["LogDirectory"];

            // Get the absolute path to the log directory
            string absoluteLogDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", logDirectory);

            if (Directory.Exists(absoluteLogDirectory))
            {
                // Get all log files in the directory
                string[] logFiles = Directory.GetFiles(absoluteLogDirectory, "log.*.txt");

                if (logFiles.Length > 0)
                {
                    // Define a regex pattern to extract the date and time from the filename
                    Regex datePattern = new Regex(@"log\.(\d{8})\.(\d{6})");

                    // Order the files by the extracted date and time
                    string latestLogFile = logFiles
                        .Select(file => new
                        {
                            FilePath = file,
                            FileDate = datePattern.Match(Path.GetFileName(file)).Success
                                ? DateTime.ParseExact(
                                    datePattern.Match(Path.GetFileName(file)).Groups[1].Value +
                                    datePattern.Match(Path.GetFileName(file)).Groups[2].Value,
                                    "yyyyMMddHHmmss",
                                    null)
                                : DateTime.MinValue
                        })
                        .OrderByDescending(file => file.FileDate)
                        .First().FilePath;

                    return latestLogFile;
                }
                else
                {
                    throw new FileNotFoundException("No log files found in the specified directory.");
                }
            }
            else
            {
                throw new DirectoryNotFoundException("The specified log directory does not exist.");
            }
        }

        public static string[] GetLastNLinesFromFile(string filePath, int numberOfLines)
        {
            var lines = new List<string>();
            Regex logPattern = new Regex(@"(WARN|INFO|ERROR)\s*:\s*(.*)", RegexOptions.IgnoreCase);

            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(fs))
            {
                // Move the stream position to the end of the file
                long position = fs.Length;

                // Buffer to hold the characters read from the file
                char[] buffer = new char[1024];
                int charsRead;

                // Read backwards until we have the desired number of lines or reach the beginning of the file
                while (position > 0 && lines.Count < numberOfLines)
                {
                    // Move the position back by the buffer size or to the start of the file
                    long readLength = Math.Min(buffer.Length, position);
                    position -= readLength;
                    fs.Position = position;

                    // Read the buffer and convert it to a string
                    charsRead = reader.Read(buffer, 0, (int)readLength);
                    string text = new string(buffer, 0, charsRead);

                    // Split the text into lines and add them to the list
                    string[] textLines = text.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    // Filter lines to include only those matching the log pattern
                    var relevantLines = textLines
                        .Select(line => logPattern.Match(line))
                        .Where(match => match.Success)
                        .Select(match => $"{match.Groups[1].Value}: {match.Groups[2].Value}");

                    lines.InsertRange(0, relevantLines);

                    // If we have more lines than needed, remove the excess from the beginning
                    if (lines.Count > numberOfLines)
                    {
                        lines.RemoveRange(0, lines.Count - numberOfLines);
                    }
                }
            }

            return lines.ToArray();
        }
    }
}