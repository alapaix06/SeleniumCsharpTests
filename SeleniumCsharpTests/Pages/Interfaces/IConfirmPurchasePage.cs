using OpenQA.Selenium;

namespace SeleniumCsharpTests.Pages.Interfaces
{

    public interface IConfirmPurchasePage
    {
        /// <summary>
        /// Sets the country code and selects it from the dropdown.
        /// </summary>
        /// <param name="countryCode">The code of the country to be selected.</param>
        void SetCountryCodeAndSelect(string countryCode);

        /// <summary>
        /// Returns the success message displayed after the purchase is confirmed.
        /// </summary>
        /// <returns>The success message.</returns>
        string GetFinishMessage();

        /// <summary>
        /// Returns the selector for the checkbox that confirms acceptance of the terms and conditions.
        /// </summary>
        /// <returns>The selector for the checkbox.</returns>
        IWebElement GetSelectorAcceptTerms();

        /// <summary>
        /// Returns the selector for the button to confirm the purchase.
        /// </summary>
        /// <returns>The selector for the button.</returns>
        IWebElement GetSelectorFinishButton();
    }
}