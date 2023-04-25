using OpenQA.Selenium;

namespace SeleniumCsharpTests.Pages.Interfaces;

public interface ICheckoutPage
{
    /// <summary>
    /// Returns a list of all the products displayed in the checkout page
    /// </summary>
    /// <returns>A list of IWebElements representing the products in the checkout page</returns>
    IList<IWebElement> GetProductsList();

    /// <summary>
    /// Clicks on the 'Success' button to confirm the purchase and proceeds to the confirmation page
    /// </summary>
    /// <returns>An instance of the ConfirmPurchasePage</returns>
    ConfirmPurchasePage ClickSuccessButton();
}