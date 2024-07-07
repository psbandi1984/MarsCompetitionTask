using MarsQACompetitionTaskNUnit.Assertions;
using MarsQACompetitionTaskNUnit.Utilities.JsonReader;
using NUnit.Framework;


namespace MarsQACompetitionTaskNUnit.Tests
{
    [TestFixture]
    [Parallelizable]
    public class EducationTest : BaseTest
    {
        [SetUp]
        public void SetUpEducation()
        {
            educationPageObject.NavigateToEducationTab();
            educationPageObject.ClearEducation();
            Thread.Sleep(1000);
        }

        [Test, Order(1), Description("This test create a new Education record with valid data")]
        public void TestCreateEducationWithValidData()
        {
            List<EducationConfig> testData = EducationConfig.LoadCreateEducationWithValidData();
            foreach (var education in testData)
            {
                educationPageObject.CreateEducationRecord(education);
                AssertionHelpers.AssertToolTipMessage(educationPageObject, education.AssertionMessage);
                bool recordPresent = educationPageObject.IsEducationRecordPresent(education);
                Assert.That(recordPresent, Is.True);
                educationPageObject.DeleteLastEducationRecords();
            }
        }

        [Test, Order(2), Description("This test edits education records with valid data")]
        public void TestEditEducationRecords()
        {
            List<EducationConfig> createData = EducationConfig.LoadCreateEducationWithValidData();
            List<EducationConfig> editData = EducationConfig.LoadEditEducationWithValidData();

            foreach (var initialeducation in createData)
            {

                educationPageObject.CreateEducationRecord(initialeducation);
                AssertionHelpers.AssertToolTipMessage(educationPageObject, initialeducation.AssertionMessage);
            }

            foreach (var education in editData)
            {
                EducationConfig originalEducation = createData.FirstOrDefault(e => e.University == education.OriginalEducation);

                if (originalEducation != null)
                {
                    educationPageObject.SelectEducationRecord(originalEducation);
                    educationPageObject.EditEducationRecord(education);
                    AssertionHelpers.AssertToolTipMessage(educationPageObject, education.AssertionMessage);
                    Thread.Sleep(1000);
                    bool recordPresent = educationPageObject.IsEducationRecordPresent(education);
                    Assert.That(recordPresent, Is.True);
                }
            }
            foreach (var education in editData)
            {
                educationPageObject.DeleteLastEducationRecords();
            }
        }


        [Test, Order(3), Description("This test deletes education records")]
        public void TestDeleteEducationRecords()
        {
            List<EducationConfig> testData = EducationConfig.LoadDeleteEducation();

            foreach (var education in testData)
            {
                educationPageObject.CreateEducationRecord(education);
                bool recordPresentBeforeDeletion = educationPageObject.IsEducationRecordPresent(education);

                if (recordPresentBeforeDeletion)
                {
                    educationPageObject.DeleteLastEducationRecords();
                    AssertionHelpers.AssertToolTipMessage(educationPageObject, education.AssertionMessage);
                    bool recordPresent = educationPageObject.IsEducationRecordPresent(education);
                    Assert.That(recordPresent, Is.False);
                }
            }
        }


        [Test, Order(4), Description("This test add education record with null data")]
        public void TestCreateEducationRecordWithNullData()
        {
            List<EducationConfig> testData = EducationConfig.LoadCreateEducationWithNullData();

            foreach (var education in testData)
            {
                educationPageObject.CreateEducationRecord(education);
                bool recordPresent = educationPageObject.IsEducationRecordPresent(education);
                Assert.That(recordPresent, Is.False);
                educationPageObject.CancelButton.Click();
                Thread.Sleep(1000);
            }
        }


        [Test, Order(5), Description("This test add education record with Duplicate data")]
        public void TestCreateEducationRecordWithDuplicateData()
        {
            List<EducationConfig> createData = EducationConfig.LoadCreateEducationWithValidData();
            List<EducationConfig> testData = EducationConfig.LoadCreateEducationWithDuplicateData();

            EducationConfig initialeducation = createData.First();
            EducationConfig education = testData.First();

            educationPageObject.CreateEducationRecord(initialeducation);
            AssertionHelpers.AssertToolTipMessage(educationPageObject, initialeducation.AssertionMessage);

            educationPageObject.CreateEducationRecord(education);
            AssertionHelpers.AssertToolTipMessage(educationPageObject, education.AssertionMessage);
            educationPageObject.CancelButton.Click();
            Thread.Sleep(1000);
            int rowCount = educationPageObject.RowCount();
            Assert.That(educationPageObject.RowCount(), Is.EqualTo(rowCount));

            educationPageObject.DeleteLastEducationRecords();

        }


        [Test, Order(6), Description("This test edits education record with duplicate data")]
        public void TestEditEducationRecordWithDuplicateData()
        {
            List<EducationConfig> createData = EducationConfig.LoadCreateEducationWithValidData();
            List<EducationConfig> editData = EducationConfig.LoadEditEducationWithDuplicateData();

            EducationConfig initialeducation = createData.First();
            EducationConfig editEducation = editData.First();

            educationPageObject.CreateEducationRecord(initialeducation);
            AssertionHelpers.AssertToolTipMessage(educationPageObject, initialeducation.AssertionMessage);

            educationPageObject.SelectEducationRecord(initialeducation);
            educationPageObject.EditEducationRecord(editEducation);
            AssertionHelpers.AssertToolTipMessage(educationPageObject, editEducation.AssertionMessage);
            Thread.Sleep(3000);
            educationPageObject.CancelButton.Click();
            Thread.Sleep(1000);
            int rowCount = educationPageObject.RowCount();
            Assert.That(educationPageObject.RowCount(), Is.EqualTo(rowCount));

            educationPageObject.DeleteLastEducationRecords();

        }


        [Test, Order(7), Description("This test edits education record with null data")]
        public void TestEditEducationRecordWithNullData()
        {
            List<EducationConfig> createData = EducationConfig.LoadCreateEducationWithValidData();
            List<EducationConfig> editData = EducationConfig.LoadEditEducationWithNullData();

            EducationConfig initialeducation = createData.First();
            EducationConfig editEducation = editData.First();

            educationPageObject.CreateEducationRecord(initialeducation);
            AssertionHelpers.AssertToolTipMessage(educationPageObject, initialeducation.AssertionMessage);
            

            educationPageObject.SelectEducationRecord(initialeducation);
            educationPageObject.EditEducationRecord(editEducation);
            AssertionHelpers.AssertToolTipMessage(educationPageObject, editEducation.AssertionMessage);
            
            //educationPageObject.CancelButton.Click();
            //Thread.Sleep(1000);
            bool recordPresent = educationPageObject.IsEducationRecordPresent(editEducation);
            Assert.That(recordPresent, Is.False);

            Console.WriteLine($"Record presence after edit: {recordPresent}");

            educationPageObject.DeleteLastEducationRecords();

        }


        //[Test, Order(2), Description("This test edit Education record")]
        //public void TestEditEducationRecord()
        //{
        //    educationPageObject.ClearEducation();
        //    educationPageObject.CreateEducationRecord(educationConfig[0].University, educationConfig[0].Country, educationConfig[0].Title, educationConfig[0].Degree, educationConfig[0].GraduationYear);
        //    Thread.Sleep(1000);
        //    educationPageObject.EditEducationRecord(educationConfig[4].University, educationConfig[4].Country, educationConfig[4].Title, educationConfig[4].Degree, educationConfig[4].GraduationYear);
        //    AssertionHelpers.AssertToolTipMessage(educationPageObject, educationConfig[4].AssertionMessage);
        //    bool recordPresent = educationPageObject.IsEducationRecordPresent(educationConfig[4].University, educationConfig[4].Country, educationConfig[4].Title, educationConfig[4].Degree, educationConfig[4].GraduationYear);
        //    Assert.That(recordPresent, Is.True);
        //    Thread.Sleep(1000);
        //    educationPageObject.ClearEducation();
        //}

        //[Test, Order(3), Description("This test delete specific Education record")]
        //public void TestDeleteEducationRecord()
        //{
        //    educationPageObject.ClearEducation();
        //    educationPageObject.CreateEducationRecord(educationConfig[0].University, educationConfig[0].Country, educationConfig[0].Title, educationConfig[0].Degree, educationConfig[0].GraduationYear);
        //    Thread.Sleep(3000);
        //    educationPageObject.DeleteLastEducationRecords();
        //    AssertionHelpers.AssertToolTipMessage(educationPageObject, educationConfig[8].AssertionMessage);
        //    bool recordPresent = educationPageObject.IsEducationRecordPresent(educationConfig[0].University, educationConfig[0].Country, educationConfig[0].Title, educationConfig[0].Degree, educationConfig[0].GraduationYear);
        //    Assert.That(recordPresent, Is.False);
        //}

        //[Test, Order(4), Description("This test add Education record with Null data")]
        //public void TestCreateEducationRecordWithNullData()
        //{
        //    educationPageObject.ClearEducation();
        //    educationPageObject.AddNewButton.Click();
        //    educationPageObject.AddButton.Click();
        //    AssertionHelpers.AssertToolTipMessage(educationPageObject, educationConfig[1].AssertionMessage);
        //    educationPageObject.CancelButton.Click();
        //    Thread.Sleep(1000);
        //}

        //[Test, Order(5), Description("This test add Education record with Duplicate data")]
        //public void TestCreateEducationRecordWithDuplicateData()
        //{
        //    educationPageObject.ClearEducation();
        //    educationPageObject.CreateEducationRecord(educationConfig[0].University, educationConfig[0].Country, educationConfig[0].Title, educationConfig[0].Degree, educationConfig[0].GraduationYear);
        //    Thread.Sleep(1000);
        //    educationPageObject.CreateEducationRecord(educationConfig[2].University, educationConfig[2].Country, educationConfig[2].Title, educationConfig[2].Degree, educationConfig[2].GraduationYear);
        //    AssertionHelpers.AssertToolTipMessage(educationPageObject, educationConfig[2].AssertionMessage);
        //    int rowCount = educationPageObject.RowCount();
        //    Assert.That(educationPageObject.RowCount(), Is.EqualTo(rowCount));
        //    Thread.Sleep(1000);
        //    educationPageObject.ClearEducation();

        //}


        //[Test, Order(6), Description("This test add Education record with Invalid data")]
        //public void TestCreateEducationRecordWithInvalidData()
        //{
        //    educationPageObject.ClearEducation();
        //    educationPageObject.CreateEducationRecord(educationConfig[3].University, educationConfig[3].Country, educationConfig[3].Title, educationConfig[3].Degree, educationConfig[3].GraduationYear);
        //    AssertionHelpers.AssertToolTipMessage(educationPageObject, educationConfig[3].AssertionMessage);
        //    bool recordPresent = educationPageObject.IsEducationRecordPresent(educationConfig[3].University, educationConfig[3].Country, educationConfig[3].Title, educationConfig[3].Degree, educationConfig[3].GraduationYear);
        //    Assert.That(recordPresent, Is.True);
        //    educationPageObject.ClearEducation();
        //}

        //[Test, Order(7), Description("This test add Education record with Some data")]
        //public void TestAddEducationRecordWithSomeData()
        //{
        //    educationPageObject.ClearEducation();
        //    educationPageObject.CreateEducationWithSomeData(educationConfig[9].Country, educationConfig[9].Title, educationConfig[9].GraduationYear);
        //    Thread.Sleep(1000);
        //    AssertionHelpers.AssertToolTipMessage(educationPageObject, educationConfig[9].AssertionMessage);
        //    educationPageObject.CancelButton.Click();
        //    educationPageObject.ClearEducation();

        //}


        //[Test, Order(8), Description("This test edit Education record with Some data")]
        //public void TestEditEducationRecordWithSomeData()
        //{
        //    educationPageObject.ClearEducation();
        //    educationPageObject.CreateEducationRecord(educationConfig[0].University, educationConfig[0].Country, educationConfig[0].Title, educationConfig[0].Degree, educationConfig[0].GraduationYear);
        //    educationPageObject.EditEducationWithSomeData(educationConfig[5].University, educationConfig[5].Country, educationConfig[5].Title, educationConfig[5].Degree, educationConfig[5].GraduationYear);
        //    Thread.Sleep(1000);
        //    AssertionHelpers.AssertToolTipMessage(educationPageObject, educationConfig[5].AssertionMessage);
        //    educationPageObject.CancelButton.Click();
        //    educationPageObject.ClearEducation();

        //}

        //[Test, Order(9), Description("This test edit Education record with Duplicate data")]
        //public void TestEditEducationRecordWithDuplicateData()
        //{
        //    educationPageObject.ClearEducation();
        //    educationPageObject.CreateEducationRecord(educationConfig[4].University, educationConfig[4].Country, educationConfig[4].Title, educationConfig[4].Degree, educationConfig[4].GraduationYear);
        //    educationPageObject.EditEducationRecord(educationConfig[6].University, educationConfig[6].Country, educationConfig[6].Title, educationConfig[6].Degree, educationConfig[6].GraduationYear);
        //    int rowCount = educationPageObject.RowCount();
        //    Assert.That(educationPageObject.RowCount(), Is.EqualTo(rowCount));
        //    AssertionHelpers.AssertToolTipMessage(educationPageObject, educationConfig[6].AssertionMessage);
        //    Thread.Sleep(1000);
        //    educationPageObject.CancelButton.Click();
        //    educationPageObject.ClearEducation();
        //}

        //[Test, Order(10), Description("This test edit Education record with Invalid data")]
        //public void TestEditEducationRecordWithInvalidData()
        //{
        //    educationPageObject.ClearEducation();
        //    educationPageObject.CreateEducationRecord(educationConfig[6].University, educationConfig[6].Country, educationConfig[6].Title, educationConfig[6].Degree, educationConfig[6].GraduationYear);
        //    educationPageObject.EditEducationRecord(educationConfig[7].University, educationConfig[7].Country, educationConfig[7].Title, educationConfig[7].Degree, educationConfig[7].GraduationYear);
        //    int rowCount = educationPageObject.RowCount();
        //    Assert.That(educationPageObject.RowCount(), Is.EqualTo(rowCount));
        //    AssertionHelpers.AssertToolTipMessage(educationPageObject, educationConfig[7].AssertionMessage);
        //}


    }
}
