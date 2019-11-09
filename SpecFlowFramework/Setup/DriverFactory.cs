using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using SpecFlowFramework.Support;
using SpecFlowFramework.Support.Models;

namespace SpecFlowFramework.Setup
{
    public class DriverFactory
    {
        public Browser GetBrowser(string browser)
        {
            switch (browser)
            {
                case "firefox":
                    return GetFirefoxBrowser();
                case "ie":
                    return GetIeBrowser();
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
    }

    
}
