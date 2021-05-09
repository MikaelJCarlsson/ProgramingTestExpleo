using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
namespace ExpleoTestApp
{
    public static class Selenium
    {
        public static void WebScraper()
        {
            Console.WriteLine("Problem 2\n\n");
            IWebDriver driver = new ChromeDriver();

            driver.Navigate().GoToUrl("https://google.com");

            IReadOnlyCollection<IWebElement> webElements = driver.FindElements(By.TagName("a"));

            Console.WriteLine("Weblinks for www.google.com:\n");

            foreach (var link in webElements)
            {
                Console.WriteLine(link.GetAttribute("href"));
            }
            driver.Close();
            driver.Quit();
        }
    }
}
