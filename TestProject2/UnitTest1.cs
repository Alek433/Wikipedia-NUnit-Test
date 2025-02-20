using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestProject2
{
    public class Tests
    {
        IWebDriver _driver;
        WebDriverWait wait;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            //_driver.Url = "https://www.wikipedia.org";
            _driver.Manage().Window.Maximize();
            wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void Test1()
        {
            _driver.Navigate().GoToUrl("https://www.wikipedia.org/");
            string expectedTitle = "Wikipedia";
            string actualTitle = _driver.Title;
            Assert.That(actualTitle, Does.Contain(expectedTitle), "Заглавието не съдържа 'Wikipedia'");
        }
        [Test]
        public void Test2()
        {
            _driver.Navigate().GoToUrl("https://www.wikipedia.org/");
            IWebElement searchBox = _driver.FindElement(By.Id("searchInput"));
            searchBox.SendKeys("Selenium");

            searchBox.SendKeys(Keys.Enter);

            string expectedTitle = "Selenium";
            string actualTitle = _driver.Title;
            Assert.That(actualTitle, Does.Contain(expectedTitle), "Не намира 'Selenium'");
        }
        [Test]
        public void Test3()
        {
            _driver.Navigate().GoToUrl("https://www.wikipedia.org/");

            // Търсим "Selenium"
            IWebElement searchBox = _driver.FindElement(By.Id("searchInput"));
            searchBox.SendKeys("Selenium");
            searchBox.SendKeys(Keys.Enter);

            // Изчакваме страницата да се зареди
            wait.Until(d => d.FindElement(By.Id("firstHeading")));

            // Превъртаме надолу и търсим секцията "History"
            IWebElement historySection = wait.Until(d => d.FindElement(By.CssSelector("#History, span#History")));

            // Скролваме до елемента, за да го направим видим
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", historySection);

            // Проверяваме дали текстът е "History"
            Assert.That(historySection.Text, Is.EqualTo("History"), "Секцията 'History' не беше намерена!");
        }
        [Test]
        public void Test4()
        {
            IList<IWebElement> links = _driver.FindElements(By.CssSelector("#mw-content-text a"));

            Assert.That(links.Count, Is.GreaterThan(0), "Няма линкове в статията!");

            // Кликваме върху първия линк
            links[0].Click();

            // Проверяваме дали новата страница е различна от началната
            Assert.That(_driver.Url, Does.Not.Contain("Selenium"), "Линкът не отвежда към различна страница!");
        }
        [Test]
        public void Test5()
        {
            _driver.Navigate().GoToUrl("https://www.wikipedia.org/");

            // Намери и кликни върху немския език (Deutsch)
            IWebElement germanLanguageLink = _driver.FindElement(By.XPath("//a[@id='js-link-box-de']"));
            germanLanguageLink.Click();

            // Проверка дали URL съдържа "de.wikipedia.org"
            Assert.That(_driver.Url, Does.Contain("de.wikipedia.org"), "Страницата не се смени на немски!");
        }


    [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
        /*private void AcceptCookies()
        {
            var element = _driver.FindElement(By.Id("diodomi-"));
            element.Click();

            var signInButton = _driver.FindElement(By.XPath);
        }*/
    }
}