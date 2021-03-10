using System;
using OpenQA.Selenium;

namespace HackappWebTests
{
    class SignupPage : Page
    {
        public SignupPage(IWebDriver driver)
            : base(driver, "signup")
        {
        }

        public void Navigate()
        {
            driver.Navigate().GoToUrl(Url);
        }

        //Регистрация аккаунта
        public void Signup(string YourName, string username, string password, string passwordConfirm)
        {
            if (username == null)
            {
                throw new ArgumentNullException(nameof(username));
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }
            if (passwordConfirm == null)
            {
                throw new ArgumentNullException(nameof(passwordConfirm));
            }

            SetInput("name", YourName);
            SetInput("username", username);
            SetInput("password", password);
            SetInput("password-confirm", passwordConfirm);
            ClickElementByXPath("//button[@class='button is-link']");
        }

        //Регистрация акканта, пароль подтвеждается автоматически
        public void Signup(string YourName, string username, string password)
        {
            if (username == null)
            {
                throw new ArgumentNullException(nameof(username));
            }
            if (password == null)
            {
                throw new ArgumentNullException(nameof(password));
            }

            SetInput("name", YourName);
            SetInput("username", username);
            SetInput("password", password);
            SetInput("password-confirm", password);
            ClickElementByXPath("//button[@class='button is-link']");
        }

        //Вернуть текст сообщения об ошибке регистрации
        public string ErrorMessage()
        {
            return GetElementTextByXPath("//div[@class='notification is-danger']");            
        }
        //Вернуть текст сообщения об успешной регистрации
        public string SuccessMessage()
        {
            return GetElementTextByCssSelector("article div.message-body");                 
        }
    }
}
