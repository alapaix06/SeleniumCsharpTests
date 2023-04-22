using Newtonsoft.Json.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using Optional;
using SeleniumCsharpTests.PageObjects;
using SeleniumCsharpTests.Utility;
using SeleniumCsharpTests.Utility.ExtentReportConfig;

namespace SeleniumCsharpTests.Tests
{
    public sealed class ShopFacts : BaseTests
    {
        private Option<LoginPage> _loginPage = Option.None<LoginPage>();
        
        /// <summary>
        ///  Method to set up the test environment before each test case
        /// </summary>
        [SetUp]
        public void SetUp()
        {
            BrowserConfig("https://rahulshettyacademy.com/loginpagePractise/");
            ExtentTestManager.CreateTest(TestContext.CurrentContext.Test.Name, Option.None<string>());
        }
        
        /// <summary>
        /// Test method to simulate an end-to-end shopping experience
        /// </summary>
        [Test]
        [TestCaseSource(nameof(GetDataFromJson))]
        public void E2EFactory(string username, string password, string[] products, string countryCode,
            string expectedText)
        {
            try
            {
                // Initializes the login page object and logs in with valid credentials
                _loginPage = Option.Some(new LoginPage(GetDriver()));

                _loginPage.Match(
                    some: loginPage =>
                    {
                        ProductsPage productsPage = loginPage.PerformValidLogin(username, password);

                        // Waits for the products page to load and stores the desired product names in an array
                        productsPage.WaitForPageToLoad();

                        // Iterates over the product cards and adds the desired products to the cart
                        IList<IWebElement> cardProducts = productsPage.GetAllCards();
                        cardProducts.Select(product => new
                        { Element = product, Title = product.FindElement(productsPage.GetCardTitleSelector()).Text })
                            .Where(x => products.Contains(x.Title))
                            .ToList()
                            .ForEach(x => x.Element.FindElement(productsPage.GetAddToCheckoutButtonSelector()).Click());

                        // Navigates to the checkout page and verifies that the correct products have been added to the cart
                        CheckoutPage checkOutPage = productsPage.ClickOnCheckoutButton();
                        string[] productsListCheck = checkOutPage.GetProductsList().Select(p => p.Text).ToArray();
                        Assert.That(products, Is.EqualTo(productsListCheck));

                        // Navigates to the confirmation page, selects a country, agrees to the terms and conditions, and completes the purchase
                        ConfirmPurchasePage confirmPurchasePage = checkOutPage.ClickSuccessButton();
                        confirmPurchasePage.SetCountryCodeAndSelect(countryCode);
                        confirmPurchasePage.GetSelectorAcceptTerms().Click();
                        confirmPurchasePage.GetSelectorFinishButton().Click();
                        StringAssert.Contains(expectedText, confirmPurchasePage.GetFinishMessage());

                    },
                    none: () => throw new Exception("LoginPage is not initialized"));

            }
            catch (NoSuchElementException e)
            {
                throw new NoSuchElementException("An element was not found. Please check the locator.", e);
            }
            catch (Exception e)
            {
                string errorMessage = $"Test Fail in execution: {e.Message}";
                TestContext.Progress.WriteLine(errorMessage);
                Assert.Fail(errorMessage);
                throw new Exception(errorMessage);
            }
        }

        /// <summary>
        /// This method reads test data from a JSON file and returns it as a collection of TestCaseData objects.
        /// </summary>
        /// <returns>A collection of TestCaseData objects containing test data from the JSON file.</returns>
        public static IEnumerable<TestCaseData> GetDataFromJson()
        {
            // Set the path to the JSON file containing the test data.
            string path = "Utility/NewData.json";
            string json;

            try
            {
                // Read the contents of the JSON file.
                json = File.ReadAllText(path);
            }
            catch (Exception e)
            {
                throw new Exception($"Error reading the json file: {e.Message}");
            }

            // Parse the JSON string into a JToken object.
            JToken jsonObject = JToken.Parse(json);

            // Loop through the test cases in the JSON object.
            foreach (JToken testCase in jsonObject["testCases"] ?? Enumerable.Empty<JToken>())
            { 
                // Extract the test data from the JSON object.
                string username = testCase["username"]?.Value<string>() ?? String.Empty;
                string password = testCase["password"]?.Value<string>() ?? String.Empty;
                string[] products = testCase["products"]?.ToObject<string[]>() ?? Array.Empty<string>();
                string countryCode = testCase["countryCode"]?.Value<string>() ?? String.Empty;
                string expectedText = testCase["textSuccess"]?.Value<string>() ?? String.Empty;

                // Create a new TestCaseData object and return it.
                yield return new TestCaseData(username, password, products, countryCode, expectedText);
            }
        }
        
        /// <summary>
        ///  Method to tear down the test environment after each test case
        /// </summary>
        [TearDown]
        public void Down()
        {
            EndExtent();
            QuitBrowsers();
        }
    }
}