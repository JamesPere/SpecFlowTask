using OpenQA.Selenium;

namespace SpecFlowFramework.Support.Models
{
    public class Browser
    {
        public IWebDriver Driver { get; set; }
        public string Name { get; set; }
        public string Viewport { get; set; }
    }
}
