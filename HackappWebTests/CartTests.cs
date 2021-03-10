using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace HackappWebTests
{
    [TestFixture]
    public class CartTests : TestBase
    {
        private LoginPage loginPage;
        private SignupPage signupPage;
        private BooksPage booksPage;
        private CartPage cartPage;

        /// <summary>
        /// BeforeTest вызывается перед запуском кажого тестов.
        /// </summary>
        [SetUp]
        public void BeforeTest()
        {
            driver = CreateDriver();
            driver.Manage().Window.Maximize();
            loginPage = new LoginPage(driver);
            signupPage = new SignupPage(driver);
            booksPage = new BooksPage(driver);
            cartPage = new CartPage(driver);
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
        public void SuccessAddBook()
        {
            string testName = "SuccessAddBook";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testcartA";
                string password = "cart10";

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                List<IWebElement> bookAddButtons = booksPage.GetBookAddButtons();
                bookAddButtons[0].Click();
                cartPage.Navigate();

                Assert.IsNull(cartPage.CartEmptyMessage(), "Ошибка, нет сообщения, что корзина пуста");
                Assert.IsNotNull(Page.GetBooks(driver), "В корзине нет книги");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void SuccessUpdateAmountBooks()
        {
            string testName = "SuccessUpdateAmountBooks";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testcartB";
                string password = "cart11";
                int expectedAmount = 4;

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                List<IWebElement> bookAddButtons = booksPage.GetBookAddButtons();
                //Добавить в корзину первую книгу
                bookAddButtons[0].Click();
                //Добавить в корзину вторую книгу
                bookAddButtons[1].Click();
                cartPage.Navigate();
                //Изменить количество первой книги на 3
                cartPage.SetInputInUpdateField(0, 3);
                //Нажать кнопку обновить у первой книги
                cartPage.GetUpdateButtons()[0].Click();

                Assert.IsNotNull(Page.GetBooks(driver), "В корзине нет книги");
                Assert.AreEqual(expectedAmount, cartPage.GetAmountBooksToBuy(), "Количество к покупке не соответствует количеству добавленных книг");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void SuccessDeleteOneBook_OneBookAdded()
        {
            string testName = "SuccessDeleteOneBook_OneBookAdded";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testcartC";
                string password = "cart12";
                int expectedCount = 0;

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                List<IWebElement> bookAddButtons = booksPage.GetBookAddButtons();
                //Добавить в корзину первую книгу
                bookAddButtons[0].Click();
                cartPage.Navigate();
                cartPage.GetDeleteButtons()[0].Click();

                Assert.IsTrue(Page.GetBooks(driver).Count() == expectedCount, "В корзине есть книги");

            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void SuccessDeleteOneBook_TwoBooksAdded()
        {
            string testName = "SuccessDeleteOneBook_TwoBooksAdded";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testcartD";
                string password = "cart13";

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                List<IWebElement> bookAddButtons = booksPage.GetBookAddButtons();
                //Добавить в корзину первую книгу
                bookAddButtons[0].Click();
                //Добавить в корзину вторую книгу
                bookAddButtons[1].Click();
                cartPage.Navigate();
                //Удалить первую книгу
                cartPage.GetDeleteButtons()[0].Click();

                Assert.IsTrue(Page.GetBooks(driver).Count != 0, "Корзина полность очистилась");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void CorrectDisplay_AmountBooksInCartButton()
        {
            string testName = "CorrectDisplay_AmountBooksInCartButton";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testcartE";
                string password = "cart14";
                int expectedAmount = 5;

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                List<IWebElement> bookAddButtons = booksPage.GetBookAddButtons();
                //Добавить в корзину первую книгу 3 раза
                for (int i = 0; i < 3; i++)
                    bookAddButtons[0].Click();
                //Добавить в корзину вторую книгу 2 раза
                for (int i = 0; i < 2; i++)
                    bookAddButtons[1].Click();
                cartPage.Navigate();

                Assert.AreEqual(expectedAmount, cartPage.GetAmountBooksFromCartButton(), "Кнопка 'корзина' отображает неверное количество книг");

            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void CanAddBooks_Everyone()
        {
            string testName = "CanAddBooks_Everyone";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testbooksF";
                string password = "book15";

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                List<string> booksInCatalog = booksPage.GetBookTitlesText();
                //Сортируем на случай, если книги добавленные в корзину, буду в другой последовательности
                booksInCatalog.Sort();
                //Добавляем все книги в корзину
                booksPage.GetBookAddButtons().ForEach(btn => btn.Click());
                cartPage.Navigate();
                List<string> booksInCart = booksPage.GetBookTitlesText();
                //Как и книги изи каталога, сортируем книги в корзину
                booksInCart.Sort();

                Assert.AreEqual(booksInCatalog, booksInCart, "Не все книги добавились в корзину");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void CanAddBook_ThreeTimes()
        {
            string testName = "CanAddBook_ThreeTimes";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testbooksG";
                string password = "book16";
                int expectedAmount = 3;

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                List<IWebElement> bookAddButtons = booksPage.GetBookAddButtons();
                //Добавить в корзину первую книгу 3 раза
                for (int i = 0; i < expectedAmount; i++)
                    bookAddButtons[0].Click();
                cartPage.Navigate();

                //cartPage.GetValueFromUpdateFields()[0] - берет значения из поля "количество книг" у первой книги (индексация начинается с 0)
                Assert.AreEqual(expectedAmount, cartPage.GetValueFromUpdateFields()[0], "В корзине количество книг не равно 3");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void CantAddBook_DontLogin()
        {
            string testName = "CantAddBook_DontLogin";
            LogStart(testName);
            try
            {
                string expectedTitle = "Log in - Testmart";

                booksPage.Navigate();
                List<IWebElement> bookAddButtons = booksPage.GetBookAddButtons();
                bookAddButtons[0].Click();

                Assert.AreEqual(expectedTitle, driver.Title, "Переход на форму авторизации не произошел");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void CantOpenCart_DontLogin()
        {
            string testName = "CantOpenCart_DontLogin";
            LogStart(testName);
            try
            {
                string expectedTitle = "Log in - Testmart";

                cartPage.Navigate();

                Assert.AreEqual(expectedTitle, driver.Title, "Переход на форму авторизации не произошел");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }


        [Test]
        public void BookTotalPriceCart_Equal_SumBookPrices()
        {
            string testName = "BookTotalPriceCart_Equal_SumBookPrices";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testbooksH";
                string password = "book17";

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                List<IWebElement> bookAddButtons = booksPage.GetBookAddButtons();
                //Добавляем все книги в корзину
                booksPage.GetBookAddButtons().ForEach(btn => btn.Click());
                cartPage.Navigate();
                List<double> booksActualPrices = Page.GetBookActualPricesDouble(driver);
                double expectedAmount = booksActualPrices.Sum();

                Assert.AreEqual(expectedAmount, cartPage.GetBooksPriceToBuy(), "Сумма цен книг в корзине не равно общей стоимости корзины"); 
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }
    }
}
