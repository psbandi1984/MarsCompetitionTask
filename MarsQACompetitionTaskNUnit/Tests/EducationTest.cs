using MarsQACompetitionTaskNUnit.Assertions;
using NUnit.Framework;


namespace MarsQACompetitionTaskNUnit.Tests
{
    [Parallelizable]
    [TestFixture]
    public class EducationTest : BaseTest
    {
        [SetUp]
        public void SetUpEducation()
        {
            educationPageObject.NavigateToEducationTab();
            Thread.Sleep(1000);
        }      

        [Test, Order(1), Description("This test create a new Education record")]
        public void TestCreateEducationRecord()
        {
           
            educationPageObject.ClearEducation();
            educationPageObject.CreateEducationRecord(educationConfig[0].University, educationConfig[0].Country, educationConfig[0].Title, educationConfig[0].Degree, educationConfig[0].GraduationYear);
            AssertionHelpers.AssertToolTipMessage(educationPageObject, educationConfig[0].AssertionMessage);
            bool recordPresent = educationPageObject.IsEducationRecordPresent(educationConfig[0].University, educationConfig[0].Country, educationConfig[0].Title, educationConfig[0].Degree, educationConfig[0].GraduationYear);
            Assert.IsTrue(recordPresent);
            educationPageObject.ClearEducation();
        }

        [Test, Order(2), Description("This test edit Education record")]
        public void TestEditEducationRecord()
        {           
            educationPageObject.ClearEducation();
            educationPageObject.CreateEducationRecord(educationConfig[0].University, educationConfig[0].Country, educationConfig[0].Title, educationConfig[0].Degree, educationConfig[0].GraduationYear);
            Thread.Sleep(1000);
            educationPageObject.EditEducationRecord(educationConfig[4].University, educationConfig[4].Country, educationConfig[4].Title, educationConfig[4].Degree, educationConfig[4].GraduationYear);
            AssertionHelpers.AssertToolTipMessage(educationPageObject, educationConfig[4].AssertionMessage);
            bool recordPresent = educationPageObject.IsEducationRecordPresent(educationConfig[4].University, educationConfig[4].Country, educationConfig[4].Title, educationConfig[4].Degree, educationConfig[4].GraduationYear);
            Assert.IsTrue(recordPresent);
            Thread.Sleep(1000);
            educationPageObject.ClearEducation();
        }

        [Test, Order(3), Description("This test delete specific Education record")]
        public void TestDeleteEducationRecord()
        {           
            educationPageObject.ClearEducation();       
            educationPageObject.CreateEducationRecord(educationConfig[0].University, educationConfig[0].Country, educationConfig[0].Title, educationConfig[0].Degree, educationConfig[0].GraduationYear);
            Thread.Sleep(3000);
            educationPageObject.DeleteLastEducationRecords();
            AssertionHelpers.AssertToolTipMessage(educationPageObject, educationConfig[8].AssertionMessage);
            bool recordPresent = educationPageObject.IsEducationRecordPresent(educationConfig[0].University, educationConfig[0].Country, educationConfig[0].Title, educationConfig[0].Degree, educationConfig[0].GraduationYear);
            Assert.IsFalse(recordPresent);          
        }

        [Test, Order(4), Description("This test add Education record with Null data")]
        public void TestCreateEducationRecordWithNullData()
        {
            educationPageObject.ClearEducation();
            educationPageObject.AddNewButton.Click();
            educationPageObject.AddButton.Click();
            AssertionHelpers.AssertToolTipMessage(educationPageObject, educationConfig[1].AssertionMessage);
            educationPageObject.CancelButton.Click();
            Thread.Sleep(1000);
        }

        [Test, Order(5), Description("This test add Education record with Duplicate data")]
        public void TestCreateEducationRecordWithDuplicateData()
        {
            educationPageObject.ClearEducation();
            educationPageObject.CreateEducationRecord(educationConfig[0].University, educationConfig[0].Country, educationConfig[0].Title, educationConfig[0].Degree, educationConfig[0].GraduationYear);
            Thread.Sleep(1000);
            educationPageObject.CreateEducationRecord(educationConfig[2].University, educationConfig[2].Country, educationConfig[2].Title, educationConfig[2].Degree, educationConfig[2].GraduationYear);
            AssertionHelpers.AssertToolTipMessage(educationPageObject, educationConfig[2].AssertionMessage);
            int rowCount = educationPageObject.RowCount();           
            Assert.That(educationPageObject.RowCount(), Is.EqualTo(rowCount));
            Thread.Sleep(1000);
            educationPageObject.ClearEducation();

        }


        [Test, Order(6), Description("This test add Education record with Invalid data")]
        public void TestCreateEducationRecordWithInvalidData()
        {
            educationPageObject.ClearEducation();
            educationPageObject.CreateEducationRecord(educationConfig[3].University, educationConfig[3].Country, educationConfig[3].Title, educationConfig[3].Degree, educationConfig[3].GraduationYear);
            AssertionHelpers.AssertToolTipMessage(educationPageObject, educationConfig[3].AssertionMessage);
            bool recordPresent = educationPageObject.IsEducationRecordPresent(educationConfig[3].University, educationConfig[3].Country, educationConfig[3].Title, educationConfig[3].Degree, educationConfig[3].GraduationYear);
            Assert.IsTrue(recordPresent);
            educationPageObject.ClearEducation();
        }

        [Test, Order(7), Description("This test add Education record with Some data")]
        public void TestAddEducationRecordWithSomeData()
        {
            educationPageObject.ClearEducation();
            educationPageObject.CreateEducationWithSomeData(educationConfig[9].Country, educationConfig[9].Title, educationConfig[9].GraduationYear); 
            Thread.Sleep(1000);
            AssertionHelpers.AssertToolTipMessage(educationPageObject, educationConfig[9].AssertionMessage);
            educationPageObject.CancelButton.Click();
            educationPageObject.ClearEducation();

        }


        [Test, Order(8), Description("This test edit Education record with Some data")]
        public void TestEditEducationRecordWithSomeData()
        {
            educationPageObject.ClearEducation();
            educationPageObject.CreateEducationRecord(educationConfig[0].University, educationConfig[0].Country, educationConfig[0].Title, educationConfig[0].Degree, educationConfig[0].GraduationYear);
            educationPageObject.EditEducationWithSomeData(educationConfig[5].University, educationConfig[5].Country, educationConfig[5].Title, educationConfig[0].Degree, educationConfig[5].GraduationYear);
            Thread.Sleep(1000);
            AssertionHelpers.AssertToolTipMessage(educationPageObject, educationConfig[5].AssertionMessage);
            educationPageObject.CancelButton.Click();
            educationPageObject.ClearEducation();

        }

        [Test, Order(9), Description("This test edit Education record with Duplicate data")]
        public void TestEditEducationRecordWithDuplicateData()
        {
            educationPageObject.ClearEducation();
            educationPageObject.CreateEducationRecord(educationConfig[4].University, educationConfig[4].Country, educationConfig[4].Title, educationConfig[4].Degree, educationConfig[4].GraduationYear);
            educationPageObject.EditEducationRecord(educationConfig[6].University, educationConfig[6].Country, educationConfig[6].Title, educationConfig[6].Degree, educationConfig[6].GraduationYear);            
            int rowCount = educationPageObject.RowCount();
            Assert.That(educationPageObject.RowCount(), Is.EqualTo(rowCount));
            AssertionHelpers.AssertToolTipMessage(educationPageObject, educationConfig[6].AssertionMessage);
            Thread.Sleep(1000);
            educationPageObject.CancelButton.Click();
            educationPageObject.ClearEducation();
        }

        [Test, Order(10), Description("This test edit Education record with Invalid data")]
        public void TestEditEducationRecordWithInvalidData()
        {
            educationPageObject.ClearEducation();
            educationPageObject.CreateEducationRecord(educationConfig[6].University, educationConfig[6].Country, educationConfig[6].Title, educationConfig[6].Degree, educationConfig[6].GraduationYear);
            educationPageObject.EditEducationRecord(educationConfig[7].University, educationConfig[7].Country, educationConfig[7].Title, educationConfig[7].Degree, educationConfig[7].GraduationYear);
            int rowCount = educationPageObject.RowCount();
            Assert.That(educationPageObject.RowCount(), Is.EqualTo(rowCount));
            AssertionHelpers.AssertToolTipMessage(educationPageObject, educationConfig[7].AssertionMessage);
        }


    }
}
