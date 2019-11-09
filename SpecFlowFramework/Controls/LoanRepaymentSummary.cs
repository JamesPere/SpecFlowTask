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
    public class LoanRepaymentSummary
    {

        private By LoanSummaryValues => By.ClassName("loan-summary__column");
        private By LoanSummaryAmount => By.ClassName("loan-summary__column__amount");

        private By LoanSummaryAmountLabel = By.ClassName("loan-summary__column__amount__label");

        private Browser _browser;

        public LoanRepaymentSummary(Browser browser)
        {
            _browser = browser;
        }

        public string GetLoanAmount()
        {
            var amount = _browser.GetMultiple(LoanSummaryValues)
                .First(lsv => lsv.Get(LoanSummaryAmountLabel).Text() == "Loan")
                .Get(LoanSummaryAmount);

            _browser.WaitFor(() => !String.IsNullOrEmpty(amount.Text()));

            return amount.Text();
        }

        public string GetInterestAmount()
        {
            var amount = _browser.GetMultiple(LoanSummaryValues)
                .First(lsv => lsv.Get(LoanSummaryAmountLabel).Text() == "Interest")
                .Get(LoanSummaryAmount);

            _browser.WaitFor(() => !String.IsNullOrEmpty(amount.Text()));

            return amount.Text();
        }

        public string GetTotalAmount()
        {
            var amount = _browser.GetMultiple(LoanSummaryValues)
                .First(lsv => lsv.Get(LoanSummaryAmountLabel).Text() == "Total")
                .Get(LoanSummaryAmount);
                

            _browser.WaitFor(() => !String.IsNullOrEmpty(amount.Text()));

            return amount.Text();
        }

        public string GetPerMonthAmount()
        {
            var amount = _browser.GetMultiple(LoanSummaryValues)
                .First(lsv => lsv.Get(LoanSummaryAmountLabel).Text() == "Per month")
                .Get(LoanSummaryAmount);

            _browser.WaitFor(() => !String.IsNullOrEmpty(amount.Text()));

            return amount.Text();
        }
    }
}
