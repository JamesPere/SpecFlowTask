Feature: FinanceCalculator
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

Background: 
Given I am using Chrome


Scenario Outline: Add two numbers
	Given I navigate to http://www.auden.co.uk/Credit/ShortTermLoan
	And I select a loan amount of <loanAmount>
	Then I expect the value of the loan to show as <loanText>
	Given I select a repayment type of <RepaymentType>
	And I select an Instalment value of <Instalments>
	Then I expect the value of the instalment to show as <Instalments>
	Given I expand the loan schedule options
	And I choose a Saturday
	Then I expect to see a day other than a weekend
	Examples: 
	| loanAmount | loanText | RepaymentType | Instalments |
	| 500        | £500     | MONTHLY       | 2           |
	| 200        | £200     | MONTHLY       | 3           |
	| 210        | £210     | MONTHLY       | 4           |
	| 490        | £490     | MONTHLY       | 5           |
	| 510        | £510     | MONTHLY       | 6           |
	| 520        | £520     | MONTHLY       | 7           |
	| 550        | £550     | MONTHLY       | 8           |
	| 560        | £560     | MONTHLY       | 9           |
	| 570        | £570     | MONTHLY       | 10          |
	| 600        | £600     | MONTHLY       | 11          |
	| 700        | £700     | MONTHLY       | 12          |
	| 760        | £760     | MONTHLY       | 2           |
	| 800        | £800     | MONTHLY       | 3           |
	| 820        | £820     | MONTHLY       | 4           |
	| 1000       | £1000    | MONTHLY       | 5           |