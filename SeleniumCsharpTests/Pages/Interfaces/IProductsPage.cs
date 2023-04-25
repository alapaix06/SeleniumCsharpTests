using OpenQA.Selenium;

namespace SeleniumCsharpTests.Pages.Interfaces
{

    /// <summary>
    /// Interface representing the Products page of a website
    /// </summary>
    public interface IProductsPage
    {
        /// <summary>
        /// Waits for the page to finish loading
        /// </summary>
        void WaitForPageToLoad();

        /// <summary>
        /// Gets all the product cards on the page
        /// </summary>
        /// <returns>List of web elements representing product cards</returns>
        IList<IWebElement> GetAllProductCards();

        /// <summary>
        /// Gets the selector for the title of a product card
        /// </summary>
        /// <returns>Selector for the title of a product card</returns>
        By GetProductCardTitleSelector();

        /// <summary>
        /// Gets the selector for the "Add to Cart" button on a product card
        /// </summary>
        /// <returns>Selector for the "Add to Cart" button</returns>
        By GetAddToCartButtonSelector();

        /// <summary>
        /// Clicks on the "Add to Cart" button and navigates to the checkout page
        /// </summary>
        /// <returns>Instance of the checkout page</returns>
        CheckoutPage ClickOnCheckoutButton();
    }
}