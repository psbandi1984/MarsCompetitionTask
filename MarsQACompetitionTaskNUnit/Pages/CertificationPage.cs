using MarsQACompetitionTaskNUnit.Utilities;
using OpenQA.Selenium;

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

            WaitUtils.WaitToBeClickable(driver, "Xpath", "CertificationTab", 20);
            CertificationTab.Click();
        }

        public void CreateCertificationRecord(string Certificate, string From, string Year)
        {
            // Click on Add New button
            WaitUtils.WaitToBeVisible(driver, "Xpath", "AddNewButton", 10);
            AddNewButton.Click();

            // Enter Certificate    
            CertificateTextbox.Click();
            CertificateTextbox.SendKeys(Certificate);

            // Enter Certified From data
            CertifiedFromTextbox.Click();
            CertifiedFromTextbox.SendKeys(From);


            // Select CertificationYear from dropdown list
            WaitUtils.WaitToBeVisible(driver, "Xpath", "YearDropdown", 5);
            YearDropdown.Click();
            driver.FindElement(By.XPath("//*[@value='" + Year + "']")).Click();


            // Click on save button
            WaitUtils.WaitToBeVisible(driver, "Xpath", "AddButton", 5);
            AddButton.Click();
            Thread.Sleep(1000);

        }

        public bool IsCertificationRecordPresent(string Certificate, string From, string Year, int rowNumber = 0)
        {
            bool recordPresent = false;
            string getCertificate, getFrom, getYear;
            int certificationRowPresent;

            if (rowNumber == 0)
            {
                certificationRowPresent = GetCertificationRow(Certificate, From, Year);
                if (certificationRowPresent > 0)
                {
                    recordPresent = true;
                }
            }
            else
            {
                WaitUtils.WaitToBeVisible(driver, "Xpath", "getCertificate", 60);
                getCertificate = driver.FindElement(By.XPath($"//div[@data-tab='fourth']//table/tbody[{rowNumber}]/tr/td[1]")).Text;
                WaitUtils.WaitToBeVisible(driver, "Xpath", "getFrom", 60);
                getFrom = driver.FindElement(By.XPath($"//div[@data-tab='fourth']//table/tbody[{rowNumber}]/tr/td[2]")).Text;
                getYear = driver.FindElement(By.XPath($"//div[@data-tab='fourth']//table/tbody[{rowNumber}]/tr/td[3]")).Text;
               
                ReportLogger.LogInfo($"Retrieved [row {rowNumber}]: Country: {getCertificate}");
                if (Certificate.Equals(getCertificate) && From.Equals(getFrom) && Year.Equals(getYear)) { recordPresent = true; }
            }

            return recordPresent;
        }

        public int RowCount()
        {
            return CertificationRows.Count;
        }

        public int GetCertificationRow(string Certificate, string From, string Year)
        {
            string getCertificate, getFrom, getYear;

            for (int i = 1; i <= RowCount(); i++)
            {
                try
                {
                    getCertificate = driver.FindElement(By.XPath($"//div[@data-tab='fourth']//table/tbody[{i}]/tr/td[1]")).Text;
                    getFrom = driver.FindElement(By.XPath($"//div[@data-tab='fourth']//table/tbody[{i}]/tr/td[2]")).Text;
                    getYear = driver.FindElement(By.XPath($"//div[@data-tab='fourth']//table/tbody[{i}]/tr/td[3]")).Text;
                    if (Certificate.Equals(getCertificate) && From.Equals(getFrom) && Year.Equals(getYear))
                    {
                        ReportLogger.LogInfo($"Certification Present at row: {i}");
                        return i;
                    }
                }
                catch (NoSuchElementException)
                {
                    continue;
                }
            }
            return 0;
        }


        public void EditCertificationRecord(string Certificate, string From, string Year)
        {

            // click edit pencil icon for the existing record
            WaitUtils.WaitToBeVisible(driver, "Xpath", "EditPencilIcon", 10);
            LastEditPencilIcon.Click();

            if (Certificate.Length > 0)
            {
                CertificateTextbox.Clear();
                CertificateTextbox.SendKeys(Certificate);
            }

            if (From.Length > 0)

            {
                CertifiedFromTextbox.Clear();
                CertifiedFromTextbox.SendKeys(From);
            }

            if(Year.Length > 0)
            {
                YearDropdown.Click();
                WaitUtils.WaitToBeVisible(driver, "Xpath", "YearDropdown", 30);

                IWebElement YearOption = driver.FindElement(By.XPath("//*[@value='" + Year + "']"));
                YearOption.Click();
                Thread.Sleep(1000);
            }


            UpdateButton.Click();
            //Thread.Sleep(5000);
        }

        public void DeleteLastCertificationRecords()
        {

            LastDeletePencilIcon.Click();
            Thread.Sleep(3000);
        }

        public void ClearCertification()
        {

            try
            {
                int rowCount = CertificationRows.Count;

                for (int i = 1; i <= rowCount; i++)
                {
                    DeleteLastCertificationRecords();
                    Thread.Sleep(1000);
                }
            }
            catch (NullReferenceException)
            {
                return;
            }
        }

    }
}
