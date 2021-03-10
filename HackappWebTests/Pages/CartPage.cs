using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using OpenQA.Selenium;

namespace HackappWebTests
{
    public class CartPage : Page
    {
        public CartPage(IWebDriver driver)
            : base(driver, "cart")
        {
        }

        public void Navigate()
        {
            driver.Navigate().GoToUrl(Url);
        }
        //Возвращает сообщение "Корзина пуста"
        public string CartEmptyMessage()
        {
            return GetElementTextByXPath("//div[@class='content']/p");
        }
        //Устанавливает значения количества книги
        public void SetInputInUpdateField(int index, int value)
        {
            List<IWebElement> updateFileds = driver.FindElements(By.Id("input")).ToList();
            try
            {
                updateFileds[index].SendKeys(value.ToString());
            }
            catch
            {
                throw new ArgumentNullException(nameof(updateFileds));
            }
        }
        //Возвращает кнопки "обновить"
        public List<IWebElement> GetUpdateButtons()
        {
            return driver.FindElements(By.XPath("//div[@class='field has-addons']/button[@class='button is-success']")).ToList();
        }
        //Возвращает кнопки "удалить"
        public List<IWebElement> GetDeleteButtons()
        {
            return driver.FindElements(By.XPath("//div[@class='level-item']/button[@class='button']")).ToList();
        }
        //Возвращает кнопку "купить"
        public IWebElement GetBuyButton()
        {
            try
            { 
            return driver.FindElement(By.XPath("//div[@class='panel-block']/button[@class='button is-success is-fullwidth']"));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
        //Возвращает количество книг к покупке
        public Nullable<int> GetAmountBooksToBuy()
        {
            try
            {
                string value = driver.FindElement(By.XPath("//div[@class='panel-block']/p[@class='is-size-5']")).Text;
                string amount = Regex.Match(value, @"\d+").Value;
                return int.Parse(amount);
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
        //Возвращает цену книг к покупке
        public Nullable<double> GetBooksPriceToBuy()
        {
            try
            {
                string value = driver.FindElement(By.XPath("//div[@class='panel-block']/p[@class='is-size-5']/strong[@class='has-text-danger']")).Text;
                return double.Parse(value.Replace('.',','));
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
        //Возвращает количество книг с кнопки "корзина"
        public Nullable<int> GetAmountBooksFromCartButton()
        {
            try
            {
                IWebElement amountBooks = GetCartButton().FindElement(By.XPath(".//strong/span[@class='tag']"));
                if (int.TryParse(amountBooks.Text, out int amount))
                    return amount;
                else
                    return null;
            }
            catch (NoSuchElementException)
            {
                return null;
            }
        }
        //Возвращает количество каждой книги
        public List<Nullable<int>> GetValueFromUpdateFields()
        {
            List<IWebElement> updateFileds = driver.FindElements(By.Id("input")).ToList();
            List<Nullable<int>> bookAmounts = new List<Nullable<int>>();

            foreach (var element in updateFileds)
                if (int.TryParse(element.GetAttribute("value"), out int amount))
                    bookAmounts.Add(amount);
                else
                    bookAmounts.Add(null);

            return bookAmounts;

        }
    }
}
