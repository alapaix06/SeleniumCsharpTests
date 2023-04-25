using AventStack.ExtentReports;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using Optional;
using SeleniumCsharpTests.Utility.ReportManager;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumCsharpTests.Tests
{

    /// <summary>
    /// Base class for setting up and tearing down browser tests
    /// </summary>
    [TestFixture]
    public class BaseTests
    {
        //Thread-local driver instance
        private readonly ThreadLocal<IWebDriver> _driver = new ThreadLocal<IWebDriver>();

        //Option for browser name
        private Option<string> _browserName;

        //Timeout in seconds
        private readonly int _seconds = 5;

        //Default browser name
        private readonly string _defaultBrowser = "Chrome";

        /// <summary>
        /// One-time setup method for Extent Reports
        /// </summary>
        [OneTimeSetUp]
        public void SetUpTearDown()
        {
            ExtentTestManager.CreateParentTest(GetType().Name, Option.None<string>());
        }

        /// <summary>
        /// Sets up the browser configuration and navigates to the URL
        /// </summary>
        /// <param name="url">The URL to navigate to</param>
        protected void BrowserConfig(string url)
        {
            try
            {
                // Set up the driver and navigate to the URL
                _browserName = Option.Some(TestContext.Parameters["browserName"] ?? _defaultBrowser);
                _browserName.Match(
                    some: name => MultiplesBrowsers(name),
                    none: () => TestContext.Progress.WriteLine("Error: Invalid browser name")
                );
                _driver.Value.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_seconds);
                _driver.Value.Manage().Window.Maximize();
                _driver.Value.Url = url;
            }
            catch (Exception e)
            {
                Assert.Fail($"Test fail in SetUp:  {e.Message}");
            }
        }

        /// <summary>
        ///  Method to get the current driver instance
        /// </summary>
        /// <returns>The current driver instance</returns>
        protected IWebDriver GetDriver()
        {
            return _driver.Value;
        }

        /// <summary>
        ///  Method to set up different web drivers based on the input browser name
        /// </summary>
        /// <param name="browser">The name of the browser to set up</param>
        /// <exception cref="ArgumentException">Thrown when an invalid browser name is provided</exception>
        private void MultiplesBrowsers(string browser)
        {
            switch (browser)
            {
                case "Chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    _driver.Value = new ChromeDriver();
                    break;
                case "Firefox":
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    _driver.Value = new FirefoxDriver();
                    break;
                case "Edge":
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    _driver.Value = new EdgeDriver();
                    break;
                default:
                    throw new ArgumentException("Invalid browser name provided.");
            }
        }

        /// <summary>
        /// Writes a message to the ExtentReport log, indicating whether a test has passed or failed, and captures a screenshot if the test failed.
        /// </summary>
        protected void EndExtent()
        {
            // Get the test status and message
            TestStatus status = TestContext.CurrentContext.Result.Outcome.Status;
            string? message = TestContext.CurrentContext.Result.Message;

            // Format the message
            message = message.SomeWhen(msg => !string.IsNullOrEmpty(msg))
                .Match(
                    some: msg => string.Format("<pre>{0}</pre>", TestContext.CurrentContext.Result.Message),
                    none: () => ""
                );

            //switch in case fail, pass or skip
            switch (status)
            {
                case TestStatus.Failed:
                    ReportLog.Fail("Fail Test", Option.None<MediaEntityModelProvider>());
                    ReportLog.Fail(message, Option.None<MediaEntityModelProvider>());
                    ReportLog.Fail("ScreenShot",
                        CaptureFails(_driver.Value, TestContext.CurrentContext.Test.Name).SomeNotNull());
                    break;
                case TestStatus.Skipped:
                    ReportLog.Skip("Test Skipped");
                    break;
                case TestStatus.Passed:
                    ReportLog.Pass("Test Pass");
                    break;
            }
        }

        /// <summary>
        /// Captures a screenshot of the current browser window and returns it as a MediaEntityModelProvider object.
        /// </summary>
        /// <param name="name">The name to give the screenshot.</param>
        /// <param name="driver"></param>
        /// <returns>A MediaEntityModelProvider object containing the screenshot.</returns>
        private MediaEntityModelProvider CaptureFails(IWebDriver driver, string name)
        {
            string screenshot = ((ITakesScreenshot)driver).GetScreenshot().AsBase64EncodedString;

            return MediaEntityBuilder.CreateScreenCaptureFromBase64String(screenshot, name).Build();
        }

        /// <summary>
        /// Closes the current WebDriver instance.
        /// </summary>
        protected void QuitBrowsers()
        {
            try
            {
                _driver.Value.Quit();
            }
            catch (Exception e)
            {
                Assert.Fail("Test fail in TearDown: " + e.Message);
            }
        }

        /// <summary>
        /// Flushes the ExtentReport log, indicating that all tests have been completed.
        /// </summary>
        [OneTimeTearDown]
        public void TearDownExtent()
        {
            ExtentReportService.GetExtentReports().Flush();
        }
    }
}