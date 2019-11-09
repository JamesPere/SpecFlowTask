# SpecFlowTask

Tech choices:

* SpecFlow - BDD framework which allows collaboration with stakeholders by using natural language syntax
* Selenium Webdriver - Cross browser UI testing framework with a large user base
* Polly	- Retry Policies and circuit breakers, it helps overcome some of the flakyness of Selenium by adding retry on exception (useful for the ElementClickInterceptedException to prevent 'Other element would receive click') 


Patterns used:

* Page Object pattern - different screens and controls are represented as individual classes.  The intention is that the code and selectors only need to be written once and will not need to be duplicated across different tests


Other notes:

I tried to abstract away the usage of Selenium in case in the future, I decide to swap out Selenium for another UI framework.  I have tried to create a layer between both the Webdriver and WebElement interfaces that serves as a buffer between the implementation details of Selenium.

I also created a driver factory to make it easier to swap out different browser configurations.