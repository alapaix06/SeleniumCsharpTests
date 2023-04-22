using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;

namespace SeleniumCsharpTests.PageObjects;

/// <summary>
///     Define the page object class for the confirmation page
/// </summary>
public class ConfirmPurchasePage
{
    private readonly WebDriverWait _wait;

    // Private fields to store the driver and WebDriverWait objects
    private IWebDriver _driver;
    
    /// <summary>
    ///     Constructor for the page object, initializes the driver and WebDriverWait objects
    /// </summary>
    /// <param name="driver">The configurations for drivers in selenium</param>
    public ConfirmPurchasePage(IWebDriver driver)
    {
        _driver = driver;
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

        // Initializes the page object elements using the PageFactory class
        PageFactory.InitElements(driver, this);
    }
    
    // PageFactory annotation to locate the finish button element
    [FindsBy(How = How.ClassName, Using = "btn-success")]
    [CacheLookup]
    private IWebElement finishButton;

    // PageFactory annotation to locate the terms and conditions checkbox element
    [FindsBy(How = How.CssSelector, Using = "label[for*='checkbox2']")]
    [CacheLookup]
    private IWebElement termsCheckbox;

    // Private fields to locate the country input and select elements
    private readonly By countryInputLocator = By.Id("country");

    // PageFactory annotation to locate the success message element
    [FindsBy(How = How.ClassName, Using = "alert-success")]
    [CacheLookup]
    private IWebElement MessageFinish;

    private readonly By countrySelectLocator = By.LinkText("India");

    // Method to select the country from the dropdown list
    public void SetCountryCodeAndSelect(string countryCode)
    {
        try
        {
            // Waits until the country input element is visible, then enters the country code
            _wait.Until(ExpectedConditions.ElementIsVisible(countryInputLocator)).SendKeys(countryCode);

            // Waits until the select country element is visible, then clicks on it to select the country
            _wait.Until(ExpectedConditions.ElementIsVisible(countrySelectLocator)).Click();
        }
        catch (Exception e)
        {
            // Throws an exception if either of the waits fail
            throw new ArgumentException("Test fail waiting element: " + e.Message);
        }
    }

    /// <summary>
    ///     Getter method for the CheckTerms element
    /// </summary>
    /// <returns>the element secure terms check</returns>
    public IWebElement GetSelectorAcceptTerms()
    {
        return termsCheckbox;
    }

    /// <summary>
    ///     Getter method for the ButtonFinish element
    /// </summary>
    /// <returns>the element secure button finish</returns>
    public IWebElement GetSelectorFinishButton()
    {
        return finishButton;
    }

    /// <summary>
    ///     Getter method for the success message text
    /// </summary>
    /// <returns>return the element string within message</returns>
    public string GetFinishMessage()
    {
        return MessageFinish.Text;
    }
}