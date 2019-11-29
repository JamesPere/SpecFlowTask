using System;
using System.Data;
using System.Drawing;
using System.Linq;
using OpenQA.Selenium;
using Polly;
using SpecFlowFramework.Support.Models;

namespace SpecFlowFramework.Support.Extensions
{
    public static class BrowserElementExtensions {
        public static void Type(this BrowserElement element, string text)
        {
            element.Element.SendKeys(text);
        }

        public static void Click(this BrowserElement element)
        {
            var policy = Policy.Handle<ElementClickInterceptedException>().WaitAndRetry(5, x => TimeSpan.FromSeconds(1));
            policy.Execute(() =>  element.Element.Click());

        }

        public static BrowserElement Get(this BrowserElement element, Locator selector)
        {
            try
            {
                var childElement = element.Element.FindElement(selector.ElementLocator);
                return new BrowserElement()
                {
                    Element = childElement,
                    Selector = selector.ElementLocator
                };
            }
            catch (Exception)
            {
                return null;
            }

        }

        public static BrowserElement GetChildWithText(this BrowserElement element, string text)
        {
            try
            {
                var bySelector = By.XPath($"//*[text()='{text}']");
                return element.Element.FindElements(bySelector).Select(el => new BrowserElement()
                {
                    Element = el,
                    Selector = bySelector
                }).First();
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public static string Text(this BrowserElement element)
        {
            return element.Element.Text;
        }

        public static Point Location(this BrowserElement element)
        {
            return element.Element.Location;
        }

        public static string Value(this BrowserElement element)
        {
            return element.Element.GetAttribute("value");
        }

        public static string Attribute(this BrowserElement element, string attribute)
        {
            return element.Element.GetAttribute(attribute);
        }

        public static Size Size(this BrowserElement element)
        {
            return element.Element.Size;
        }

        public static bool HasClassWithValue(this BrowserElement element, string className)
        {
            return element.Element.GetAttribute("class").Contains(className);
        }


    }
}
