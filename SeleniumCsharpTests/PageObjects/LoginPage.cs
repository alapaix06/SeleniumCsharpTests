using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumCsharpTests.PageObjects;

/// <summary>
///     Class representing the Login page of the website
/// </summary>
public class LoginPage
{
    private readonly IWebDriver _driver;

    /// <summary>
    /// Constructor for LoginPage class
    /// </summary>
    /// <param name="driver">The WebDriver object to be used for automation</param>
    public LoginPage(IWebDriver driver)
    {
        _driver = driver;
        PageFactory.InitElements(driver, this);
    }

    //PageFactory annotation to locate the sign in button
    [FindsBy(How = How.Id, Using = "signInBtn")]
    [CacheLookup]
    private IWebElement ButtonSignIn;

    //PageFactory annotation to locate the password input
    [FindsBy(How = How.Id, Using = "password")]
    [CacheLookup]
    private IWebElement Password;

    //PageFactory annotation to locate the terms check
    [FindsBy(How = How.Id, Using = "terms")]
    [CacheLookup]
    private IWebElement Terms;

    // PageFactory annotation to locate the username input
    [FindsBy(How = How.Id, Using = "username")]
    [CacheLookup]
    private IWebElement Username;

    /// <summary>
    ///     Method to perform a valid login by entering the given username, password and accepting the terms
    /// </summary>
    /// <param name="user">Username to login</param>
    /// <param name="pass">Password to login</param>
    /// <returns>ProductsPage object representing the products page after login</returns>
    public ProductsPage ValidLogin(string user, string pass)
    {
        Username.SendKeys(user);
        Password.SendKeys(pass);
        Terms.Click();
        ButtonSignIn.Click();
        return new ProductsPage(_driver);
    }
}