using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace HackappWebTests
{
    /// <summary>
    /// LoginTests содержит набор UI-тестов, проверяющих страницу входа.
    /// </summary>
    [TestFixture]
    public class LoginTests : TestBase
    {
        private LoginPage loginPage;
        private SignupPage singupPage;

        /// <summary>
        /// BeforeTest вызывается перед запуском кажого тестов.
        /// </summary>
        [SetUp]
        public void BeforeTest()
        {
            driver = CreateDriver();
            driver.Manage().Window.Maximize();
            loginPage = new LoginPage(driver);
            singupPage = new SignupPage(driver);
        }

        /// <summary>
        /// AfterTest вызывается после завершения каждого теста.
        /// </summary>
        [TearDown]
        public void AfterTest()
        {
            driver.Quit();
        }

        [Test]
        public void CantLogin_InvalidCredentials()
        {
            string testName = "CantLogin_InvalidCredentials";
            LogStart(testName);
            try 
            { 
                string username = "wrongUser";
                string password = "wrongPassword";

                loginPage.Navigate();
                loginPage.Login(username, password);

                Assert.IsNotNull(loginPage.ErrorMessage(), "Не правильный Username или Password.");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void CantLogin_EmptyCredentials()
        {
            string testName = "CantLogin_EmptyCredentials";
            LogStart(testName);
            try 
            { 
                string username = "";
                string password = "";

                loginPage.Navigate();
                loginPage.Login(username, password);

                Assert.IsNotNull(loginPage.ErrorMessage(), "Ошибка, можно залогинится с пустыми Username или Password.");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void SuccessLogin_ValidCredentials()
        {
            string testName = "SuccessLogin_ValidCredentials";
            LogStart(testName);
            try 
            { 
               string yourname = "Alexey";
               string username = "testloginA";
               string password = "logi10";
               
               singupPage.Navigate();
               singupPage.Signup(yourname, username, password);
               loginPage.Navigate();
               loginPage.Login(username, password);

               // TODO: Написать код, зарегистрирующий нового пользователя, на Demo надо сначала руками зарегистрировать такого пользоваля!

               Assert.IsNull(loginPage.ErrorMessage(), "Ошибка, нельзя залогиниться с валидными данными (имя, логин, пароль)");
               Assert.IsNotNull(Page.GetSignout(driver), "Signout button");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void SuccessLogout()
        {
            string testName = "SuccessLogout";
            LogStart(testName);
            try 
            { 
                string yourname = "Alexey";
                string username = "testloginB";
                string password = "logi11";

                // TODO: Написать код, зарегистрирующий нового пользователя, на Demo надо сначала руками зарегистрировать такого пользоваля!

                singupPage.Navigate();
                singupPage.Signup(yourname, username, password);

                loginPage.Navigate();
                loginPage.Login(username, password);
                Page.GetSignout(driver).Click();

                Assert.IsNull(Page.GetSignout(driver), "No Signout button");
                Assert.DoesNotThrow(() => driver.FindElement(By.CssSelector("a.button[href=\"/login\"]")), "Login link");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void SuccessLogin_PasswordEightCharacters()
        {
            string testName = "SuccessLogin_PasswordEightCharacters";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testloginC";
                string password = "login111";

                singupPage.Navigate();
                singupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);

                Assert.IsNull(loginPage.ErrorMessage(), "Ошибка, нельзя залогиниться с паролем из 8 символов");
                Assert.IsNotNull(Page.GetSignout(driver), "Signout button");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void SuccessLogin_PasswordRussianCharacters()
        {
            string testName = "SuccessLogin_PasswordRussianCharacters";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testloginD";
                string password = "паро12";

                singupPage.Navigate();
                singupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);

                Assert.IsNull(loginPage.ErrorMessage(), "Ошибка, нельзя залогиниться с паролем из цифр и русских букв");
                Assert.IsNotNull(Page.GetSignout(driver), "Signout button");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }


        /* [Test]
         public void ExampleWithLogging()
         {
             // Click "Open additional output for this result" in test results to see logging messages.
             // Or open file in bin/Debug/tests.log.
             log.Info("== TestWithLogging started");

             string actual = "Success";
             Assert.AreEqual("Success", actual);

             log.Info("== TestWithLogging completed");
         }*/
    }
}
