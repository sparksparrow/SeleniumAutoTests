using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace HackappWebTests
{
    public class OrdersPage : Page
    {
        public OrdersPage(IWebDriver driver)
            : base(driver, "orders")
        {
        }

        public void Navigate(int id)
        {
            driver.Navigate().GoToUrl($"{Url}/{id}");
        }
        //Возвращает дату доставки заказа
        public Nullable<DateTime> GetDeliveryDate()
        {
            try
            {
                string dateString = driver.FindElement(By.XPath("//div[@class='content']/p/span[@class='tag is-success is-light is-medium']")).Text;
                string[] date = dateString.Split('-');
                return new DateTime(int.Parse(date[2]), int.Parse(date[1]), int.Parse(date[0]));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
        //Возвращает номера книг в заказе
        public List<string> GetBookIds()
        {
            IReadOnlyCollection<IWebElement> bookIds = driver.FindElements(By.XPath("//table[@class='table']/tbody//th"));
            List<string> Ids = new List<string>();

            foreach (var element in bookIds)
                Ids.Add(element.Text);

            return Ids;
        }
        //Возвращает итоговое количество книг в заказе
        public Nullable<int> GetTotalAmount()
        {
            List<IWebElement> booksTotalAmount = driver.FindElements(By.XPath("//table[@class='table']/tfoot//th[3]")).ToList();
            if (int.TryParse(booksTotalAmount[0].Text, out int amount))
                return amount;
            else
                return null;
        }
        //Возвращает итоговую цену книг в заказе
        public Nullable<double> GetTotalPrice()
        {                        
                List<IWebElement> bookTotalPrice = driver.FindElements(By.XPath("//table[@class='table']/tfoot//th[4]")).ToList();
                if (double.TryParse(bookTotalPrice[0].Text.Replace('.', ',').Remove(bookTotalPrice[0].Text.LastIndexOf(' '), 2), out double price))
                    return price; 
                else
                    return null;
        }
        //Возвращает список число каждой книги в заказе
        public List<int> GetListAmountsEveryBook()
        {
            List<IWebElement> bookAmountsWeb = driver.FindElements(By.XPath("//table[@class='table']/tbody/tr/td[2]")).ToList();
            List<int> bookAmounts = new List<int>();

            foreach (var element in bookAmountsWeb)
                if (int.TryParse(element.Text, out int result))
                    bookAmounts.Add(result);

                return bookAmounts;
        }
        //Возвращает список цен каждой книги в заказе
        public List<double> GetListPricesEveryBook()
        {
            List<IWebElement> bookPricesWeb = driver.FindElements(By.XPath("//table[@class='table']/tbody/tr/td[3]")).ToList();
            List<double> bookPrices = new List<double>();

            foreach (var element in bookPricesWeb)
                if (double.TryParse(element.Text.Replace('.',','), out double result))
                    bookPrices.Add(result);

            return bookPrices;
        }
        //Возвращает номер заказа
        public Nullable<int> GetIdOrder()
        {
            try
            {
               string IdOrderText = driver.FindElement(By.XPath("//section[@class='section']/h1[@class='title']")).Text;
                Regex regexInt = new Regex(@"\D*");

                return int.Parse(regexInt.Replace(IdOrderText, ""));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
        //Возвращает дату оформления заказа
        public Nullable<DateTime> GetDateCheckout()
        {
            try
            {
                //формат даты
                Regex regexDate = new Regex(@"\d{2}.\d{2}.\d{4}");
                //формат времени
                Regex regexTime = new Regex(@"\d{2}:\d{2}:\d{2}");  
                string DateCheckoutText = driver.FindElement(By.XPath("//section[@class='section']/div[@class='content']/p[1]")).Text;
   
                var date = regexDate.Match(DateCheckoutText).Value.Split('.').Select(str=>int.Parse(str)).ToList();
                var time = regexTime.Match(DateCheckoutText).Value.Split(':').Select(str => int.Parse(str)).ToList();

                return new DateTime(date.ElementAt(2), date.ElementAt(1), date.ElementAt(0), time.ElementAt(0), time.ElementAt(1), time.ElementAt(2));
            }
            catch
            {
                return null;
            }
        }

    }
}
