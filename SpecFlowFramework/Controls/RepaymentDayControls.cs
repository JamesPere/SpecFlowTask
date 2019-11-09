using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlowFramework.Support.Enum;
using SpecFlowFramework.Support.Extensions;
using SpecFlowFramework.Support.Models;
using SpecFlowFramework.Support.Utils;

namespace SpecFlowFramework.Controls
{
    public class RepaymentDayControls
    {

        private By LoanRepaymentText => By.ClassName("loan-schedule__tab__panel__detail__tag__text");

        private Browser _browser;

        public RepaymentDayControls(Browser browser)
        {
            _browser = browser;
        }

        public void VerifyPaymentDate(string day)
        {
            Enum.TryParse(day, out CalendarEnum calendarDay);
            var currentDate = DateTime.Now.AddMonths(1);
            var desiredDate =
                CalendarUtil.GetNextDay(calendarDay, new DateTime(currentDate.Year, currentDate.Month, 1));

            var expectedRepaymentDate = LoanEngineUtil.GetExpectedRepaymentDate(desiredDate);

            var actualRepaymentDate = _browser.Get(LoanRepaymentText).Text();

            Assert.That(actualRepaymentDate, Is.EqualTo(expectedRepaymentDate.ToString("dddd d MMM yyyy")));

        }
    }
}
