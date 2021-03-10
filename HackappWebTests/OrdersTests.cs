using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;

namespace HackappWebTests
{
    /// <summary>
    /// LoginTests содержит набор UI-тестов, проверяющих страницу входа.
    /// </summary>
    [TestFixture]
    public class OrdersTests : TestBase
    {
        private LoginPage loginPage;
        private SignupPage signupPage;
        private BooksPage booksPage;
        private CartPage cartPage;
        private OrdersPage ordersPage;

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
            ordersPage = new OrdersPage(driver);
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
        public void BookIDs_Diffrent()
        {
            string testName = "BookIDs_Diffrent";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testorderA";
                string password = "orde10";

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
                cartPage.GetBuyButton().Click();
                string notExpectedIdBooks = ordersPage.GetBookIds()[0];

                Assert.NotNull(notExpectedIdBooks, "Id первой книги не существует");
                Assert.NotNull(ordersPage.GetBookIds()[1], "Id второй книги не существует");
                Assert.AreNotEqual(notExpectedIdBooks, ordersPage.GetBookIds()[1], "Id книг в заказе одинаковые");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }



        [Test]
        public void Correct_OrderDeliveryDate()
        {
            string testName = "Correct_OrderDeliveryDate";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testorderB";
                string password = "orde11";

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
                cartPage.GetBuyButton().Click();

                Assert.AreEqual(driver.Title, "Orders - Testmart", "Страница заказов не открылась");
                Assert.IsTrue(DateTime.Today < ordersPage.GetDeliveryDate(), "Дата доставки раньше текущей даты");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void CantOpenOrder_DontLogin()
        {
            string testName = "CantOpenOrder_DontLogin";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testorderC";
                string password = "orde12";
                //Регистрируем нового пользователя
                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                //Авторизуемся
                loginPage.Navigate();
                loginPage.Login(username, password);
                //Добавляем в корзину первую книгу
                booksPage.GetBookAddButtons()[0].Click();
                cartPage.Navigate();
                //Оформляем заказ
                cartPage.GetBuyButton().Click();
                //Получаем URl заказа пользователя
                string notExpectedUrl = driver.Url;
                //Выходим из учетной записи
                Page.GetSignout(driver).Click();

                driver.Navigate().GoToUrl(notExpectedUrl);

                Assert.AreNotEqual(notExpectedUrl, driver.Url, "Неавторизованный пользователь получил доступ к заказу");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }
        [Test]
        public void CantOpenSomeoneElsesOrder_Login()
        {
            string testName = "CantOpenSomeoneElsesOrder_Login";
            LogStart(testName);
            try
            {
                string yournameFirst = "Alexey";
                string usernameFirst = "testorderD";
                string passwordFirst = "orde13";
                string yournameSecond = "Vasya";
                string usernameSecond = "testorderE";
                string passwordSecond = "orde14";

                //Регистрируем нового пользователя
                signupPage.Navigate();
                signupPage.Signup(yournameFirst, usernameFirst, passwordFirst);
                //Авторизуемся
                loginPage.Navigate();
                loginPage.Login(usernameFirst, passwordFirst);
                //Добавляем в корзину первую книгу
                booksPage.GetBookAddButtons()[0].Click();
                cartPage.Navigate();
                //Оформляем заказ
                cartPage.GetBuyButton().Click();
                //Получаем URl заказа первого пользователя
                string notExpectedUrl = driver.Url;
                //Выходим из учетной записи
                Page.GetSignout(driver).Click();
                //Регистрируем второго нового пользователя
                signupPage.Navigate();
                signupPage.Signup(yournameSecond, usernameSecond, passwordSecond);
                //Авторизуемся под вторым пользователем
                loginPage.Navigate();
                loginPage.Login(usernameSecond, passwordSecond);
                //Открываем заказ первого пользователя
                driver.Navigate().GoToUrl(notExpectedUrl);
                
                Assert.AreNotEqual(notExpectedUrl, driver.Url, "Один пользователь получил доступ к заказу другого пользователя");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void BookTotalAmountOrder_Equal_SumBookAmounts()
        {
            string testName = "BookTotalAmountOrder_Equal_SumBookAmounts";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testorderF";
                string password = "orde15";

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                List<IWebElement> bookAddButtons = booksPage.GetBookAddButtons();
                //Добавляем все книги в корзину
                booksPage.GetBookAddButtons().ForEach(btn => btn.Click());
                cartPage.Navigate();
                //Оформляем заказ
                cartPage.GetBuyButton().Click();
                int expectedAmount = ordersPage.GetListAmountsEveryBook().Sum();

                Assert.AreEqual(expectedAmount, ordersPage.GetTotalAmount(), "Суммарное количество оформленных книг не равно итоговому количеству книг в заказе");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void BookTotalPriceOrder_Equal_SumBookPrices()
        {
            string testName = "BookTotalPriceOrder_Equal_SumBookPrices";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testorderG";
                string password = "orde16";

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                List<IWebElement> bookAddButtons = booksPage.GetBookAddButtons();
                //Добавляем все книги в корзину
                booksPage.GetBookAddButtons().ForEach(btn => btn.Click());
                cartPage.Navigate();
                //Оформляем заказ
                cartPage.GetBuyButton().Click();
                double expectedAmount = ordersPage.GetListPricesEveryBook().Sum();

                Assert.AreEqual(expectedAmount, ordersPage.GetTotalPrice(), "Сумма цен оформленных книг не равно итоговой сумме заказа");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void IdOrders_Diffrent()
        {
            string testName = "IdOrders_Diffrent";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testorderH";
                string password = "orde17";

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                //Добавляем первую книги в корзину
                booksPage.GetBookAddButtons()[0].Click();
                cartPage.Navigate();
                //Оформляем заказ
                cartPage.GetBuyButton().Click();
                int? notExpectedIdOrder = ordersPage.GetIdOrder();
                //Возвращаемся в каталог
                booksPage.Navigate();
                //Добавляем первую книги в корзину
                booksPage.GetBookAddButtons()[0].Click();
                //Оформляем второй заказ
                cartPage.Navigate();
                cartPage.GetBuyButton().Click();

                Assert.NotNull(notExpectedIdOrder, "Id не существует");
                Assert.AreNotEqual(notExpectedIdOrder, ordersPage.GetIdOrder(), "Id заказов одинаковые");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

        [Test]
        public void DateСheckout_Today()
        {
            string testName = "DateСheckout_Today";
            LogStart(testName);
            try
            {
                string yourname = "Alexey";
                string username = "testorderI";
                string password = "orde18";

                signupPage.Navigate();
                signupPage.Signup(yourname, username, password);
                loginPage.Navigate();
                loginPage.Login(username, password);
                booksPage.Navigate();
                //Добавляем первую книги в корзину
                booksPage.GetBookAddButtons()[0].Click();
                cartPage.Navigate();
                //Оформляем заказ
                cartPage.GetBuyButton().Click();
                string expectedСheckout = DateTime.Now.ToString();

                Assert.AreEqual(expectedСheckout, ordersPage.GetDateCheckout().ToString(), "Дата заказа не соответсвует текущему времени");
            }
            catch
            {
                LogException(testName);
            }
            LogEnd(testName);
        }

    }
}
