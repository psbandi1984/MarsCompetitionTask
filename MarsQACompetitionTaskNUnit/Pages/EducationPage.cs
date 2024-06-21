using MarsQACompetitionTaskNUnit.Utilities;
using OpenQA.Selenium;

namespace MarsQACompetitionTaskNUnit.Pages
{
    public class EducationPage : CommonDriver
    {
       
        public EducationPage(IWebDriver driver) : base(driver)
        {
            this.driver = driver;
        }

        public EducationPage() : base() 
        {

        }

        //WebElements
        public IWebElement ProfileTab => driver.FindElement(By.XPath("//section//a[@href='/Account/Profile']"));
        public IWebElement EducationTab => driver.FindElement(By.XPath("//a[@data-tab='third']"));
        public IWebElement AddNewButton => driver.FindElement(By.XPath("//div[@data-tab='third']//table//div[@class='ui teal button '][(text()='Add New')]"));
        public IWebElement UniversityTextbox => driver.FindElement(By.Name("instituteName"));
        public IWebElement CountryDropdown => driver.FindElement(By.Name("country"));
        public IWebElement CountryOption => driver.FindElement(By.XPath("//*[@value='\" + Country  + \"']"));
        public IWebElement TitleDropdown => driver.FindElement(By.Name("title"));
        public IWebElement TitleOption => driver.FindElement(By.XPath("//*[@value='\" + Title  + \"']"));
        public IWebElement DegreeTextbox => driver.FindElement(By.Name("degree"));
        public IWebElement GraduationYearDropdown => driver.FindElement(By.Name("yearOfGraduation"));
        public IWebElement GraduationYearOption => driver.FindElement(By.XPath("//*[@value='\" + Graduation Year  + \"']"));
        public IWebElement AddButton => driver.FindElement(By.XPath("//input[@value='Add']"));
        public IWebElement CancelButton => driver.FindElement(By.XPath("//input[@value='Cancel']"));
        public IWebElement ToolTipMessage => driver.FindElement(By.XPath("//*[@class='ns-box-inner']"));
        public IWebElement LastEditPencilIcon => driver.FindElement(By.XPath("//div[@data-tab='third']//table/tbody[last()]//i[@class='outline write icon']"));
        public IWebElement LastDeletePencilIcon => driver.FindElement(By.XPath("//div[@data-tab='third']//table/tbody[last()]//i[@class='remove icon']"));
        public IWebElement UpdateButton => driver.FindElement(By.XPath("//input[@value='Update']"));

        public IList<IWebElement> EducationRows => driver.FindElements(By.XPath("//div[@data-tab='third']//table/tbody"));
        
        //Methods

        public void NavigateToEducationTab()
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);         
            ProfileTab.Click();           

            WaitUtils.WaitToBeClickable(driver, "Xpath", "EducationTab", 20);
            EducationTab.Click();
            
        }
        public void CreateEducationRecord(string University, string Country, string Title, string Degree, string GraduationYear)
        {
            // Click on Add New button
            WaitUtils.WaitToBeVisible(driver, "Xpath", "AddNewButton", 20);
            AddNewButton.Click();

            // Enter University Name    
            UniversityTextbox.Click();
            UniversityTextbox.SendKeys(University);

            // Select Country from dropdown list
            WaitUtils.WaitToBeVisible(driver, "Xpath", "CountryDropdown", 5);
            CountryDropdown.Click();
            driver.FindElement(By.XPath("//*[@value='" + Country + "']")).Click();

            // Select Title from dropdown list
            WaitUtils.WaitToBeVisible(driver, "Xpath", "TitleDropdown", 5);
            TitleDropdown.Click();
            driver.FindElement(By.XPath("//*[@value='" + Title + "']")).Click();

            // Enter Degree    
            DegreeTextbox.Click();
            DegreeTextbox.SendKeys(Degree);

            // Select GraduationYear from dropdown list
            WaitUtils.WaitToBeVisible(driver, "Xpath", "GraduationYearDropdown", 5);
            GraduationYearDropdown.Click();
            driver.FindElement(By.XPath("//*[@value='" + GraduationYear + "']")).Click();

            // Click on save button
            AddButton.Click();
            Thread.Sleep(5000);

        }

        public void CreateEducationWithSomeData(string Country, string Title, string GraduationYear)
        {
            // Click on Add New button
            WaitUtils.WaitToBeVisible(driver, "Xpath", "AddNewButton", 20);
            AddNewButton.Click();

            // Select Country from dropdown list
            WaitUtils.WaitToBeVisible(driver, "Xpath", "CountryDropdown", 5);
            CountryDropdown.Click();
            driver.FindElement(By.XPath("//*[@value='" + Country + "']")).Click();

            // Select Title from dropdown list
            WaitUtils.WaitToBeVisible(driver, "Xpath", "TitleDropdown", 5);
            TitleDropdown.Click();
            driver.FindElement(By.XPath("//*[@value='" + Title + "']")).Click();

            // Select GraduationYear from dropdown list
            WaitUtils.WaitToBeVisible(driver, "Xpath", "GraduationYearDropdown", 5);
            GraduationYearDropdown.Click();
            driver.FindElement(By.XPath("//*[@value='" + GraduationYear + "']")).Click();

            // Click on save button
            AddButton.Click();
            Thread.Sleep(5000);

        }

        public bool IsEducationRecordPresent(string University, string Country, string Title, string Degree, string GraduationYear, int rowNumber = 0)
        {
            bool recordPresent = false;
            string getUniversity, getCountry, getTitle, getDegree, getGraduationYear;
            int educationRowPresent;

            if (rowNumber == 0)
            {
                educationRowPresent = GetEducationRow(University, Country, Title, Degree, GraduationYear);
                if (educationRowPresent > 0)
                {
                    recordPresent = true;
                }
            }
            else
            {
                WaitUtils.WaitToBeVisible(driver, "Xpath", "getCountry", 60);
                getCountry = driver.FindElement(By.XPath($"//div[@data-tab='third']//table/tbody[{rowNumber}]/tr/td[1]")).Text;
                WaitUtils.WaitToBeVisible(driver, "Xpath", "getUniversity", 60);
                getUniversity = driver.FindElement(By.XPath($"//div[@data-tab='third']//table/tbody[{rowNumber}]/tr/td[2]")).Text;
                getTitle = driver.FindElement(By.XPath($"//div[@data-tab='third']//table/tbody[{rowNumber}]/tr/td[3]")).Text;
                getDegree = driver.FindElement(By.XPath($"//div[@data-tab='third']//table/tbody[{rowNumber}]/tr/td[4]")).Text;
                getGraduationYear = driver.FindElement(By.XPath($"//div[@data-tab='third']//table/tbody[{rowNumber}]/tr/td[5]")).Text;

                ReportLogger.LogInfo($"Retrieved [row {rowNumber}]: Country: {getCountry}, University: {getUniversity}");
                if (Country.Equals(getCountry) && University.Equals(getUniversity) && Title.Equals(getTitle) && Degree.Equals(getDegree) && GraduationYear.Equals(getGraduationYear)) { recordPresent = true; }
            }

            return recordPresent;
        }

        public int RowCount()
        {
            return EducationRows.Count;
        }

        public int GetEducationRow(string University, string Country, string Title, string Degree, string GraduationYear)
        {
            string getUniversity, getCountry, getTitle, getDegree, getGraduationYear;

            for (int i = 1; i <= RowCount(); i++)
            {
                try
                {
                    getCountry = driver.FindElement(By.XPath($"//div[@data-tab='third']//table/tbody[{i}]/tr/td[1]")).Text;
                    getUniversity = driver.FindElement(By.XPath($"//div[@data-tab='third']//table/tbody[{i}]/tr/td[2]")).Text;
                    getTitle = driver.FindElement(By.XPath($"//div[@data-tab='third']//table/tbody[{i}]/tr/td[3]")).Text;
                    getDegree = driver.FindElement(By.XPath($"//div[@data-tab='third']//table/tbody[{i}]/tr/td[4]")).Text;
                    getGraduationYear = driver.FindElement(By.XPath($"//div[@data-tab='third']//table/tbody[{i}]/tr/td[5]")).Text;
                    if (Country.Equals(getCountry) && University.Equals(getUniversity) && Title.Equals(getTitle) && Degree.Equals(getDegree) && GraduationYear.Equals(getGraduationYear))
                    {
                        ReportLogger.LogInfo($"Education Present at row: {i}");
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

        public void EditEducationRecord(string University, string Country, string Title, string Degree, string GraduationYear)
        {

            // click edit pencil icon for the existing record
            WaitUtils.WaitToBeVisible(driver, "Xpath", "LastEditPencilIcon", 10);
            LastEditPencilIcon.Click();

            if (University.Length > 0)
            {
                UniversityTextbox.Clear();
                UniversityTextbox.SendKeys(University);
            }

            if (Country.Length > 0)
            {
                CountryDropdown.Click();
                WaitUtils.WaitToBeVisible(driver, "Xpath", "CountryDropdown", 30);

                IWebElement EditCountryOption = driver.FindElement(By.XPath("//*[@value='" + Country + "']"));
                EditCountryOption.Click();

            }

            if (Title.Length > 0)
            {
                TitleDropdown.Click();
                WaitUtils.WaitToBeVisible(driver, "Xpath", "TitleDropdown", 30);

                IWebElement TitleOption = driver.FindElement(By.XPath("//*[@value='" + Title + "']"));
                TitleOption.Click();
               

            }

            if (Degree.Length > 0)
            {
                DegreeTextbox.Clear();
                DegreeTextbox.SendKeys(Degree);
            }

            if (GraduationYear.Length > 0)
            {
                GraduationYearDropdown.Click();
                WaitUtils.WaitToBeVisible(driver, "Xpath", "GraduationYearDropdown", 30);

                IWebElement GraduationYearOption = driver.FindElement(By.XPath("//*[@value='" + GraduationYear + "']"));
                GraduationYearOption.Click();

            }

            UpdateButton.Click();
            Thread.Sleep(5000);
        }

        public void EditEducationWithSomeData(string University, string Country, string Title, string Degree, string GraduationYear)
        {
            try
            {

                WaitUtils.WaitToBeVisible(driver, "Xpath", "LastEditPencilIcon", 10);
                LastEditPencilIcon.Click();

                // Update University if a value is provided
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                UniversityTextbox.Clear();
                UniversityTextbox.SendKeys(Keys.Tab);  // Trigger blur event
                Console.WriteLine("UniversityTextbox cleared.");
                if (!string.IsNullOrEmpty(University))
                {

                    UniversityTextbox.SendKeys(University);
                }
                else
                {
                    Console.WriteLine("UniversityTextbox remains empty.");
                }

                CountryDropdown.Click();
                WaitUtils.WaitToBeVisible(driver, "Xpath", "CountryDropdown", 30);
                IWebElement EditCountryOption = driver.FindElement(By.XPath("//*[@value='" + Country + "']"));
                EditCountryOption.Click();
                Console.WriteLine($"CountryDropdown set to: {Country}");

                TitleDropdown.Click();
                WaitUtils.WaitToBeVisible(driver, "Xpath", "TitleDropdown", 30);
                IWebElement TitleOption = driver.FindElement(By.XPath("//*[@value='" + Title + "']"));
                TitleOption.Click();
                Console.WriteLine($"TitleDropdown set to: {Title}");

                // Update Degree if a value is provided
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                DegreeTextbox.Clear();
                DegreeTextbox.SendKeys(Keys.Tab);  // Trigger blur event
                Console.WriteLine("DegreeTextbox cleared.");
                if (!string.IsNullOrEmpty(Degree))
                {

                    DegreeTextbox.SendKeys(Degree);
                }
                else
                {
                    Console.WriteLine("DegreeTextbox remains empty.");
                }

                GraduationYearDropdown.Click();
                WaitUtils.WaitToBeVisible(driver, "Xpath", "GraduationYearDropdown", 30);
                IWebElement GraduationYearOption = driver.FindElement(By.XPath("//*[@value='" + GraduationYear + "']"));
                GraduationYearOption.Click();
                Console.WriteLine($"GraduationYearDropdown set to: {GraduationYear}");

                WaitUtils.WaitToBeClickable(driver, "Xpath", "UpdateButton", 30);
                UpdateButton.Click();
                Console.WriteLine("Update button clicked.");
                Thread.Sleep(5000);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Exception occurred while editing education record: {ex.Message}");
            }
        }
    

        public void DeleteLastEducationRecords()
        {

            LastDeletePencilIcon.Click();
            Thread.Sleep(3000);
        }

        public void ClearEducation()
        {

            try
            {
                int rowCount = EducationRows.Count;
            
                for (int i = 1; i <= rowCount; i++)
                {
                    DeleteLastEducationRecords();
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
