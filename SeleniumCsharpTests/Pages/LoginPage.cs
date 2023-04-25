using OpenQA.Selenium;
using SeleniumCsharpTests.Pages.Interfaces;
using SeleniumExtras.PageObjects;

namespace SeleniumCsharpTests.Pages
{

    /// <summary>
    ///  Class representing the Login page of the website
    /// </summary>
    public class LoginPage : ILoginPage
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
        [FindsBy(How = How.Id, Using = "signInBtn")] [CacheLookup]
        private IWebElement _buttonSignIn;

        //PageFactory annotation to locate the password input
        [FindsBy(How = How.Id, Using = "password")] [CacheLookup]
        private IWebElement _passwordInput;

        //PageFactory annotation to locate the terms check
        [FindsBy(How = How.Id, Using = "terms")] [CacheLookup]
        private IWebElement _termsCheckbox;

        // PageFactory annotation to locate the username input
        [FindsBy(How = How.Id, Using = "username")] [CacheLookup]
        private IWebElement _usernameInput;

        /// <summary>
        ///     Method to perform a valid login by entering the given username, password and accepting the terms
        /// </summary>
        /// <param name="user">Username to login</param>
        /// <param name="pass">Password to login</param>
        /// <returns>ProductsPage object representing the products page after login</returns>
        public ProductsPage PerformValidLogin(string user, string pass)
        {
            _usernameInput.SendKeys(user);
            _passwordInput.SendKeys(pass);
            _termsCheckbox.Click();
            _buttonSignIn.Click();
            return new ProductsPage(_driver);
        }
    }
}