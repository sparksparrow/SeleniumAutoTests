using System;
using OpenQA.Selenium;

namespace HackappWebTests
{
    public class LoginPage : Page
    {
        public LoginPage(IWebDriver driver)
            : base(driver, "login")
        {
        }

        public void Navigate()
        {
            driver.Navigate().GoToUrl(Url);
        }

        public void Login(string username, string password)
        {
            if (username == null)
            {
                throw new ArgumentNullException(nameof(username));
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            SetInput("username", username);
            SetInput("password", password);
            ClickElementById("submit");
        }

        public string ErrorMessage()
        {
            return GetElementText("msg-error");
        }
    }
}
