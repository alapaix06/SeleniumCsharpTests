using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumCsharpTests.Utility;

public class BaseTests
{
    private readonly int _seconds = 5;
    private IWebDriver _webDriver;

    /// <summary>
    ///     Method to set up the test environment before each test case
    /// </summary>
    [SetUp]
    public void SetUp()
    {
        try
        {
            // Set up the driver and navigate to the URL
            MultiplesBrowsers("Chrome");
            _webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(_seconds);
            _webDriver.Manage().Window.Maximize();
            _webDriver.Url = "https://rahulshettyacademy.com/loginpagePractise/";
        }
        catch (Exception e)
        {
            // Print error message and fail the test if there is an exception
            TestContext.Progress.WriteLine("Test Fail in SetUp: " + e.Message);
            Assert.Fail("Test fail in SetUp: " + e.Message);
        }
    }

    /// <summary>
    ///     Method to get the current driver instance
    /// </summary>
    /// <returns>The current driver instance</returns>
    protected IWebDriver GetDriver()
    {
        return _webDriver;
    }

    /// <summary>
    ///     Method to set up different web drivers based on the input browser name
    /// </summary>
    /// <param name="browser">The name of the browser to set up</param>
    /// <exception cref="ArgumentException">Thrown when an invalid browser name is provided</exception>
    private void MultiplesBrowsers(string browser)
    {
        switch (browser)
        {
            case "Chrome":
                new DriverManager().SetUpDriver(new ChromeConfig());
                _webDriver = new ChromeDriver();
                break;
            case "Firefox":
                new DriverManager().SetUpDriver(new FirefoxConfig());
                _webDriver = new FirefoxDriver();
                break;
            case "Edge":
                new DriverManager().SetUpDriver(new EdgeConfig());
                _webDriver = new EdgeDriver();
                break;
            default:
                throw new ArgumentException("Invalid browser name provided.");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    protected static JsonReader GetDataParser()
    {
        return new JsonReader();
    }

    /// <summary>
    ///  Method to tear down the test environment after each test case
    /// </summary>
    [TearDown]
    public void Down()
    {
        try
        {
            // Quit the driver
            _webDriver.Quit();
        }
        catch (Exception e)
        {
            // Print error message and fail the test if there is an exception
            TestContext.Progress.WriteLine("Test Fail in TearDown: " + e.Message);
            Assert.Fail("Test fail in TearDown: " + e.Message);
        }
    }
}