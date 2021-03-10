using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;

namespace HackappWebTests
{
    public class Page
    {
        public static Uri SiteUrl { get; } = new Uri("http://localhost:8080");
        public Uri Url { get; }
        protected IWebDriver driver;

        public Page(IWebDriver driver, string pageUrl)
        {
            this.driver = driver;
            Url = new Uri(SiteUrl, pageUrl);
        }
        
        protected void SetInput(string id, string value)
        {
            IWebElement u = driver.FindElement(By.Id(id));
            if (value == "")
            {
                u.Clear();
            }
            else
            {
                u.SendKeys(value);
            }
        }

        protected string GetElementText(string id)
        {
            if (driver.Url != Url.AbsoluteUri)
            {
                return null;
            }

            try
            {
                return driver.FindElement(By.Id(id)).Text;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        protected string GetElementTextByXPath(string xpath)
        {
            if (driver.Url != Url.AbsoluteUri)
            {
                return null;
            }

            try
            {
                return driver.FindElement(By.XPath(xpath)).Text;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
        protected string GetElementTextByCssSelector(string cssSelector)
        {
            if (driver.Url != Url.AbsoluteUri)
            {
                return null;
            }

            try
            {
                return driver.FindElement(By.CssSelector(cssSelector)).Text;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }

        protected void ClickElementById(string id)
        {
            driver.FindElement(By.Id(id)).Click();
        }

        protected void ClickElementByXPath(string xpath)
        {
            driver.FindElement(By.XPath(xpath)).Click();
        }
        //Возвращает книги с текущей страницы
        public static IReadOnlyCollection<IWebElement> GetBooks(IWebDriver driver)
        {
            return driver.FindElements(By.CssSelector("section article.media")).ToList(); ;
        }
        //Возвращает цены книг с текущей страницы в формате string
        public static List <string> GetBookActualPricesText(IWebDriver driver)
        {
            IReadOnlyCollection<IWebElement> books = GetBooks(driver);
            List<string> bookActualPricesText = new List<string>();

            foreach (var element in books)
                bookActualPricesText.Add(element.FindElement(By.XPath(".//p[@class='is-size-4']/strong[@class='has-text-danger']")).Text);

            return bookActualPricesText;
        }
        //Возвращает цены книг с текущей страницы в формате double
        public static List<double> GetBookActualPricesDouble(IWebDriver driver)
        {
            IReadOnlyCollection<IWebElement> books = GetBooks(driver);
            List<string> bookActualPricesText = new List<string>();

            foreach (var element in books)
                bookActualPricesText.Add(element.FindElement(By.XPath(".//p[@class='is-size-4']/strong[@class='has-text-danger']")).Text);

            List<double> bookActualPricesDouble = new List<double>();
            //Переводим цену из string в double
            foreach (var price in bookActualPricesText)
                bookActualPricesDouble.Add(double.Parse(price.Replace('.', ',').Remove(price.LastIndexOf(' '), 2)));

            return bookActualPricesDouble;
        }

        public static IWebElement GetSignout(IWebDriver driver)
        {
            try
            {
                return driver.FindElement(By.Id("signout"));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
        //Получаем кнопку "корзина"
        public IWebElement GetCartButton()
        {
            return driver.FindElement(By.XPath("//a[@class='button is-primary'][@href='/cart']"));
        }
    }
}
