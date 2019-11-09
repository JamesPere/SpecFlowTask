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
	Then I expect to see a day other than a weekend
	And I expect the loan summary to be correct for a loan of <loanAmount> and <Instalments> instalments
	Examples: 
	| loanAmount | loanText | RepaymentType | Instalments | PaymentDay | ExpectedPaymentDay |
	| 200        | £200     | MONTHLY       | 3           | Saturday   | Friday             |
	| 310        | £310     | MONTHLY       | 4           | Sunday     | Friday             |
	| 420        | £420     | MONTHLY       | 5           | Monday     | Monday             |
	| 530        | £530     | MONTHLY       | 6           | Tuesday    | Tuesday            |
	| 1000       | £1000    | MONTHLY       | 7           | Wednesday  | Wednesday          |
	| 670        | £670     | MONTHLY       | 8           | Thursday   | Thursday           |
	| 890        | £890     | MONTHLY       | 9           | Friday     | Friday             |
	#| 270        | £270     | MONTHLY       | 10          | Tuesday    |
	#| 280        | £280     | MONTHLY       | 11          | Tuesday    |
	#| 290        | £290     | MONTHLY       | 12          | Tuesday    |
	#| 300        | £300     | MONTHLY       | 4           | Tuesday    |
	#| 310        | £310     | MONTHLY       | 4           | Tuesday    |
	#| 500        | £500     | MONTHLY       | 2           | Sunday     |
	#| 490        | £490     | MONTHLY       | 5           | Wednesday  |
	#| 510        | £510     | MONTHLY       | 6           | Thursday   |
	#| 520        | £520     | MONTHLY       | 7           | Friday     |
	#| 550        | £550     | MONTHLY       | 8           | Saturday   |
	#| 560        | £560     | MONTHLY       | 9           | Sunday     |
	#| 570        | £570     | MONTHLY       | 10          | Sunday     |
	#| 600        | £600     | MONTHLY       | 11          | Monday     |
	#| 700        | £700     | MONTHLY       | 12          | Tuesday    |
	#| 760        | £760     | MONTHLY       | 2           | Wednesday  |
	#| 800        | £800     | MONTHLY       | 3           | Thursday   |
	#| 820        | £820     | MONTHLY       | 4           | Friday     |
	#| 1000       | £1000    | MONTHLY       | 5           | Saturday   |
														   