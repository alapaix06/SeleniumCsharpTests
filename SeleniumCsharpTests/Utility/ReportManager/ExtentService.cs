using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.Reporter.Configuration;
using Optional;
using AventStack.ExtentReports;

namespace SeleniumCsharpTests.Utility.ReportManager
{
    /// <summary>
    /// A singleton class that provides an instance of the ExtentReports object for generating HTML reports.
    /// </summary>
    public sealed class ExtentReportService
    {
        private static ExtentReports _extentReports;

        /// <summary>
        /// Gets an instance of the ExtentReports object, and attaches the HTML reporter with the necessary configurations.
        /// </summary>
        /// <returns>The instance of the ExtentReports object.</returns>
        public static ExtentReports GetExtentReports()
        {
            Option<ExtentReports> extentReportsOption = _extentReports.SomeNotNull();
        
            extentReportsOption.MatchNone(() =>
            {
                _extentReports = new ExtentReports();
                
                // Get the project directory and create the directory for the reports
                string reportDirectory = Path.Combine(GetProjectDirectory(), "Reports");
                string reportPath = Path.Combine(reportDirectory, "index.html");
                ExtentHtmlReporter reporter = new(reportPath);
                
                // Configure the HTML reporter with the desired title, report name, and theme
                reporter.Config.DocumentTitle = "Framework Automation Report";
                reporter.Config.ReportName = "Shop Testing";
                reporter.Config.Theme = Theme.Standard;
                _extentReports.AttachReporter(reporter);
            });
            
            // Return the instance of the ExtentReports object
            return _extentReports;
        }

        /// <summary>
        /// Gets the project directory for storing the reports.
        /// </summary>
        /// <returns>The project directory path.</returns>
        private static string GetProjectDirectory()
        {
            // Get the current directory and remove the "bin" folder from the path
            string currentDirectory = Directory.GetCurrentDirectory();
            return currentDirectory.Split("bin")[0];
        }
    }
}
