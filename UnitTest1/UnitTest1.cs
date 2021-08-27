using System;
using System.Diagnostics;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

using UnitTest1.UI;
using UnitTest1.UI.Pages;

namespace UnitTest1
{
    [TestClass]
    public class UnitTest1
    {
        private IWebDriver _driver;

        [TestInitialize]
        public void SetupTest()
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Console.Out));
            _driver = new ChromeDriver();
            _driver.Url = "https://d21vtxezke9qd9.cloudfront.net/#/";
            _driver.Manage().Window.Maximize();
        }

        [TestMethod]
        public void TestMethod1()
        {
            PlanetPage planetPage = new PlanetPage(_driver);
            planetPage.GotoPage();
            planetPage.LoadPlanets();

            foreach (Planet planet in planetPage.Planets)
            {
                planet.ClickExplore();
                IWebElement popup = _driver.FindElement(By.ClassName("popup-message"));
                new WebDriverWait(_driver, TimeSpan.FromSeconds(2.0d)).Until(_ => popup.Displayed);
                Assert.AreEqual("Exploring " + planet.Name, popup.Text);
                new WebDriverWait(_driver, TimeSpan.FromSeconds(10.0d)).Until(_ => !popup.Displayed);
            }
        }

        [TestCleanup]
        public void CleanupTest()
        {
            _driver.Quit();
        }
    }
}
