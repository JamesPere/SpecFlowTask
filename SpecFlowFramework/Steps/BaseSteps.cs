using System;
using BoDi;
using SpecFlowFramework.Pages;
using SpecFlowFramework.Setup;
using SpecFlowFramework.Support;
using SpecFlowFramework.Support.Extensions;
using SpecFlowFramework.Support.Models;
using TechTalk.SpecFlow;

namespace SpecFlowFramework.Steps
{
    [Binding]
    public class BaseSteps
    {
        private IObjectContainer _objectContainer;
        private DriverFactory _driverFactory;
        private ScenarioContext _scenarioContext;
        private FeatureContext _featureContext;

        public BaseSteps(IObjectContainer objectContainer, ScenarioContext scenarioContext, FeatureContext featureContext)
        {
            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;
            _featureContext = featureContext;
            _driverFactory = new DriverFactory();
        }

        [Given(@"I am using (.*)")]
        public void GivenIAmUsing(string browser)
        {
            _objectContainer.RegisterInstanceAs<Browser>(_driverFactory.GetBrowser(browser));
            _objectContainer.RegisterInstanceAs<ShortTermLoan>(new ShortTermLoan(_objectContainer.Resolve<Browser>()));
        }

        [Given(@"I navigate to (.*)")]
        public void GivenINavigate(string url)
        {
            _objectContainer.Resolve<Browser>().GoTo(url);
        }

       

        [BeforeScenario()]
        public void BeforeScenario()
        {
            Console.WriteLine("Starting Scenario");
        }



        [AfterScenario()]
        public void AfterScenario()
        {
            if(_scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError)
            {
                var filePath = GenerateScreenshotPath();
                _objectContainer.Resolve<Browser>().CreateScreenshot(filePath);
                Console.Write($"Screenshot generated: {filePath}");
            }

            Console.WriteLine($"Scenario Finished");
            _objectContainer.Resolve<Browser>().Quit();
            _objectContainer.Dispose();
            
        }

        

        private string GenerateScreenshotPath()
        {
            var time = DateTime.Now.ToString("dd_MM_yyyy_hh_mm_ss");
            var scenario = _scenarioContext.ScenarioInfo.Title;
            var browser = _objectContainer.Resolve<Browser>().Name;
            var directory = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, System.AppDomain.CurrentDomain.RelativeSearchPath ?? "");
            
            return $@"{directory}\{time}_{scenario}_{browser}.png";
        }
    }
}
