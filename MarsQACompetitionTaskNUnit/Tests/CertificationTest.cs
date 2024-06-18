using MarsQACompetitionTaskNUnit.Utilities;
using NUnit.Framework;

namespace MarsQACompetitionTaskNUnit.Tests
{
    [Parallelizable]
    [TestFixture]
    public class CertificationTest: BaseTest 
    {
        [SetUp]
        public void SetUpCertification()
        {           
            certificationPageObject.NavigateToCertificationTab();
            Thread.Sleep(1000);
        }

        [Test, Order(1), Description("This test create a new Certification record")]
        public void TestCreateCertificationRecord()
        {         
            certificationPageObject.ClearCertification();
            certificationPageObject.CreateCertificationRecord(certificationConfig[0].Certificate, certificationConfig[0].From, certificationConfig[0].Year);
            AssertionHelpers.AssertToolTipMessage(certificationPageObject, certificationConfig[0].AssertionMessage);
            bool recordPresent = certificationPageObject.IsCertificationRecordPresent(certificationConfig[0].Certificate, certificationConfig[0].From, certificationConfig[0].Year);
            Assert.IsTrue(recordPresent);
            certificationPageObject.ClearCertification();
        }

        [Test, Order(2), Description("This test edit  Certification record")]
        public void TestEditCertificationRecord()
        {
            certificationPageObject.ClearCertification();
            certificationPageObject.CreateCertificationRecord(certificationConfig[0].Certificate, certificationConfig[0].From, certificationConfig[0].Year);
            certificationPageObject.EditCertificationRecord(certificationConfig[6].Certificate, certificationConfig[6].From, certificationConfig[6].Year);
            AssertionHelpers.AssertToolTipMessage(certificationPageObject, certificationConfig[0].AssertionMessage);
            certificationPageObject.ClearCertification();
        }

        [Test, Order(3), Description("This test delete specific Certification record")]
        public void TestDeleteCertificationRecord()
        {
            certificationPageObject.ClearCertification();
            certificationPageObject.CreateCertificationRecord(certificationConfig[0].Certificate, certificationConfig[0].From, certificationConfig[0].Year);
            Thread.Sleep(3000);
            certificationPageObject.DeleteLastCertificationRecords();
            AssertionHelpers.AssertToolTipMessage(certificationPageObject, certificationConfig[2].AssertionMessage);
            bool recordPresent = certificationPageObject.IsCertificationRecordPresent(certificationConfig[0].Certificate, certificationConfig[0].From, certificationConfig[0].Year);
            Assert.IsFalse(recordPresent);
        }

        [Test, Order(4), Description("This test add Certification record with Null data")]
        public void TestCreateCertificationRecordWithNullData()
        {
            certificationPageObject.ClearCertification();
            certificationPageObject.AddNewButton.Click();
            certificationPageObject.AddButton.Click();
            AssertionHelpers.AssertToolTipMessage(certificationPageObject, certificationConfig[1].AssertionMessage);
            certificationPageObject.CancelButton.Click();
            Thread.Sleep(1000);
        }

        [Test, Order(5), Description("This test add Certification record with Duplicate data")]
        public void TestCreateCertificationRecordWithDuplicateData()
        {
            certificationPageObject.ClearCertification();
            certificationPageObject.CreateCertificationRecord(certificationConfig[0].Certificate, certificationConfig[0].From, certificationConfig[0].Year);
            Thread.Sleep(1000);
            certificationPageObject.CreateCertificationRecord(certificationConfig[3].Certificate, certificationConfig[3].From, certificationConfig[3].Year);
            AssertionHelpers.AssertToolTipMessage(certificationPageObject, certificationConfig[3].AssertionMessage);
            int rowCount = certificationPageObject.RowCount();
            Assert.That(certificationPageObject.RowCount(), Is.EqualTo(rowCount));
            Thread.Sleep(1000);
            certificationPageObject.ClearCertification();

        }

        [Test, Order(6), Description("This test add Certification record with some data")]
        public void TestCreateCertificationRecordWithSomeData()
        {
            certificationPageObject.ClearCertification();
            certificationPageObject.CreateCertificationRecord(certificationConfig[4].Certificate, certificationConfig[4].From, certificationConfig[4].Year);
            Thread.Sleep(1000);
            AssertionHelpers.AssertToolTipMessage(certificationPageObject, certificationConfig[4].AssertionMessage);
            certificationPageObject.CancelButton.Click();
            Thread.Sleep(1000);
        }

        [Test, Order(7), Description("This test add Certification record with Invalid data")]
        public void TestCreateCertificationRecordWithInvalidData()
        {
            certificationPageObject.ClearCertification();
            certificationPageObject.CreateCertificationRecord(certificationConfig[5].Certificate, certificationConfig[5].From, certificationConfig[5].Year);
            AssertionHelpers.AssertToolTipMessage(certificationPageObject, certificationConfig[5].AssertionMessage);
            bool recordPresent = certificationPageObject.IsCertificationRecordPresent(certificationConfig[5].Certificate, certificationConfig[5].From, certificationConfig[5].Year);
            Assert.IsTrue(recordPresent);
            certificationPageObject.ClearCertification();
        }

        [Test, Order(8), Description("This test edit Certification record with Null data")]
        public void TestEditCertificationRecordWithNullData()
        {
            certificationPageObject.ClearCertification();
            certificationPageObject.CreateCertificationRecord(certificationConfig[0].Certificate, certificationConfig[0].From, certificationConfig[0].Year);
            certificationPageObject.EditCertificationRecord(certificationConfig[7].Certificate, certificationConfig[7].From, certificationConfig[7].Year);
            certificationPageObject.UpdateButton.Click();
            certificationPageObject.CancelButton.Click();
            AssertionHelpers.AssertToolTipMessage(certificationPageObject, certificationConfig[7].AssertionMessage);
            certificationPageObject.ClearCertification();
        }

        [Test, Order(9), Description("This test edit Certification record with Some data")]
        public void TestEditCertificationRecordWithSomeData()
        {
            certificationPageObject.ClearCertification();
            certificationPageObject.CreateCertificationRecord(certificationConfig[0].Certificate, certificationConfig[0].From, certificationConfig[0].Year);
            certificationPageObject.EditCertificationRecord(certificationConfig[8].Certificate, certificationConfig[8].From, certificationConfig[8].Year);
            Thread.Sleep(1000);
            certificationPageObject.UpdateButton.Click();
            certificationPageObject.CancelButton.Click();
            AssertionHelpers.AssertToolTipMessage(certificationPageObject, certificationConfig[7].AssertionMessage);
            certificationPageObject.ClearCertification();

        }

        [Test, Order(10), Description("This test edit Certification record with Duplicate data")]
        public void TestEditCertificationRecordWithDuplicateData()
        {
            certificationPageObject.ClearCertification();
            certificationPageObject.CreateCertificationRecord(certificationConfig[6].Certificate, certificationConfig[6].From, certificationConfig[6].Year);
            certificationPageObject.EditCertificationRecord(certificationConfig[8].Certificate, certificationConfig[8].From, certificationConfig[8].Year);
            int rowCount = certificationPageObject.RowCount();
            Assert.That(certificationPageObject.RowCount(), Is.EqualTo(rowCount));
            AssertionHelpers.AssertToolTipMessage(certificationPageObject, certificationConfig[8].AssertionMessage);
            Thread.Sleep(1000);
            certificationPageObject.CancelButton.Click();
            certificationPageObject.ClearCertification();
        }

        [Test, Order(11), Description("This test edit Certification record with Invalid data")]
        public void TestEditCertificationRecordWithInvalidData()
        {
            certificationPageObject.ClearCertification();
            certificationPageObject.CreateCertificationRecord(certificationConfig[0].Certificate, certificationConfig[0].From, certificationConfig[0].Year);
            certificationPageObject.EditCertificationRecord(certificationConfig[11].Certificate, certificationConfig[11].From, certificationConfig[11].Year);
            Thread.Sleep(1000);
            int rowCount = certificationPageObject.RowCount();
            Assert.That(certificationPageObject.RowCount(), Is.EqualTo(rowCount));
            AssertionHelpers.AssertToolTipMessage(certificationPageObject, certificationConfig[11].AssertionMessage);
        }


    }
}
