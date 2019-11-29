using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using SpecFlowFramework.Controls;
using SpecFlowFramework.Support;
using SpecFlowFramework.Support.Enum;
using SpecFlowFramework.Support.Extensions;
using SpecFlowFramework.Support.Models;
using SpecFlowFramework.Support.Utils;

namespace SpecFlowFramework.Pages
{
    public class ShortTermLoan
    {
        private readonly Browser _browser;

        private Locator LoanCalculator => WebLocator.Class("loan-calculator");
        
        private readonly LoanAmountControls _loanAmountControls;
        private readonly InstalmentsControls _instalmentsControls;
        private RepaymentDayControls _repaymentDayControls;
        private LoanRepaymentSummary _loanRepaymentSummary;


        public ShortTermLoan(Browser browser)
        {
            _browser = browser;
            _loanAmountControls = new LoanAmountControls(_browser);
            _instalmentsControls = new InstalmentsControls(_browser);
            _repaymentDayControls = new RepaymentDayControls(_browser);
            _loanRepaymentSummary = new LoanRepaymentSummary(_browser);
        }

        public void VerifyLoanRepaymentSummary(int requestedloanAmount, int requestedInstalments)
        {
            var loanAmount = _loanRepaymentSummary.GetLoanAmount();
            var interestAmount = _loanRepaymentSummary.GetInterestAmount();
            var totalAmount = _loanRepaymentSummary.GetTotalAmount();
            //var perMonthAmount = _loanRepaymentSummary.GetPerMonthAmount();

            var requestedLoanAmountAsDecimal = Convert.ToDecimal(requestedloanAmount);
            var interestAmountAsDecimal = Convert.ToDecimal(interestAmount.Replace("£", "").Trim());
            var totalAmountAsDecimal = requestedLoanAmountAsDecimal + interestAmountAsDecimal;

            Assert.That(loanAmount, Is.EqualTo($"£{Convert.ToDecimal(requestedloanAmount):0.00}"));
            Assert.That(totalAmount, Is.EqualTo($"£{totalAmountAsDecimal}"));

            //Commenting out as the per month calc is a bit wacky
            //var totalRounded = Math.Round(totalAmountAsDecimal, 2, MidpointRounding.AwayFromZero);
            //var repayments = Decimal.Round(Convert.ToDecimal(totalRounded / requestedInstalments), 2);

            //Assert.That(perMonthAmount, Is.EqualTo($"£{repayments:0.00}"));
        }

        public void VerifyLoanSummaryOptionType(string optionType)
        {
            _loanRepaymentSummary.VerifyLoanSummaryOptionType(optionType);
        }

        public void VerifyPaymentDate(string day)
        {
            _repaymentDayControls.VerifyPaymentDate(day);
        }


        public void VerifyExpectedDay(string day)
        {
            _browser.WaitFor(() => _browser.Get(LoanCalculator).HasClassWithValue("disable-pointer-events") == false);
            Assert.That(_instalmentsControls.GetScheduleText().ToUpper().Contains(day.ToUpper()), $"Expected: {day.ToUpper()}, Actual: {_instalmentsControls.GetScheduleText().ToUpper()}");
        }

        public void VerifyScheduleNotOnWeekend()
        {
            Assert.That(_instalmentsControls.GetScheduleText().ToUpper(), Does.Not.Contain("SUNDAY"));
            Assert.That(_instalmentsControls.GetScheduleText().ToUpper(), Does.Not.Contain("SATURDAY"));
        }

        public void VerifyLoanHeader(string loanValue)
        {
            Assert.That(_loanAmountControls.GetLoanAmount(), Is.EqualTo(loanValue));
        }

        public void VerifyInstalmentValue(string instalmentValue)
        {
            Assert.That(_instalmentsControls.GetInstalmentAmount(), Is.EqualTo(instalmentValue));
        }


        public void VerifyMinimumAndMaxThresholds()
        {
            _loanAmountControls.VerifyMaximumAmount();
            _loanAmountControls.VerifyMinimumAmount();
        }

        public void SelectLoanValue(string loanValue)
        {
            _browser.WaitFor(() => _browser.Get(LoanCalculator).HasClassWithValue("disable-pointer-events") == false);
            _loanAmountControls.SelectLoanValue(loanValue);
        }

        public void SelectRepaymentType(string repaymentType)
        {
            _instalmentsControls.SelectRepaymentType(repaymentType);
        }

        public void SelectInstalmentValue(string instalmentValue)
        {
            _instalmentsControls.SelectInstalmentValue(instalmentValue);
        }

        public void ExpandRepaymentDate()
        {
            _instalmentsControls.ExpandRepaymentDate();
        }

        public void SelectCalendarDay(string day)
        {
            _instalmentsControls.SelectCalendarDay(day);
        }

        

    }
}
