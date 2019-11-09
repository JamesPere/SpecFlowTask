using OpenQA.Selenium;

namespace SpecFlowFramework.Support.Models
{
    public class BrowserElement
    {
        public By Selector { get; set; }
        public IWebElement Element { get; set; }
    }
}
