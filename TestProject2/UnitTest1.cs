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
            Assert.That(actualTitle, Does.Contain(expectedTitle), "���������� �� ������� 'Wikipedia'");
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
            Assert.That(actualTitle, Does.Contain(expectedTitle), "�� ������ 'Selenium'");
        }
        [Test]
        public void Test3()
        {
            _driver.Navigate().GoToUrl("https://www.wikipedia.org/");

            // ������ "Selenium"
            IWebElement searchBox = _driver.FindElement(By.Id("searchInput"));
            searchBox.SendKeys("Selenium");
            searchBox.SendKeys(Keys.Enter);

            // ��������� ���������� �� �� ������
            wait.Until(d => d.FindElement(By.Id("firstHeading")));

            // ���������� ������ � ������ �������� "History"
            IWebElement historySection = wait.Until(d => d.FindElement(By.CssSelector("#History, span#History")));

            // ��������� �� ��������, �� �� �� �������� �����
            ((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", historySection);

            // ����������� ���� ������� � "History"
            Assert.That(historySection.Text, Is.EqualTo("History"), "�������� 'History' �� ���� ��������!");
        }
        [Test]
        public void Test4()
        {
            IList<IWebElement> links = _driver.FindElements(By.CssSelector("#mw-content-text a"));

            Assert.That(links.Count, Is.GreaterThan(0), "���� ������� � ��������!");

            // �������� ����� ������ ����
            links[0].Click();

            // ����������� ���� ������ �������� � �������� �� ���������
            Assert.That(_driver.Url, Does.Not.Contain("Selenium"), "������ �� ������� ��� �������� ��������!");
        }
        [Test]
        public void Test5()
        {
            _driver.Navigate().GoToUrl("https://www.wikipedia.org/");

            // ������ � ������ ����� ������� ���� (Deutsch)
            IWebElement germanLanguageLink = _driver.FindElement(By.XPath("//a[@id='js-link-box-de']"));
            germanLanguageLink.Click();

            // �������� ���� URL ������� "de.wikipedia.org"
            Assert.That(_driver.Url, Does.Contain("de.wikipedia.org"), "���������� �� �� ����� �� ������!");
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