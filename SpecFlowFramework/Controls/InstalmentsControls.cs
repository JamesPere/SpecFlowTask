using System;
using System.Linq;
using OpenQA.Selenium;
using SpecFlowFramework.Support.Extensions;
using SpecFlowFramework.Support.Models;

namespace SpecFlowFramework.Controls
{
    public class InstalmentsControls
    {
        public By RepaymentTypeTabs => By.Id("tab");
        private By MonthlyLoanDurationSlider => By.Id("monthly");

        private By LoanScheduleRange => By.CssSelector(".loan-schedule__tab__header__range span");

        private readonly By LoanInstalmentAmount = By.CssSelector(".loan-schedule__tab__header__amount span");

        private By RepaymentDayIcon => By.ClassName("loan-schedule__tab__panel__header__button__icon");

        private By LoanScheduleText => By.ClassName("loan-schedule__tab__panel__detail__tag__text");

        private readonly Browser _browser;

        private readonly RepaymentCalendarControls _repaymentCalendar;

        public InstalmentsControls(Browser browser)
        {
            _browser = browser;
            _repaymentCalendar = new RepaymentCalendarControls(_browser);
        }

        public void SelectRepaymentType(string repaymentType)
        {
            _browser.WaitForEnabled(MonthlyLoanDurationSlider);
            _browser.WaitFor(() => _browser.Get(MonthlyLoanDurationSlider).HasClassWithValue("disable-pointer-events") == false);
            var repaymentTabs = _browser.GetMultiple(RepaymentTypeTabs);
            repaymentTabs.First(x => x.Value().Contains(repaymentType.ToLower())).Click();
        }

        public void SelectInstalmentValue(string instalmentValue)
        {
            _browser.WaitForEnabled(MonthlyLoanDurationSlider);
            _browser.WaitFor(() => _browser.Get(MonthlyLoanDurationSlider).HasClassWithValue("disable-pointer-events") == false);

            var offset = GetDesiredInstalmentsValueSliderOffset(instalmentValue);
            _browser.MoveToAndClick(MonthlyLoanDurationSlider, offset);
        }

        public void ExpandRepaymentDate()
        {
            _browser.Get(RepaymentDayIcon).Click();
            _browser.WaitForExistance(_repaymentCalendar.Calendar);
        }

        public void SelectCalendarDay(string day)
        {
            _repaymentCalendar.SelectCalendarDay(day);
        }

        public string GetInstalmentAmount()
        {
            return _browser.Get(LoanInstalmentAmount).Text();
        }

        public string GetScheduleText()
        {
            return _browser.Get(LoanScheduleText).Text();
        }


        private int GetDesiredInstalmentsValueSliderOffset(string instalments)
        {

            var installmentFigure = Convert.ToInt32(instalments);
            var lowerValue = GetLowerInstalmentRange();
            var upperValue = GetUpperInstalmentRange();
            var sliderClickOffset = 0;
            var installmentIncrements = Convert.ToInt32(_browser.Get(MonthlyLoanDurationSlider).Attribute("step"));

            if (installmentFigure < lowerValue)
            {
                throw new Exception("Instalment figure cannot be less than range");
            }

            if (installmentFigure == lowerValue)
            {
                return 0;
            }


            if (installmentFigure > lowerValue)
            {
                var sliderWidth = _browser.Get(MonthlyLoanDurationSlider).Size().Width;
                var numberOfIncrements = (upperValue - lowerValue) / installmentIncrements; //the number of choices
                var calculatedWidth = sliderWidth / numberOfIncrements; //the width of each choice

                var numberOfIncrementsToMake = (installmentFigure - lowerValue) / installmentIncrements;

                var total = (calculatedWidth * numberOfIncrementsToMake);

                return total;
            }

            return sliderClickOffset;
        }


        private int GetLowerInstalmentRange()
        {
            var range = _browser.Get(LoanScheduleRange).Text();
            var splitCharPosition = range.IndexOf("-", StringComparison.Ordinal);
            var lowerRange = range.Substring(0, splitCharPosition);
            return Convert.ToInt32(lowerRange.Trim());
        }

        private int GetUpperInstalmentRange()
        {
            var range = _browser.Get(LoanScheduleRange).Text();
            var splitCharPosition = range.IndexOf("-", StringComparison.Ordinal);
            var upperRange = range.Substring(splitCharPosition + 1, (range.Length - (splitCharPosition + 1)));
            return Convert.ToInt32(upperRange.Trim());

        }



    }
}
