Feature: Short Term Loan
	In order to check the finance calculator is working correctly

Background: 
	Given I am using Chrome

Scenario Outline: The finance calculator shows the correct STL details
	Given I navigate to http://www.auden.co.uk/Credit/ShortTermLoan
	And I select a loan amount of <loanAmount>
	Then I expect the value of the loan to show as <loanText>
	And I expect the minimum and maximum loan boundaries to be correct
	Given I select a repayment type of <RepaymentType>
	And I select an Instalment value of <Instalments>
	Then I expect the value of the instalment to show as <Instalments>
	Given I expand the loan schedule options
	And I choose a <PaymentDay>
	Then I expect the day displayed to be the <ExpectedPaymentDay>
	And I expect the option type to show as <PaymentOptionType>
	And I expect to see a day other than a weekend
	And I expect the loan summary to be correct for a loan of <loanAmount> and <Instalments> instalments
	Examples: 
	| loanAmount | loanText | RepaymentType | Instalments | PaymentDay | ExpectedPaymentDay | PaymentOptionType |
	| 200        | £200     | MONTHLY       | 3           | Saturday   | Friday             | Fixed day         |
	| 310        | £310     | MONTHLY       | 4           | Sunday     | Friday             | Fixed day         |
	| 420        | £420     | MONTHLY       | 5           | Monday     | Monday             | Fixed day         |
	| 530        | £530     | MONTHLY       | 6           | Tuesday    | Tuesday            | Fixed day         |
	| 1000       | £1000    | MONTHLY       | 7           | Wednesday  | Wednesday          | Fixed day         |
	| 670        | £670     | MONTHLY       | 8           | Thursday   | Thursday           | Fixed day         |
	| 890        | £890     | MONTHLY       | 9           | Friday     | Friday             | Fixed day         |
