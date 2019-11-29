using OpenQA.Selenium;

namespace SpecFlowFramework.Support.Models
{
    public static class WebLocator
    {
        public static Locator CssSelector(string locator)
        {
            return new Locator
            {
                ElementLocator = By.CssSelector(locator)
            };
        }

        public static Locator Id(string locator)
        {
            return new Locator
            {
                ElementLocator = By.Id(locator)
            };
        }

        public static Locator Class(string locator)
        {
            return new Locator
            {
                ElementLocator = By.CssSelector(locator)
            };
        }
    }
}
