using MarsQACompetitionTaskNUnit.Utilities;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MarsQACompetitionTaskNUnit.Assertions
{
    public static class AssertionHelpers
    {
        //To verify ToolTip messages after creating/editing/deleting records
        public static void AssertToolTipMessage(CommonDriver page, string expectedMessage)
        {

            IWebDriver driver = page.getDriver();

            //WaitUtils.WaitToBeVisible(driver, "XPath", "//*[@class='ns-box-inner']", 10);
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(30));
            wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//*[@class='ns-box-inner']")));

            IWebElement toolTipMessage = driver.FindElement(By.XPath("//*[@class='ns-box-inner']"));

            string actualMessage = toolTipMessage.Text.Trim();

            Console.WriteLine("Tooltip Text: " + actualMessage);

            // Remove single quotes from the expected message
            string expectedMessageWithoutQuotes = expectedMessage.Replace("'", "");

            // Check if the actual message matches the expected message without quotes
            if (actualMessage == expectedMessageWithoutQuotes)
            {
                // Log the success
                Console.WriteLine("Tooltip message matches the expected message.");
                ReportLogger.LogPass("Passed " + expectedMessage);
            }
            else
            {
                // Log the failure and provide details about the differences
                ReportLogger.LogFail("Failed " + expectedMessage);
                Console.WriteLine($"Expected: '{expectedMessage}'");
                Console.WriteLine($"But was:  '{actualMessage}'");
                Assert.Fail("Tooltip message does not match the expected message.");
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(60);

            }
        }
       
    }
}
