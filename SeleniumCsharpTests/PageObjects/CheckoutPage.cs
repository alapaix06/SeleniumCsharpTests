using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumCsharpTests.PageObjects
{
    /// <summary>
    /// Page object for the checkout page
    /// </summary>
    public class CheckoutPage
    {
        private readonly IWebDriver _driver;

        /// <summary>
        /// Constructor that initializes the driver and page elements
        /// </summary>
        /// <param name="driver">The WebDriver instance to use</param>
        public CheckoutPage(IWebDriver driver)
        {
            _driver = driver;
            // Initializes the elements in the page using PageFactory from SeleniumExtras
            PageFactory.InitElements(driver, this);
        }

        // Finds the "Success" button using a CSS selector
        [FindsBy(How = How.CssSelector, Using = "td .btn-success")]
        [CacheLookup]
        private IWebElement _successButton;

        // Finds the list of products in the checkout using a CSS selector
        [FindsBy(How = How.CssSelector, Using = "h4 a")]
        [CacheLookup]
        private IList<IWebElement> _productsInCheckoutList;

        /// <summary>
        /// Gets the list of products in the checkout
        /// </summary>
        /// <returns>A list of IWebElement objects representing the products</returns>
        public IList<IWebElement> GetProductsList()
        {
            return _productsInCheckoutList;
        }

        /// <summary>
        /// Clicks on the "Success" button and returns a new page object for the confirmation page
        /// </summary>
        /// <returns>A ConfirmPurchasePage object representing the confirmation page</returns>
        public ConfirmPurchasePage ClickSuccessButton()
        {
            _successButton.Click();
            return new ConfirmPurchasePage(_driver);
        }
    }
}