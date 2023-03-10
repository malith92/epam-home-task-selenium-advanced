using NUnit.Framework;
using HomeTaskWebDriverAdvanced.PageObjects;
using HomeTaskWebDriverAdvanced.Driver;
using HomeTaskWebDriverAdvanced.WebDriver;
using OpenQA.Selenium.DevTools;
using log4net;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;

namespace HomeTaskWebDriverAdvanced.Tests
{
    [TestFixture]
    [Parallelizable(ParallelScope.All)]
    public class LoginPageTests : BaseTestObject
    {
        private readonly ILog log = LogManager.GetLogger(typeof(LoginPageTests));

        private LoginPage? _loginPage;
        private GoogleDriveHomePage? _emailAccountPage;

        [Test]
        public void LoginTest()
        {
            _loginPage = new LoginPage(webDriver);

            _emailAccountPage = _loginPage.Login(configurationFileReader.TestData.EmailCredentials.UserName, configurationFileReader.TestData.EmailCredentials.Password);
            bool loginSuccessful = _emailAccountPage.DriveLinkIsDisplayed();

            Assert.IsTrue(loginSuccessful);
        }

        [SetUp]
        public void SetUp()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            webDriver = new RemoteWebDriver(new Uri("http://localhost:4444"), chromeOptions);

            log.Info("Browser opened");

            webDriver.Navigate().GoToUrl(configurationFileReader.ConfigData.AppUrl);

            log.Info("Navigated to Url");
        }

        [TearDown]
        public void TearDown()
        {
            webDriver.Quit();
            log.Info("Browser closed");
        }
    }
}