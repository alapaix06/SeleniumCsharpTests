using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace SeleniumCsharpTests.Data.Models
{
    public static class ShoppingModel
    {
        /// <summary>
        /// Gets or sets the username for the shopping test.
        /// </summary>
        private static string UserName { get; set; }
        
        /// <summary>
        /// Gets or sets the password for the shopping test.
        /// </summary>
        private static string Password { get; set; }
        
        /// <summary>
        /// Gets or sets the array of products to be purchased in the shopping test.
        /// </summary>
        private static string [] Products { get; set; }
        
        /// <summary>
        /// Gets or sets the country code for the shopping test.
        /// </summary>
        private static string CountryCode { get; set; }
        
        /// <summary>
        /// Gets or sets the expected success message for the shopping test.
        /// </summary>
        private static string ExpectedText { get; set; }
        
        /// <summary>
        /// This method extracts test data from a JSON file and returns it as a collection of TestCaseData objects.
        /// </summary>
        /// <param name="filePath">The path of the JSON file containing the test data.</param>
        /// <returns>A collection of TestCaseData objects.</returns>
        /// <exception cref="Exception">Thrown when there is an error reading the JSON file.</exception>
        public static IEnumerable<TestCaseData> GetDataFromJson(string filePath)
        {
            string json;

            try
            {
                // Read the contents of the JSON file.
                json = File.ReadAllText(filePath);
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
                UserName = testCase["username"]?.Value<string>() ?? String.Empty;
                Password = testCase["password"]?.Value<string>() ?? String.Empty;
                Products = testCase["products"]?.ToObject<string[]>() ?? Array.Empty<string>();
                CountryCode = testCase["countryCode"]?.Value<string>() ?? String.Empty;
                ExpectedText = testCase["textSuccess"]?.Value<string>() ?? String.Empty;

                // Create a new TestCaseData object and return it.
                yield return new TestCaseData(UserName, Password, Products, CountryCode, ExpectedText);
            }
        }
    }
}
