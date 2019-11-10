using System;
using BoDi;
using SpecFlowFramework.Pages;
using SpecFlowFramework.Setup;
using SpecFlowFramework.Support;
using TechTalk.SpecFlow;

namespace SpecFlowFramework.Steps
{
    [Binding]
    public class ShortTermLoanSteps
    {
        private IObjectContainer _objectContainer;

        public ShortTermLoanSteps(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [Given(@"I select a loan amount of (.*)")]
        public void GivenISelectALoanAmountOf(string loanValue)
        {
            _objectContainer.Resolve<ShortTermLoan>().SelectLoanValue(loanValue);
        }

        [Then(@"I expect the value of the loan to show as (.*)")]
        public void ThenIExpectTheValueOfTheLoanToShowAs(string loanValue)
        {
            _objectContainer.Resolve<ShortTermLoan>().VerifyLoanHeader(loanValue);
        }

        [Then(@"I expect the value of the instalment to show as (.*)")]
        public void ThenIExpectTheValueOfTheInstalmentToShowAs(string instalmentValue)
        {
            _objectContainer.Resolve<ShortTermLoan>().VerifyInstalmentValue(instalmentValue);
        }

        [Given(@"I select a repayment type of (.*)")]
        public void GivenISelectARepaymentTypeOf(string repaymentType)
        {
            _objectContainer.Resolve<ShortTermLoan>().SelectRepaymentType(repaymentType);
        }

        [Given(@"I select an Instalment value of (.*)")]
        public void GivenISelectAInstalmentValueOf(string instalment)
        {
            _objectContainer.Resolve<ShortTermLoan>().SelectInstalmentValue(instalment);
        }

        [Given(@"I expand the loan schedule options")]
        public void GivenIExpandTheLoanScheduleOptions()
        {
            _objectContainer.Resolve<ShortTermLoan>().ExpandRepaymentDate();
        }

        [Given(@"I choose a (.*)")]
        public void GivenIChooseASunday(string day)
        {
            _objectContainer.Resolve<ShortTermLoan>().SelectCalendarDay(day);
        }

        [Then(@"I expect to see a day other than a weekend")]
        public void ThenIExpectToSeeADayOtherThanAWeekend()
        {
            _objectContainer.Resolve<ShortTermLoan>().VerifyScheduleNotOnWeekend();
        }

        [Then(@"I expect to see the correct date for (.*)")]
        public void ThenIExpectToSeeTheCorrectDate(string paymentDay)
        {
            _objectContainer.Resolve<ShortTermLoan>().VerifyPaymentDate(paymentDay);
        }

        [Then(@"I expect the loan summary to be correct for a loan of (.*) and (.*) instalments")]
        public void ThenIExpectTheLoanSummaryToBeCorrectForAnd(int loanAmount, int instalments)
        {
            _objectContainer.Resolve<ShortTermLoan>().VerifyLoanRepaymentSummary(loanAmount, instalments);
        }

        [Then(@"I expect the minimum and maximum loan boundaries to be correct")]
        public void ThenIExpectTheMinimumAndMaximumLoanBoundariesToBeCorrect()
        {
            _objectContainer.Resolve<ShortTermLoan>().VerifyMinimumAndMaxThresholds();
        }

        [Then(@"I expect the day displayed to be the (.*)")]
        public void ThenIExpectTheDayDisplayedToBeTheFriday(string expectedDay)
        {
            _objectContainer.Resolve<ShortTermLoan>().VerifyExpectedDay(expectedDay);
        }

        [Then(@"I expect the option type to show as (.*)")]
        public void ThenIExpectTheOptionTypeToShowAs(string optionType)
        {
            _objectContainer.Resolve<ShortTermLoan>().VerifyLoanSummaryOptionType(optionType);
        }

    }
}
