using System;
using System.Data;
using System.Drawing;
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
            var policy = Policy.Handle<ElementClickInterceptedException>().WaitAndRetry(3, x => TimeSpan.FromSeconds(2));
            policy.Execute(() =>  element.Element.Click());

        }

        public static string Text(this BrowserElement element)
        {
            return element.Element.Text;
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
