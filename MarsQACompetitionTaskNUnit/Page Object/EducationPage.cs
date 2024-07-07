using MarsQACompetitionTaskNUnit.Utilities;
using MarsQACompetitionTaskNUnit.Utilities.JsonReader;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace MarsQACompetitionTaskNUnit.Pages
{
    public class EducationPage : CommonDriver
    {

        public EducationPage(IWebDriver driver) : base(driver)
        {
           
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

            WaitUtils.WaitToBeClickable(driver, "Xpath", "EducationTab", 10);
            EducationTab.Click();
            
        }
        public void CreateEducationRecord(EducationConfig education)
        {
            
            WaitUtils.WaitToBeVisible(driver, "Xpath", "AddNewButton", 20);
            AddNewButton.Click();

            WaitUtils.WaitToBeVisible(driver, "Xpath", "UniversityTextbox", 10);
            if (!string.IsNullOrEmpty(education.University))
            {
                UniversityTextbox.Click();
                UniversityTextbox.SendKeys(education.University);
            }
                        
            WaitUtils.WaitToBeVisible(driver, "Xpath", "CountryDropdown", 10);
            if (!string.IsNullOrEmpty(education.Country))
            {
                CountryDropdown.Click();
                driver.FindElement(By.XPath("//*[@value='" + education.Country + "']")).Click();
            }
                        
            WaitUtils.WaitToBeVisible(driver, "Xpath", "TitleDropdown", 10);
            if (!string.IsNullOrEmpty(education.Title))
            {
                TitleDropdown.Click();
                driver.FindElement(By.XPath("//*[@value='" + education.Title + "']")).Click();
            }

            WaitUtils.WaitToBeVisible(driver, "Xpath", "DegreeTextbox", 10);
            if (!string.IsNullOrEmpty(education.Degree))
            {
                DegreeTextbox.Click();
                DegreeTextbox.SendKeys(education.Degree);
            }
                       
            WaitUtils.WaitToBeVisible(driver, "Xpath", "GraduationYearDropdown", 10);
            if (!string.IsNullOrEmpty(education.GraduationYear))
            {
                GraduationYearDropdown.Click();
                driver.FindElement(By.XPath("//*[@value='" + education.GraduationYear + "']")).Click();
            }

            WaitUtils.WaitToBeClickable(driver, "Xpath", "AddButton", 30);
            AddButton.Click();

        }
                
        public bool IsEducationRecordPresent(EducationConfig education, int rowNumber = 0)
        {
            bool recordPresent = false;
            string getUniversity, getCountry, getTitle, getDegree, getGraduationYear;
            int educationRowPresent;

            if (rowNumber == 0)
            {
                educationRowPresent = GetEducationRow(education);
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
                if (education.Country.Equals(getCountry) && education.University.Equals(getUniversity) && education.Title.Equals(getTitle) && education.Degree.Equals(getDegree) && education.GraduationYear.Equals(getGraduationYear)) { recordPresent = true; }
            }

            return recordPresent;
        }

        public int RowCount() => EducationRows.Count;

        public int GetEducationRow(EducationConfig education)
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
                    if (education.Country.Equals(getCountry) && education.University.Equals(getUniversity) && education.Title.Equals(getTitle) && education.Degree.Equals(getDegree) && education.GraduationYear.Equals(getGraduationYear))
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

       
        public void SelectEducationRecord(EducationConfig education)
        {
            int rowNumber = GetEducationRow(education);
            if (rowNumber > 0)
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                var editButton = driver.FindElement(By.XPath($"//div[@data-tab='third']//table/tbody[{rowNumber}]//i[@class='outline write icon']"));
                editButton.Click();
            }

        }

        public void EditEducationRecord(EducationConfig education)
        {
            try
            {

                // Update University if a value is provided
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);
                UniversityTextbox.Clear();
                UniversityTextbox.SendKeys(Keys.Tab);  // Trigger blur event
                Console.WriteLine("UniversityTextbox cleared.");
                if (!string.IsNullOrEmpty(education.University))
                {

                    UniversityTextbox.SendKeys(education.University);
                }
                else
                {
                    Console.WriteLine("UniversityTextbox remains empty.");
                }

                CountryDropdown.Click();
                WaitUtils.WaitToBeVisible(driver, "Xpath", "CountryDropdown", 30);
                IWebElement EditCountryOption = driver.FindElement(By.XPath("//*[@value='" + education.Country + "']"));
                EditCountryOption.Click();
                Console.WriteLine($"CountryDropdown set to: {education.Country}");

                TitleDropdown.Click();
                WaitUtils.WaitToBeVisible(driver, "Xpath", "TitleDropdown", 30);
                IWebElement TitleOption = driver.FindElement(By.XPath("//*[@value='" + education.Title + "']"));
                TitleOption.Click();
                Console.WriteLine($"TitleDropdown set to: {education.Title}");
                               
                DegreeTextbox.Clear();
                DegreeTextbox.SendKeys(Keys.Tab);  // Trigger blur event
                Console.WriteLine("DegreeTextbox cleared.");
                if (!string.IsNullOrEmpty(education.Degree))
                {

                    DegreeTextbox.SendKeys(education.Degree);
                }
                else
                {
                    Console.WriteLine("DegreeTextbox remains empty.");
                }

                GraduationYearDropdown.Click();
                WaitUtils.WaitToBeVisible(driver, "Xpath", "GraduationYearDropdown", 50);
                IWebElement GraduationYearOption = driver.FindElement(By.XPath("//*[@value='" + education.GraduationYear + "']"));
                GraduationYearOption.Click();
                Console.WriteLine($"GraduationYearDropdown set to: {education.GraduationYear}");

                WaitUtils.WaitToBeClickable(driver, "Xpath", "UpdateButton", 30);
                UpdateButton.Click();
                Console.WriteLine("Update button clicked.");
                //Thread.Sleep(5000);
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

            if (EducationRows != null && EducationRows.Count > 0)
            {
                
                int rowCount = EducationRows.Count;
            
                for (int i = 1; i <= rowCount; i++)
                {
                    DeleteLastEducationRecords();
                    Thread.Sleep(1000);
                }
            }
           
        }

    }
}
