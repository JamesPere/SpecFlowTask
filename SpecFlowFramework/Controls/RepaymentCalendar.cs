using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using SpecFlowFramework.Support.Enum;
using SpecFlowFramework.Support.Extensions;
using SpecFlowFramework.Support.Models;
using SpecFlowFramework.Support.Utils;

namespace SpecFlowFramework.Controls
{
    public class RepaymentCalendar
    {

        public By Calendar => By.CssSelector(".loan-schedule__tab__panel__content.active");
        public By CalendarDates => By.ClassName("date-selector__date");

        private Browser _browser;

        public RepaymentCalendar(Browser browser)
        {
            _browser = browser;
        }

        public void SelectCalendarDay(string day)
        {
            CalendarEnum calendarDay;
            Enum.TryParse(day, out calendarDay);
            var currentDate = DateTime.Now.AddMonths(1);
            var desiredDate =
                CalendarUtil.GetNextDay(calendarDay, new DateTime(currentDate.Year, currentDate.Month, 1));

            Console.WriteLine($"Desired Date: {desiredDate:dd/MM/yyyy}");

            var calendarItem = _browser.GetMultiple(CalendarDates).First(x => x.Text() == desiredDate.Day.ToString());

            calendarItem.Click();
            _browser.WaitFor(() => !String.IsNullOrEmpty(calendarItem.Attribute("disabled")));

        }
    }
}
