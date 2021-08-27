using OpenQA.Selenium;

namespace UnitTest1.UI
{
    public class Planet
    {
        private IWebElement _element, _exploreButton;
        private string _name;
        private long _distance;
        private double _radius;

        internal Planet(IWebElement element, string name, long distance, double radius)
        {
            _element = element;
            _exploreButton = _element.FindElement(By.TagName("button"));
            _name = name;
            _distance = distance;
            _radius = radius;
        }

        internal IWebElement Element
        {
            get => _element;

            private set { }
        }

        public string Name
        {
            get => _name;

            private set { }
        }

        public long DistanceFromSun
        {
            get => _distance;

            private set { }
        }

        public double Radius
        {
            get => -_radius;

            private set { }
        }

        public void ClickExplore()
        {
            _exploreButton.Click();
        }
    }
}
