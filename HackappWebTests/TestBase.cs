using System;
using System.Diagnostics;
using NLog;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace HackappWebTests
{
    public class TestBase
    {
        // PowerShell: 
        //   cd HackappTests\HackappWebTests\bin\Debug
        //   Get-Content -Wait .\test.log
        protected static readonly Logger log = LogManager.GetCurrentClassLogger();

        protected ChromeDriverService service;
        //protected EdgeDriverService service;
        //protected FirefoxDriverService service;
        protected IWebDriver driver;

        /// <summary>
        /// Init вызывается один раз перед запуском тестов.
        /// </summary>
        [OneTimeSetUp]
        public void Init()
        {
            service = ChromeDriverService.CreateDefaultService();
            //service = EdgeDriverService.CreateDefaultService();
            //service = FirefoxDriverService.CreateDefaultService();
        }

        [OneTimeTearDown]
        public void Cleanup()
        {
            service.Dispose();
            KillDanglingProcesses();
        }

        protected IWebDriver CreateDriver()
        {
            return new ChromeDriver(service);
            //return new EdgeDriver(service);
            //return new FirefoxDriver(service);
        }

        private void KillDanglingProcesses()
        {
            var processes = Process.GetProcessesByName("chromedriver");
            foreach (var p in processes)
            {
                if (!p.HasExited)
                {
                    p.Kill();
                }
            }
        }
        //Лог "Начала теста"
        public void LogStart(string testName)
        {
            log.Info($"== Test \"{testName}\" started");
        }
        //Лог "Окончание теста"
        public void LogEnd(string testName)
        {
            log.Info($"== Test \"{testName}\" completed");

        }
        //Лог "Появление исключения"
        public void LogException(string testName)
        {
            log.Error($"== An exception occured in test \"{testName}\"");
        }
    }
}
