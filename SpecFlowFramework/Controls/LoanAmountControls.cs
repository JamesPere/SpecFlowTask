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
            
            var offset = Convert.ToInt32(GetApproximateLoanValueSliderOffset(loanValue));
            _browser.MoveToAndClick(LoanSlider, offset);
            AdjustSliderPosition(loanValue, offset);
        }

        private void AdjustSliderPosition(string loanValue, int offset)
        {
            var loanAmount = Convert.ToInt32(GetLoanAmount().Replace("£", ""));
            var loanValueAmount = Convert.ToInt32(loanValue);

            if (loanAmount == loanValueAmount) return;
            int maxNumberOfAdjustments = 8;
            int numberOfPixelsToChangeBy = 3;
            int currentNumberOfAdjustments = 0;
            var newOffset = offset;

            if (loanAmount > loanValueAmount)
            {
                while (loanAmount != loanValueAmount && currentNumberOfAdjustments < maxNumberOfAdjustments)
                {
                    newOffset = newOffset - numberOfPixelsToChangeBy;
                    _browser.MoveToAndClick(LoanSlider, newOffset);
                    loanAmount = Convert.ToInt32(GetLoanAmount().Replace("£", ""));
                    loanValueAmount = Convert.ToInt32(loanValue);
                    currentNumberOfAdjustments++;
                }
            }
            else
            {
                while (loanAmount != loanValueAmount && currentNumberOfAdjustments < maxNumberOfAdjustments)
                {
                    newOffset = newOffset + numberOfPixelsToChangeBy;
                    _browser.MoveToAndClick(LoanSlider, newOffset);
                    loanAmount = Convert.ToInt32(GetLoanAmount().Replace("£", ""));
                    loanValueAmount = Convert.ToInt32(loanValue);
                    currentNumberOfAdjustments++;
                }
            }
        }

        public string GetLoanAmount()
        {
            return _browser.Get(LoanHeaderAmount).Text();
        }

        private decimal GetApproximateLoanValueSliderOffset(string loanValue)
        {
            var loanFigure = Convert.ToInt32(loanValue);
            var lowerValue = GetLowerFinanceRange();
            var upperValue = GetUpperFinanceRange();
            var startingPointOffset = 25; //slider increments only happen after first 25px
            var sliderClickOffset = 0;
            var loanIncrements = Convert.ToInt32(_browser.Get(LoanSlider).Attribute("step"));

            if (loanFigure <= lowerValue) return sliderClickOffset;

            var sliderWidth = _browser.Get(LoanSlider).Size().Width;

            if (loanFigure == upperValue) return sliderWidth - startingPointOffset;

            var numberOfIncrements = (upperValue - lowerValue) / loanIncrements; //the number of choices
            int calculatedWidth = (sliderWidth + startingPointOffset) / numberOfIncrements; //the width of each choice

            var numberOfIncrementsToMake = (loanFigure - lowerValue) / loanIncrements;

            decimal total = numberOfIncrementsToMake > 1 ? ((calculatedWidth * numberOfIncrementsToMake) + startingPointOffset)
                : startingPointOffset;

            return total;

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
