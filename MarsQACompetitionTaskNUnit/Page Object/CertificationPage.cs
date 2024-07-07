using MarsQACompetitionTaskNUnit.Utilities;
using MarsQACompetitionTaskNUnit.Utilities.JsonReader;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace MarsQACompetitionTaskNUnit.Pages
{
    public class CertificationPage : CommonDriver
    {
        public CertificationPage(IWebDriver driver) : base(driver)
        {

        }

        public CertificationPage() : base()
        {

        }

        //WebElements
        public IWebElement ProfileTab => driver.FindElement(By.XPath("//section//a[@href='/Account/Profile']"));
        public IWebElement CertificationTab => driver.FindElement(By.XPath("//a[contains(text(),'Certifications')]"));
        public IWebElement AddNewButton => driver.FindElement(By.XPath("//div[@data-tab='fourth']//table//div[@class='ui teal button '][(text()='Add New')]"));
        public IWebElement CertificateTextbox => driver.FindElement(By.Name("certificationName"));
        public IWebElement CertifiedFromTextbox => driver.FindElement(By.Name("certificationFrom"));
        public IWebElement YearDropdown => driver.FindElement(By.Name("certificationYear"));
        public IWebElement YearOption => driver.FindElement(By.XPath("//*[@value='\" + Year  + \"']"));
        public IWebElement AddButton => driver.FindElement(By.XPath("//input[@value='Add']"));
        public IWebElement CancelButton => driver.FindElement(By.XPath("//input[@value='Cancel']"));
        public IWebElement ToolTipMessage => driver.FindElement(By.XPath("//*[@class='ns-box-inner']"));
        public IWebElement LastEditPencilIcon => driver.FindElement(By.XPath("//div[@data-tab='fourth']//table/tbody[last()]//i[@class='outline write icon']"));
        public IWebElement LastDeletePencilIcon => driver.FindElement(By.XPath("//div[@data-tab='fourth']//table/tbody[last()]//i[@class='remove icon']"));
        public IWebElement UpdateButton => driver.FindElement(By.XPath("//input[@value='Update']"));
        public IList<IWebElement> CertificationRows => driver.FindElements(By.XPath("//div[@data-tab='fourth']//table/tbody"));
        //Methods

        public void NavigateToCertificationTab()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            ProfileTab.Click();

            WaitUtils.WaitToBeClickable(driver, "Xpath", "CertificationTab", 10);
            CertificationTab.Click();
        }

        public void CreateCertificationRecord(CertificationConfig certification)
        {
            WaitUtils.WaitToBeVisible(driver, "Xpath", "AddNewButton", 30);
            AddNewButton.Click();

            WaitUtils.WaitToBeVisible(driver, "Xpath", "CertificateTextbox", 10);
            if (!string.IsNullOrEmpty(certification.Certificate))
            {
                CertificateTextbox.SendKeys(certification.Certificate);
            }

            WaitUtils.WaitToBeVisible(driver, "Xpath", "CertifiedFromTextbox", 10);
            if (!string.IsNullOrEmpty(certification.From))
            {
                CertifiedFromTextbox.SendKeys(certification.From);
            }

            WaitUtils.WaitToBeClickable(driver, "Xpath", "YearDropdown", 10);
            if (!string.IsNullOrEmpty(certification.Year))
            {
                YearDropdown.Click();
                driver.FindElement(By.XPath($"//*[@value='{certification.Year}']")).Click();
            }

            WaitUtils.WaitToBeClickable(driver, "Xpath", "AddButton", 30);
            AddButton.Click();

        }

        public void SelectCertificationRecord(CertificationConfig certification)
        {
            int rowNumber = GetCertificationRow(certification);
            if (rowNumber > 0)
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
             
                var editButton = driver.FindElement(By.XPath($"//div[@data-tab='fourth']//table/tbody[{rowNumber}]//i[@class='outline write icon']"));
                editButton.Click();
            }
           
        }


        public bool IsCertificationRecordPresent(CertificationConfig certification, int rowNumber = 0)
        {
            try
            {
                if (rowNumber == 0)
                {
                    int row = GetCertificationRow(certification);
                    return row > 0;
                }

                string getCertificate = driver.FindElement(By.XPath($"//div[@data-tab='fourth']//table/tbody[{rowNumber}]/tr/td[1]")).Text;
                string getFrom = driver.FindElement(By.XPath($"//div[@data-tab='fourth']//table/tbody[{rowNumber}]/tr/td[2]")).Text;
                string getYear = driver.FindElement(By.XPath($"//div[@data-tab='fourth']//table/tbody[{rowNumber}]/tr/td[3]")).Text;

                return certification.Certificate.Equals(getCertificate) && certification.From.Equals(getFrom) && certification.Year.Equals(getYear);
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public int RowCount() => CertificationRows.Count;

        public int GetCertificationRow(CertificationConfig certification)
        {
            try
            {
                for (int i = 1; i <= RowCount(); i++)
                {
                    string getCertificate = driver.FindElement(By.XPath($"//div[@data-tab='fourth']//table/tbody[{i}]/tr/td[1]")).Text;
                    string getFrom = driver.FindElement(By.XPath($"//div[@data-tab='fourth']//table/tbody[{i}]/tr/td[2]")).Text;
                    string getYear = driver.FindElement(By.XPath($"//div[@data-tab='fourth']//table/tbody[{i}]/tr/td[3]")).Text;

                    if (certification.Certificate.Equals(getCertificate) && certification.From.Equals(getFrom) && certification.Year.Equals(getYear))
                    {
                        return i;
                    }
                }
            } 
            catch (NoSuchElementException)
            {

            }
            return 0;
        }

        
        public void EditCertificationRecord(CertificationConfig certification)
        {
            try
            {
                Console.WriteLine("Starting EditCertificationRecord...");

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(60));
                
                wait.Until(ExpectedConditions.ElementToBeClickable(CertificateTextbox));
                Console.WriteLine("Certificate textbox is visible.");
                CertificateTextbox.Clear();
                Thread.Sleep(1000);
                CertificateTextbox.SendKeys(Keys.Control + "a");
                CertificateTextbox.SendKeys(Keys.Delete);
                Console.WriteLine("Certificate textbox cleared.");
                Thread.Sleep(1000);

                
                if (!string.IsNullOrEmpty(certification.Certificate))
                {

                    CertificateTextbox.SendKeys(certification.Certificate);
                    Console.WriteLine($"Entered Certificate: {certification.Certificate}");
                }

                wait.Until(ExpectedConditions.ElementToBeClickable(CertifiedFromTextbox));
                Console.WriteLine("Certified From textbox is visible.");

                CertifiedFromTextbox.Clear();
                Thread.Sleep(1000);
                CertifiedFromTextbox.SendKeys(Keys.Control + "a");
                CertifiedFromTextbox.SendKeys(Keys.Delete);
                Console.WriteLine("Certified From textbox cleared.");
                Thread.Sleep(1000);

                if (!string.IsNullOrEmpty(certification.From))
                {
                    CertifiedFromTextbox.SendKeys(certification.From);
                    Console.WriteLine($"Entered Certified From: {certification.From}");
                }


                if (!string.IsNullOrEmpty(certification.Year))
                {
 
                    wait.Until(ExpectedConditions.ElementToBeClickable(YearDropdown));
                    YearDropdown.Click();
                    Console.WriteLine("Year dropdown clicked.");

                    var yearOption = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//*[@value='{certification.Year}']")));
                    yearOption.Click();
                    Console.WriteLine($"Selected Year: {certification.Year}");

                }
  
                wait.Until(ExpectedConditions.ElementToBeClickable(UpdateButton));
                UpdateButton.Click();
                Console.WriteLine("Update button clicked.");
                
            }

            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        
        }


        public void DeleteLastCertificationRecords()
        {

            LastDeletePencilIcon.Click();
            Thread.Sleep(3000);
        }

        public void ClearCertification()
        {
            
            if (CertificationRows != null && CertificationRows.Count > 0)
            {
                                       
                int rowCount = CertificationRows.Count;

                for (int i = 1; i <= rowCount; i++)
                {
                    DeleteLastCertificationRecords();
                    Thread.Sleep(1000);
                }
            }
            
        }

    }
}
