using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace SeleniumCsharpTests.PageObjects
{
    /// <summary>
    /// This class represents the products page in an online store.
    /// </summary>
    public class ProductsPage
    {
        private readonly By _addToCheckoutSelector = By.CssSelector(".btn-info");
        private readonly By _cardTitleSelector = By.CssSelector(".card-title a");
        private readonly By _checkoutButtonSelector = By.ClassName("btn-primary");
        private readonly WebDriverWait _wait;
        private readonly IWebDriver _driver;

        [FindsBy(How = How.ClassName, Using = "card")]
        private IList<IWebElement> _cardProducts;

        [FindsBy(How = How.ClassName, Using = "btn-primary")]
        private IWebElement _checkoutButton;

        /// <summary>
        /// Constructor for the ProductsPage class.
        /// </summary>
        /// <param name="driver">The WebDriver instance to use for interacting with the page.</param>
        public ProductsPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(6));
            PageFactory.InitElements(driver, this);
        }

        /// <summary>
        /// Waits for the page to be fully loaded and visible in the browser.
        /// </summary>
        public void WaitForPageToLoad()
        {
            try
            {
                _wait.Until(ExpectedConditions.ElementIsVisible(_checkoutButtonSelector));
            }
            catch (Exception e)
            {
                throw new ArgumentException("Element not located: " + e.Message);
            }
        }

        /// <summary>
        /// Gets all product card elements on the page.
        /// </summary>
        /// <returns>A list of IWebElement elements representing the product cards on the page.</returns>
        public IList<IWebElement> GetAllCards()
        {
            return _cardProducts;
        }

        /// <summary>
        /// Gets the selector for the product card title.
        /// </summary>
        /// <returns>The By selector for the product card title.</returns>
        public By GetCardTitleSelector()
        {
            return _cardTitleSelector;
        }

        /// <summary>
        /// Gets the selector for the "Add to cart" button on the product card.
        /// </summary>
        /// <returns>The By selector for the "Add to cart" button.</returns>
        public By GetAddToCheckoutButtonSelector()
        {
            return _addToCheckoutSelector;
        }

        /// <summary>
        /// Clicks on the "Checkout" button on the page.
        /// </summary>
        /// <returns>The CheckoutPage object representing the checkout page.</returns>
        public CheckoutPage ClickOnCheckoutButton()
        {
            _checkoutButton.Click();
            return new CheckoutPage(_driver);
        }
    }
}