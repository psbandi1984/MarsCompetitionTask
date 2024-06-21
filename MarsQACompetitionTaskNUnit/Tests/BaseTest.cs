using MarsQACompetitionTaskNUnit.Pages;
using MarsQACompetitionTaskNUnit.Utilities;
using MarsQACompetitionTaskNUnit.Utilities.JsonReader;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;

namespace MarsQACompetitionTaskNUnit.Tests
{
    public class BaseTest
    {

        protected IWebDriver driver;
        protected LoginPage loginPageObject;
        protected EducationPage educationPageObject;
        protected CertificationPage certificationPageObject;
        protected List<LoginConfig> loginConfig;
        protected List<EducationConfig> educationConfig;
        protected List<CertificationConfig> certificationConfig;

        // Called once prior to executing any of the tests in a fixture
        [OneTimeSetUp]
        public void BaseFixtureSetup()
        {
            
            AppConfig config = AppConfig.LoadConfig();
            CommonDriver driverSetup = new CommonDriver();
            driver = driverSetup.Initialize();
            driver.Navigate().GoToUrl(config.url);

            loginPageObject = new LoginPage(driver);
            loginConfig = LoginConfig.LoadConfig();
            educationPageObject = new EducationPage(driver);
            educationConfig = EducationConfig.LoadConfig();
            certificationPageObject = new CertificationPage(driver);
            certificationConfig = CertificationConfig.LoadConfig();

            loginPageObject.ClickSignIn();
            Thread.Sleep(1000);
            loginPageObject.ValidLoginSteps(loginConfig[0].EmailAddress, loginConfig[0].Password);

        }

        public IWebDriver GetDriver()
        {
            return driver;
        }

        // Called once after executing any of the tests in a fixture
        [OneTimeTearDown]
        public void BaseFixtureTeardown()
        {
            ExtentManager.GetExtent().Flush();
            driver?.Dispose();
        }

        // Called before each test method in the derived class
        [SetUp]
        public void BaseSetup()
        {
            // Create a test for reporting
            ExtentManager.CreateTest(TestContext.CurrentContext.Test.Name);
            
        }

        // Performed after each test method in derived class
        [TearDown]
        public void BaseTearDown()
        {
            try
            {
                // Log test results to ExtentReports
                EndTest();                                
            }
            catch (Exception ex)
            {
                throw new Exception("Exception: " + ex);
            }
            
        }

        public void EndTest()
        {
            var teststatus = TestContext.CurrentContext.Result.Outcome.Status;
            var message = TestContext.CurrentContext.Result.Message;

            switch (teststatus)
            {
                case TestStatus.Failed:
                    ReportLogger.LogFail($"Test has failed {message}");
                    break;

                case TestStatus.Skipped:
                    ReportLogger.LogSkip($"Test skipped {message}");
                    break;

                case TestStatus.Passed:
                    ReportLogger.LogPass($"Test passed {message}");
                    break;

                default:
                    break;
            }

            ExtentManager.LogScreenshot("Ending test", loginPageObject.TakeScreenshot());
            ExtentManager.LogScreenshot("Ending test", educationPageObject.TakeScreenshot());
            ExtentManager.LogScreenshot("Ending test", certificationPageObject.TakeScreenshot());
        }
    }
}
