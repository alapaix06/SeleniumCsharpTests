using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace SeleniumCsharpTests.PageObjects;

/// <summary>
///     This class represents the products page in an online store.
/// </summary>
public class ProductsPage
{
    private readonly By _addCheckOut = By.CssSelector(".btn-info");

    // Selectors for the page elements.
    private readonly By _cardTitle = By.CssSelector(".card-title a");
    private readonly By _checkOutButton = By.ClassName("btn-primary");
    private readonly WebDriverWait _wait;
    private readonly IWebDriver _driver;

    // Attributes with PageFactory annotations to map the page elements find  card products.
    [FindsBy(How = How.ClassName, Using = "card")]
    private IList<IWebElement> CardProducts;

    //Attributes with PageFactory annotations to map the page elements find button checkout.
    [FindsBy(How = How.ClassName, Using = "btn-primary")]
    private IWebElement GoCheckOut;

    /// <summary>
    ///     Constructor for the ProductsPage class.
    /// </summary>
    /// <param name="driver">The WebDriver instance to use for interacting with the page.</param>
    public ProductsPage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(6));
        PageFactory.InitElements(driver, this);
    }

    /// <summary>
    ///     Waits for the page to be fully loaded and visible in the browser.
    /// </summary>
    public void WaitForPageIsVisible()
    {
        try
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(_checkOutButton));
        }
        catch (Exception e)
        {
            throw new ArgumentException("Element not located: " + e.Message);
        }
    }

    /// <summary>
    ///     Gets all product card elements on the page.
    /// </summary>
    /// <returns>A list of IWebElement elements representing the product cards on the page.</returns>
    public IList<IWebElement> GetCards()
    {
        return CardProducts;
    }

    /// <summary>
    ///     Gets the selector for the product card title.
    /// </summary>
    /// <returns>The By selector for the product card title.</returns>
    public By GetCardTitle()
    {
        return _cardTitle;
    }

    /// <summary>
    ///     Gets the selector for the "Add to cart" button on the product card.
    /// </summary>
    /// <returns>The By selector for the "Add to cart" button.</returns>
    public By ButtonAddCheckOut()
    {
        return _addCheckOut;
    }

    /// <summary>
    ///     Clicks on the "Checkout" button on the page.
    /// </summary>
    /// <returns>The IWebElement representing the "Checkout" button.</returns>
    public CheckOutPage ClickOnCheckOut()
    {
        GoCheckOut.Click();
        return new CheckOutPage(_driver);
    }
}