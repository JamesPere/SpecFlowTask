using System;
using OpenQA.Selenium;
using SpecFlowFramework.Support.Extensions;
using SpecFlowFramework.Support.Models;

namespace SpecFlowFramework.Controls
{
    public class LoanAmountControls
    {

        private readonly By LoanAmountRange = By.CssSelector(".loan-amount__header__range span");
        public By LoanSlider => By.CssSelector(".loan-amount__range-slider__input");

        private By LoanHeaderAmount = By.CssSelector(".loan-amount__header__amount span");

        private Browser _browser;

        public LoanAmountControls(Browser browser)
        {
            _browser = browser;
        }

        public void SelectLoanValue(string loanValue)
        {
            _browser.WaitForEnabled(LoanSlider);
            
            var offset = GetDesiredLoanValueSliderOffset(loanValue);
            _browser.MoveToAndClick(LoanSlider, offset);

        }

        public string GetLoanAmount()
        {
            return _browser.Get(LoanHeaderAmount).Text();
        }

        private int GetDesiredLoanValueSliderOffset(string loanValue)
        {
            var loanFigure = Convert.ToInt32(loanValue);
            var lowerValue = GetLowerFinanceRange();
            var upperValue = GetUpperFinanceRange();
            var startingPointOffset = 25; //slider increments only happen after first 25px
            var sliderClickOffset = 0;
            var loanIncrements = Convert.ToInt32(_browser.Get(LoanSlider).Attribute("step"));

            if (loanFigure < lowerValue)
            {
                throw new Exception("Loan Value cannot be less than range");
            }

            if (loanFigure == lowerValue)
            {
                return 0;
            }

            if (loanFigure > lowerValue)
            {

                if (loanFigure <= (upperValue / 2))
                {
                    var sliderWidth = _browser.Get(LoanSlider).Size().Width;
                    var numberOfIncrements = (upperValue - lowerValue) / loanIncrements; //the number of choices
                    var calculatedWidth = (sliderWidth + startingPointOffset) / numberOfIncrements; //the width of each choice

                    var numberOfIncrementsToMake = (loanFigure - lowerValue) / loanIncrements;

                    var total = numberOfIncrementsToMake > 1 ? ((calculatedWidth * numberOfIncrementsToMake) + startingPointOffset) - 10
                        : startingPointOffset;

                    Console.WriteLine($"total: {total}");
                    Console.WriteLine($"calculatedWidth: {calculatedWidth}");
                    Console.WriteLine($"numberOfIncrementsToMake: {numberOfIncrementsToMake}");
                    Console.WriteLine($"startingPointOffset: {startingPointOffset}");

                    return total;
                }
                else
                {
                    var sliderWidth = _browser.Get(LoanSlider).Size().Width;
                    var numberOfIncrements = (upperValue - lowerValue) / loanIncrements; //the number of choices
                    var calculatedWidth = (sliderWidth + startingPointOffset) / numberOfIncrements; //the width of each choice

                    var numberOfIncrementsToMake = (loanFigure - lowerValue) / loanIncrements;

                    var total = numberOfIncrementsToMake > 1 ? ((calculatedWidth * numberOfIncrementsToMake) + startingPointOffset) - 10
                        : startingPointOffset;

                    total = total - 1;

                    Console.WriteLine($"total: {total}");
                    Console.WriteLine($"calculatedWidth: {calculatedWidth}");
                    Console.WriteLine($"numberOfIncrementsToMake: {numberOfIncrementsToMake}");
                    Console.WriteLine($"startingPointOffset: {startingPointOffset}");

                    return total;
                }
            }

            return sliderClickOffset;
        }

        private int GetLowerFinanceRange()
        {
            _browser.WaitForPage();
            _browser.WaitForEnabled(LoanSlider);

            var range = _browser.Get(LoanAmountRange).Text();
            var splitCharPosition = range.IndexOf("-", StringComparison.Ordinal);
            var lowerRange = range.Substring(0, splitCharPosition);
            return Convert.ToInt32(lowerRange.Replace("£", "").Trim());
        }

        private int GetUpperFinanceRange()
        {
            _browser.WaitForPage();
            _browser.WaitForEnabled(LoanSlider);
            var range = _browser.Get(LoanAmountRange).Text();
            var splitCharPosition = range.IndexOf("-", StringComparison.Ordinal);
            var upperRange = range.Substring(splitCharPosition + 1, (range.Length - (splitCharPosition + 1)));
            return Convert.ToInt32(upperRange.Replace("£", "").Trim());

        }

        

    }
}
