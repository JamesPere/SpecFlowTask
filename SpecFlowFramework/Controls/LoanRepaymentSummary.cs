using System;
using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlowFramework.Support.Extensions;
using SpecFlowFramework.Support.Models;

namespace SpecFlowFramework.Controls
{
    public class LoanRepaymentSummary
    {

        private Locator LoanSummaryValues => WebLocator.Class("loan-summary__column");
        private Locator LoanSummaryAmount => WebLocator.Class("loan-summary__column__amount");

        private Locator LoanSummaryAmountLabel => WebLocator.Class("loan-summary__column__amount__label");

        private Locator LoanSummaryOptionType => WebLocator.Class("loan-schedule__tab__panel__header__button__tag");

        private Browser _browser;

        public LoanRepaymentSummary(Browser browser)
        {
            _browser = browser;
        }

        public string GetLoanAmount()
        {
            return GetLoanSummaryValue("Loan");
        }

        public string GetInterestAmount()
        {
            return GetLoanSummaryValue("Interest");
        }

        public string GetTotalAmount()
        {
            return GetLoanSummaryValue("Total");
        }

        public string GetPerMonthAmount()
        {
            return GetLoanSummaryValue("Per month");
        }

        public void VerifyLoanSummaryOptionType(string optionType)
        {
            Assert.That(_browser.Get(LoanSummaryOptionType).Text(), Is.EqualTo(optionType));
        }

        private string GetLoanSummaryValue(string option)
        {
            var amount = _browser.GetMultiple(LoanSummaryValues)
                .First(lsv => lsv.Get(LoanSummaryAmountLabel).Text() == option)
                .Get(LoanSummaryAmount);

            _browser.WaitFor(() => !String.IsNullOrEmpty(amount.Text()));

            return amount.Text();
        }
    }
}
