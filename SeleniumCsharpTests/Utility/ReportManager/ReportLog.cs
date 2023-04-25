using AventStack.ExtentReports;
using Optional;

namespace SeleniumCsharpTests.Utility.ReportManager;

public static class ReportLog
{
    /// <summary>
    /// Logs a "Pass" message to the extent report.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    public static void Pass(string message)
    {
        ExtentTestManager.GetTest().Pass(message);
    }

    /// <summary>
    /// Logs a "Fail" message to the extent report.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    /// <param name="media">Optional media (such as screenshots) to be attached to the report.</param>
    public static void Fail(string message, Option<MediaEntityModelProvider> media)
    {
        media.Match(
            some: (value) =>
            {
                ExtentTestManager.GetTest().Fail(message, value);
            },
            none: () =>
            {
                ExtentTestManager.GetTest().Fail(message);
            }
        );
    }
    
    /// <summary>
    /// Logs a "Skip" message to the extent report.
    /// </summary>
    /// <param name="message">The message to be logged.</param>
    public static void Skip(string message)
    {
        ExtentTestManager.GetTest().Skip(message);
    }
}