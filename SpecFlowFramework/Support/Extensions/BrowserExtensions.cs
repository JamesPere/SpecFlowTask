using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SpecFlowFramework.Support.Models;

namespace SpecFlowFramework.Support.Extensions
{
    public static class BrowserExtensions
    {
        public static void GoTo(this Browser browser, string url)
        {
            browser.Driver.Navigate().GoToUrl(url);
        }

        public static void Quit(this Browser browser)
        {
            browser.Driver.Quit();
        }

        public static void CreateScreenshot(this Browser browser, string path)
        {
            ((ITakesScreenshot)browser.Driver).GetScreenshot().SaveAsFile(path, ScreenshotImageFormat.Png);
        }

        public static BrowserElement Get(this Browser browser, By by)
        {
            return new BrowserElement()
            {
                Selector = by,
                Element = GetElement(browser, by)
            };
        }

        public static List<BrowserElement> GetMultiple(this Browser browser, By by)
        {
            var elements = GetElements(browser, by);
            var browserElements = new List<BrowserElement>();

            foreach (var element in elements)
            {
                browserElements.Add(new BrowserElement()
                {
                    Selector = by,
                    Element = element,
                });
            }

            return browserElements;
        }

        private static IWebElement GetElement(Browser browser, By by)
        {
            try
            {
                return browser.Driver.FindElement(by);
            }
            catch (Exception)
            {
                return null;
            }
        }

        private static IEnumerable<IWebElement> GetElements(Browser browser, By by)
        {
            try
            {
                return browser.Driver.FindElements(by);
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void WaitFor(this Browser browser, Func<bool> condition)
        {
            var wait = new WebDriverWait(browser.Driver, TimeSpan.FromSeconds(10));
            wait.Until(x =>
            {
                try
                {
                    return condition();
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public static void WaitForPage(this Browser browser)
        {
            var wait = new WebDriverWait(browser.Driver, TimeSpan.FromSeconds(10));
            wait.Until(x =>
            {
                try
                {
                    return ((IJavaScriptExecutor) browser.Driver).ExecuteScript("return document.readyState")
                        .Equals("complete");
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public static void WaitForScripts(this Browser browser)
        {
            var wait = new WebDriverWait(browser.Driver, TimeSpan.FromSeconds(10));
            wait.Until(x =>
            {
                try
                {
                    var result =
                        ((IJavaScriptExecutor) browser.Driver).ExecuteScript(
                            "return $.active == 0");
                    return Convert.ToBoolean(result);
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }


        public static void WaitForExistance(this Browser browser, By by)
        {
            var wait = new WebDriverWait(browser.Driver, TimeSpan.FromSeconds(10));
            wait.Until(x =>
            {
                try
                {
                    return browser.Get(by) != null;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public static void WaitForEnabled(this Browser browser, By by)
        {
            var wait = new WebDriverWait(browser.Driver, TimeSpan.FromSeconds(10));
            wait.Until(x =>
            {
                try
                {
                    return browser.Get(by).Element.Enabled;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }


        public static void MoveToAndClick(this Browser browser, By by, int xOffset = 0, int yOffset = 0)
        {
            var browserElement = browser.Get(by);
            var actions = new Actions(browser.Driver);
            actions.MoveToElement(browserElement.Element, xOffset, yOffset).Click().Build().Perform();
        }

        

    }
}
