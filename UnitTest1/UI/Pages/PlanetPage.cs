using System;
using System.Collections.Generic;
using System.Globalization;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UnitTest1.UI.Pages
{
    public sealed class PlanetPage : Page
    {
        private IWebDriver _webDriver;
        private List<Planet> _planets;

        public PlanetPage(IWebDriver webDriver)
        {
            _webDriver = webDriver;
            _planets = new List<Planet>();
        }

        public IReadOnlyCollection<Planet> Planets
        {
            get => _planets.AsReadOnly();

            private set { }
        }

        public void LoadPlanets()
        {
            _planets.Clear();

            foreach (IWebElement element in _webDriver.FindElements(By.ClassName("planet")))
            {
                string name = element.FindElement(By.TagName("h2")).Text;
                string distanceStr = element.FindElement(By.ClassName("distance")).Text;
                string radiusStr = element.FindElement(By.ClassName("radius")).Text;
                long distance = long.Parse(distanceStr.Substring(0, distanceStr.Length - 3), NumberStyles.AllowThousands);
                double radius = double.Parse(radiusStr.Substring(0, radiusStr.Length - 3), NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint);
                _planets.Add(new Planet(element, name, distance, radius));
            }
        }

        public override void GotoPage()
        {
            IWebElement planetsButton = _webDriver.FindElement(By.CssSelector("a[href='#/planets']"));
            new WebDriverWait(_webDriver, TimeSpan.FromSeconds(20.0d)).Until(_ => planetsButton.Displayed);
            planetsButton.Click();
            new WebDriverWait(_webDriver, TimeSpan.FromSeconds(20.0d)).Until(_ => _webDriver.FindElement(By.TagName("body")).Displayed);
        }

        public Planet GetPlanetBy(Predicate<Planet> predicate)
        {
            Planet result = null;

            foreach (Planet planet in _planets)
            {
                if (predicate.Invoke(planet))
                {
                    result = planet;
                }
            }

            return result;
        }

        public IReadOnlyCollection<Planet> GetPlanetsBy(Predicate<Planet> predicate)
        {
            List<Planet> result = new List<Planet>();

            foreach (Planet planet in _planets)
            {
                if (predicate.Invoke(planet))
                {
                    result.Add(planet);
                }
            }

            return result.AsReadOnly();
        }
    }
}
