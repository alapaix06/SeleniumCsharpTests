using Newtonsoft.Json.Linq;

namespace SeleniumCsharpTests.Utility
{
    public class JsonReader
    {
        /// <summary>
        /// Extracts a string value from a JSON file given a token name.
        /// </summary>
        /// <param name="path">The path to the JSON file.</param>
        /// <param name="tokenName">The name of the token to extract.</param>
        /// <returns>The string value of the token.</returns>
        public string ExtractData(string path, string tokenName)
        {
            // Read the JSON file into a string.
            string myJsonString = File.ReadAllText(path);

            // Parse the JSON string into a JToken object.
            JToken jsonObject = JToken.Parse(myJsonString);

            // Select the specified token and return its string value.
            return jsonObject.SelectToken(tokenName)?.Value<string>() ?? String.Empty;
        }

        /// <summary>
        /// Extracts an array of string values from a JSON file given a token name.
        /// </summary>
        /// <param name="path">The path to the JSON file.</param>
        /// <param name="tokenName">The name of the token to extract.</param>
        /// <returns>An array of string values.</returns>
        public string[] ExtractDataArray(string path, string tokenName)
        {
            // Read the JSON file into a string.
            string myJsonString = File.ReadAllText(path);

            // Parse the JSON string into a JToken object.
            JToken jsonObject = JToken.Parse(myJsonString);

            // Select the specified token and return its string values as an array.
            List<string> productsList = jsonObject.SelectTokens(tokenName).Values<string>().ToList();
            return productsList.ToArray();
        }
    }
}