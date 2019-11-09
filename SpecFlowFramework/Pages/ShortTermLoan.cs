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

        private By LoanCalculator => By.ClassName("loan-calculator");
        
        private readonly LoanAmountControls _loanAmountControls;
        private readonly InstalmentsControls _instalmentsControls;
        

        public ShortTermLoan(Browser browser)
        {
            _browser = browser;
            _loanAmountControls = new LoanAmountControls(_browser);
            _instalmentsControls = new InstalmentsControls(_browser);
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
