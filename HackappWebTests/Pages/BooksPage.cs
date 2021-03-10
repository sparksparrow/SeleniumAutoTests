using System;
using System.Collections.Generic;
using OpenQA.Selenium;

namespace HackappWebTests
{
    public class BooksPage : Page
    {
        public BooksPage(IWebDriver driver)
            : base(driver, "books")
        {
        }

        public void Navigate()
        {
            driver.Navigate().GoToUrl(Url);
        }
        //Возвращает название книг с текущей страницы 
        public List<string> GetBookTitlesText()
        {
            IReadOnlyCollection<IWebElement> books = GetBooks(driver);
            List<string> bookTitles = new List<string>();

            foreach (var element in books)
                bookTitles.Add(element.FindElement(By.XPath(".//h2[@class='title is-4']")).Text);                
            
            return bookTitles;
        }
        //Возвращает авторов книг с текущей страницы 
        public List<string> GetBookAuthorText()
        {
            IReadOnlyCollection<IWebElement> books = GetBooks(driver);
            List<string> bookDescriptions = new List<string>();

            foreach (var element in books)
                bookDescriptions.Add(element.FindElement(By.XPath(".//div[@class='content']/p[@class='subtitle is-6']")).Text);
            
            return bookDescriptions;
        }
        //Возвращает картинку рейтинга книг с текущей страницы 
        public List<string> GetBookRatingImageRef()
        {
            IReadOnlyCollection<IWebElement> books = GetBooks(driver);
            List<string> bookDescriptions = new List<string>();

            foreach (var element in books)            
                bookDescriptions.Add(element.FindElement(By.XPath(".//div[@class='level-item']/img[@alt='Book rating']")).GetAttribute("src"));
            
            return bookDescriptions;
        }
        //Возвращает рейтинг книг с текущей страницы 
        public List<string> GetBookRatingNumberText()
        {
            IReadOnlyCollection<IWebElement> books = GetBooks(driver);
            List<string> bookDescriptions = new List<string>();

            foreach (var element in books)
                bookDescriptions.Add(element.FindElement(By.XPath(".//div[@class='level-item']/small")).Text);
            
            return bookDescriptions;
        }
        //Возвращает описание книг с текущей страницы 
        public List<string> GetBookDescriptionsText()
        {
            IReadOnlyCollection<IWebElement> books = GetBooks(driver);
            List<string> bookDescriptions = new List<string>();

            foreach (var element in books)
                bookDescriptions.Add(element.FindElement(By.XPath(".//div[@class='content']/p[not(contains(@class, 'subtitle is-6'))]")).Text);
            
            return bookDescriptions;
        }
        //Возвращает старые цены книг с текущей страницы 
        public List<string> GetBookOldPricesText()
        {
            IReadOnlyCollection<IWebElement> books = GetBooks(driver);
            List<string> bookDescriptions = new List<string>();

            foreach (var element in books)
                bookDescriptions.Add(element.FindElement(By.XPath(".//small/span[@style='text-decoration: line-through;']")).Text);

            return bookDescriptions;
        }
        //Возвращает кнопки "добавить книгу в корзину" с текущей страницы 
        public List<IWebElement> GetBookAddButtons()
        {
            IReadOnlyCollection<IWebElement> books = GetBooks(driver);
            List<IWebElement> bookDescriptions = new List<IWebElement>();

            foreach (var element in books)
            {
                try
                {
                    bookDescriptions.Add(element.FindElement(By.XPath(".//a[@class='button is-success']")));
                }
                catch
                {
                    bookDescriptions.Add(null);
                }
            }             

            return bookDescriptions;
        }
        //Возвращает ISBN-13 книг с текущей страницы 
        public List<string> GetBookISBN13()
        {
            IReadOnlyCollection<IWebElement> books = GetBooks(driver);
            List<string> bookISBN13 = new List<string>();

            foreach (var element in books)
            {
                try
                {                    
                    bookISBN13.Add(element.FindElement(By.XPath(".//figure[@class='media-left']/p[string-length(text())=16]")).Text);
                }
                catch
                {
                    bookISBN13.Add(null);
                }
            }

            return bookISBN13;
        }
        //Возвращает ISBN-10 книг с текущей страницы 
        public List<string> GetBookISBN10()
        {
            IReadOnlyCollection<IWebElement> books = GetBooks(driver);
            List<string> bookISBN10 = new List<string>();

            foreach (var element in books)
            {
                try
                {
                    bookISBN10.Add(element.FindElement(By.XPath(".//figure[@class='media-left']/p[string-length(text())=12]")).Text);
                }
                catch
                {
                    bookISBN10.Add(null);
                }
            }

            return bookISBN10;
        }
    }
}
