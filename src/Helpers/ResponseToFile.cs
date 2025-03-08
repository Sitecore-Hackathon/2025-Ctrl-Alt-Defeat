using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ReadLogsAndSolve.Helpers
{
    public class ResponseToFile
    {
        public static async Task AnalyzeLogErrorsAndSaveResponseAsync()
        {
            try
            {
                // Get the latest log file path
                string latestLogFilePath = GetLatestLogFile.GetLatestLogFilePath();
                Console.WriteLine("Latest Log File Path: " + latestLogFilePath);

                string[] logLines = GetLatestLogFile.GetLastNLinesFromFile(latestLogFilePath, 500);

                if (logLines.Length > 0)
                {
                    // Send error messages to the AI model API
                    string aiResponse = await SendToAIModelAPI.SendToAIModelAPIAsync(logLines);

                    // Save the AI response to a text file
                    string responseFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "App_Data", "logs", $"AI_Error_Analysis_Response_{DateTime.Now:yyyyMMddHHmmss}.md");
                    File.WriteAllText(responseFilePath, $"@{aiResponse}");

                    Console.WriteLine("AI Response saved to: " + responseFilePath);
                }
                else
                {
                    Console.WriteLine("No error messages found in the log file.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

    }
}