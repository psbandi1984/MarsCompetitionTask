using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Reflection;

namespace MarsQACompetitionTaskNUnit.Utilities
{
    public class CommonDriver
    {
        public IWebDriver driver;
        public CommonDriver()
        {
            
        }
        public CommonDriver(IWebDriver driver)
        {
            this.driver = driver;

        }

        public IWebDriver Initialize()
        {
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();

            return driver;
        }

        public IWebDriver getDriver()
        {
            return driver;
        }


        public string TakeScreenshot()
        {
            var file = ((ITakesScreenshot)driver).GetScreenshot();
            var image = file.AsBase64EncodedString;

            return image;

        }

        public string SaveScreenshot()
        {
            var fileName = Guid.NewGuid().ToString();
            var directory = Directory.CreateDirectory(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)
                 + "\\screenshots\\").FullName;
            string filePath = directory + fileName;

            var file = ((ITakesScreenshot)driver).GetScreenshot();
            file.SaveAsFile(filePath);

            return filePath;
        }
        
    }
}
