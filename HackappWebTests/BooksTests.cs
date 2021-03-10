using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NLog;
using NUnit.Framework;
using OpenQA.Selenium;

namespace HackappWebTests
{
    /// <summary>
    /// LoginTests содержит набор UI-тестов, проверяющих страницу входа.
    /// </summary>
    [TestFixture]
    public class BooksTest : TestBase
    {
        private LoginPage loginPage;
        private SignupPage signupPage;
        private BooksPage booksPage;

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
        public void Exists_BookTitle()
        {
            string testName = "Exists_BookTitle";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testbooksA";
                string password = "book10";

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                List<string> bookTitles = booksPage.GetBookTitlesText();

                Assert.IsFalse(bookTitles.Contains(""), "Пустое название книги");
            }
            catch
            {   
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void Exists_BookAuthor()
        {
            string testName = "Exists_BookAuthor";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testbooksB";
                string password = "book11";

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                List<string> bookAuthors = booksPage.GetBookAuthorText();

                Assert.IsFalse(bookAuthors.Contains(""), "Отсутствует автор книги");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void Exists_BookRating()
        {
            string testName = "Exists_BookRating";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testbooksC";
                string password = "book12";

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                List<string> bookRatingImage = booksPage.GetBookRatingImageRef();
                List<string> bookRatingNumber = booksPage.GetBookRatingNumberText();
                
                Assert.IsFalse(bookRatingImage.Contains(""), "Нет картинки рейтинга книги");
                Assert.IsFalse(bookRatingNumber.Contains(""), "Нет значения рейтинга книги");
            }
            catch
            {
                LogException(testName);
            }
           
            LogEnd(testName);
        }

        [Test]
        public void Exists_BookDescription()
        {
            string testName = "Exists_BookDescription";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testbooksD";
                string password = "book13";

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                List<string> bookDescriptions = booksPage.GetBookDescriptionsText();

                Assert.IsFalse(bookDescriptions.Contains(""), "Пустое описание книги");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void Exists_BookActualPrice()
        {
            string testName = "Exists_BookActualPrice";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testbooksE";
                string password = "book14";

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                List<string> bookPrices = Page.GetBookActualPricesText(driver);

                Assert.IsFalse(bookPrices.Contains(""), "Нет цены книги");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void Exists_BookOldPrice()
        {
            string testName = "Exists_BookOldPrice";
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
                List<string> bookOldPrices = booksPage.GetBookOldPricesText();                

                Assert.IsFalse(bookOldPrices.Contains(""), "Нет старой цены книги");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }
        
        [Test]
        public void CantBeZero_BookOldPrices()
        {
            string testName = "CantBeZero_BookAllPrices";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testbooksG";
                string password = "book16";
                int notExpectedPrice = 0;

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                //Берем старые цены
                List<string> bookOldPricesText = booksPage.GetBookOldPricesText();
                List<double> bookOldPricesDouble = new List<double>();
                //Переводим цену из string в double
                foreach (var price in bookOldPricesText)
                    bookOldPricesDouble.Add(double.Parse(price.Replace('.',',').Remove(price.LastIndexOf(' '), 2)));

                Assert.IsFalse(bookOldPricesDouble.Contains(notExpectedPrice), "Старая цена книги равна 0");

            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void CantBeZero_ActualPrices()
        {
            string testName = "CantBeZero_ActualPrices";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testbooksH";
                string password = "book17";
                int notExpectedPrice = 0;

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                //Берем актуальные цены
                List<double> bookActualPricesDouble = Page.GetBookActualPricesDouble(driver);

                Assert.IsFalse(bookActualPricesDouble.Contains(notExpectedPrice), "Актуальная цена книги равна 0");

            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void Exists_ButtonAddBook()
        {
            string testName = "Exists_ButtonAddBook";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testbooksvI";
                string password = "book18";

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                List<IWebElement> bookAddButtons = booksPage.GetBookAddButtons();                

                Assert.IsFalse(bookAddButtons.Contains(null), "Нет кнопки 'добавить в корзину'");

            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void WatchBooks_DontLogin()
        {
            string testName = "WatchBooks_DontLogin";
            LogStart(testName);
            try
            {
                string expectedTitle = "Products - Testmart";

                booksPage.Navigate();                
                
                Assert.AreEqual(expectedTitle, driver.Title, "Нельзя без авторизации зайти на страницу каталога книг");
                Assert.IsTrue(Page.GetBooks(driver).Count > 0, "Нельзя без авторизации просматривать книги");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void Correct_BookISBN13()
        {
            string testName = "Correct_BookISBN13";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testbooksvJ";
                string password = "book19";
                Regex expectedRegex = new Regex(@"\D*\d{3}-\d{10}\D*");

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                List<string> bookISBN13 = booksPage.GetBookISBN13();

                Assert.IsFalse(bookISBN13.Select(book => expectedRegex.IsMatch(book)).Contains(false), "ISBN-13 отображен некорректно");

            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }
        [Test]
        public void Correct_BookISBN10()
        {
            string testName = "Correct_BookISBN10";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testbooksvK";
                string password = "book20";
                Regex expectedRegex = new Regex(@"\D*\d{10}\D*");

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                List<string> bookISBN10 = booksPage.GetBookISBN10();

                Assert.IsFalse(bookISBN10.Select(book => expectedRegex.IsMatch(book)).Contains(false), "ISBN-10 отображен некорректно");

            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }
    }
}
