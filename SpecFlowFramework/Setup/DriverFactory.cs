using System.Diagnostics;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using SpecFlowFramework.Support.Models;

namespace SpecFlowFramework.Setup
{
    public class DriverFactory
    {
        public Browser GetBrowser(string browser)
        {
            switch (browser)
            {
                case "Firefox":
                    return GetFirefoxBrowser();
                case "IE":
                    return GetIeBrowser();
                case "Edge":
                    return GetEdgeBrowser();
                case "Chrome":
                default:
                    return GetChromeBrowser(); 
            }
        }

        public Browser GetFirefoxBrowser()
        {
            return new Browser()
            {
                Driver = new FirefoxDriver(),
                Name = "Firefox",
                Viewport = "Desktop",
            };
        }
        public Browser GetChromeBrowser()
        {
            return new Browser()
            {
                Driver = new ChromeDriver(),
                Name = "Chrome",
                Viewport = "Desktop",
            };
        }

        public Browser GetIeBrowser()
        {
            return new Browser()
            {
                Driver = new InternetExplorerDriver(),
                Name = "IE",
                Viewport = "Desktop",
            };
        }

        public Browser GetEdgeBrowser()
        {
            return new Browser()
            {
                Driver = new EdgeDriver(),
                Name = "Edge",
                Viewport = "Desktop",
            };
        }
    }

    
}
