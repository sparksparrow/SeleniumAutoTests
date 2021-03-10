using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace HackappWebTests
{
    [TestFixture]
    public class SignupTests : TestBase
    {
        private SignupPage signupPage;

        [SetUp]
        public void BeforeTest()
        {
            driver = CreateDriver();
            driver.Manage().Window.Maximize();
            signupPage = new SignupPage(driver);
        }

        [TearDown]
        public void AfterTest()
        {
            driver.Quit();
        }

        [Test]
        public void SuccessSignup()
        {
            string testName = "SuccessSignup";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testsignupA";
                string password = "sign10";

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);

                Assert.IsNull(signupPage.ErrorMessage(), "Ошибка, нельзя зарегистрироваться с валидными данными (имя, логин, пароль)");
                Assert.IsNotNull(signupPage.SuccessMessage(), "Сообщение об успешной регистрации");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
            //throw new NotImplementedException();
        }

        [Test]
        public void SuccessSignup_WithoutYourname()
        {
            string testName = "SuccessSignup_WithoutYourname";
            LogStart(testName);
            try
            {
                string yourname = "";
                string username = "testsignupB";
                string password = "sign11";

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);

                Assert.IsNull(signupPage.ErrorMessage(), "Ошибка, нельзя зарегистрироваться без имени");
                Assert.IsNotNull(signupPage.SuccessMessage(), "Отсутствует сообщение об успешной регистрации");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
            //throw new NotImplementedException();
        }

        [Test]
        public void CantSignup_UsernameFourCharacters()
        {
            string testName = "CantSignup_UsernameFourCharacters";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "test";
                string password = "sign12";

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);

                Assert.IsNotNull(signupPage.ErrorMessage(), "Ошибка, нельзя зарегистрироваться с логином менее 5 символов (4 символа)");
                Assert.IsNull(signupPage.SuccessMessage(), "Отсутствует сообщение об успешной регистрации");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
            //throw new NotImplementedException();
        }

        [Test]
        public void CantSignup_PasswordOnlyEnglishLetters()
        {
            string testName = "CantSignup_PasswordOnlyEnglishLetters";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testsignupC";
                string password = "signup";

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);

                Assert.IsNotNull(signupPage.ErrorMessage(), "Ошибка, нельзя зарегистрироваться с паролем, состоящим из букв");
                Assert.IsNull(signupPage.SuccessMessage(), "Отсутствует сообщение об успешной регистрации");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
            //throw new NotImplementedException();
        }

        [Test]
        public void CantSignup_PasswordFourCharacters()
        {
            string testName = "CantSignup_PasswordFourCharacters";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testsignupD";
                string password = "sig1";

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);

                Assert.IsNotNull(signupPage.ErrorMessage(), "Ошибка, нельзя зарегистрироваться с паролем менее 5 символов");
                Assert.IsNull(signupPage.SuccessMessage(), "Отсутствует сообщение об успешной регистрации");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
            //throw new NotImplementedException();
        }

        [Test]
        public void CantSignup_TwoIdenticalUsers()
        {
            string testName = "CantSignup_TwoIdenticalUsers";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testsignupE";
                string password = "sign13";

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);

                Assert.IsNotNull(signupPage.ErrorMessage(), "Ошибка, нельзя зарегистрировать вторую идентичную почту");
                Assert.IsNull(signupPage.SuccessMessage(), "Отсутствует сообщение об успешной регистрации");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
            //throw new NotImplementedException();
        }

        [Test]
        public void CantSignup_WithoutConfirmPassword()
        {
            string testName = "CantSignup_WithoutConfirmPassword";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testsignupF";
                string password = "sign14";

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password, "");

                Assert.IsNotNull(signupPage.ErrorMessage(), "Ошибка, нельзя зарегистрировать пользователя без повторения пароля");
                Assert.IsNull(signupPage.SuccessMessage(), "Отсутствует сообщение об успешной регистрации");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
            //throw new NotImplementedException();
        }
    }
}
