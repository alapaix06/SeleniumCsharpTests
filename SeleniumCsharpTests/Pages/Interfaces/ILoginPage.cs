namespace SeleniumCsharpTests.Pages.Interfaces
{

    /// <summary>
    /// This interface defines the contract for a login page in a web application.
    /// </summary>
    public interface ILoginPage
    {
        /// <summary>
        /// This method performs a valid login with the provided username and password.
        /// </summary>
        /// <param name="user">The username to use for the login.</param>
        /// <param name="pass">The password to use for the login.</param>
        /// <returns>The products page that is displayed after a successful login.</returns>
        ProductsPage PerformValidLogin(string user, string pass);
    }
}