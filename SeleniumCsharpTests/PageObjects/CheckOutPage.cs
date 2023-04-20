using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace SeleniumCsharpTests.PageObjects;

/// <summary>
/// Page object for the checkout page
/// </summary>
public class CheckOutPage
{
    private readonly IWebDriver _driver;

    /// <summary>
    ///     Constructor that initializes the driver and page elements
    /// </summary>
    /// <param name="driver">The WebDriver instance to use</param>
    public CheckOutPage(IWebDriver driver)
    {
        _driver = driver;
        // Initializes the elements in the page using PageFactory from SeleniumExtras
       PageFactory.InitElements(driver, this);
    }

    // Finds the "Success" button using a CSS selector
    [FindsBy(How = How.CssSelector, Using = "td .btn-success")]
    [CacheLookup]
    private IWebElement ButtonSuccess;

    // Finds the list of products in the checkout using a CSS selector
    [FindsBy(How = How.CssSelector, Using = "h4 a")]
    [CacheLookup]
    private IList<IWebElement> ListProductsInCheckOut;



    /// <summary>
    ///     Gets the list of products in the checkout
    /// </summary>
    /// <returns>A list of IWebElement objects representing the products</returns>
    public IList<IWebElement> GetListProducts()
    {
        return ListProductsInCheckOut;
    }

    /// <summary>
    ///     Clicks on the "Success" button and returns a new page object for the confirmation page
    /// </summary>
    /// <returns>A ConfirmPurchasedPage object representing the confirmation page</returns>
    public ConfirmPurchasedPage ClickOnButtonSuccess()
    {
        ButtonSuccess.Click();
        return new ConfirmPurchasedPage(_driver);
    }
}