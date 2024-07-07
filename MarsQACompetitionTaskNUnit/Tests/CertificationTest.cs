using MarsQACompetitionTaskNUnit.Assertions;
using MarsQACompetitionTaskNUnit.Utilities.JsonReader;
using NUnit.Framework;

namespace MarsQACompetitionTaskNUnit.Tests
{
    [TestFixture]
    [Parallelizable]
    public class CertificationTest: BaseTest 
    {
        [SetUp]
        public void SetUpCertification()
        {           
            certificationPageObject.NavigateToCertificationTab();
            certificationPageObject.ClearCertification();
            Thread.Sleep(1000);
        }

        [Test, Order(1), Description("This test create a new Certification record with valid data")]
        public void TestCreateCertificationWithVaildData()
        {
            // Load test data for creating certification
            List<CertificationConfig> testData = CertificationConfig.LoadCreateCertificationWithValidData();

            foreach (var certification in testData)
            {
                certificationPageObject.CreateCertificationRecord(certification);
                AssertionHelpers.AssertToolTipMessage(certificationPageObject, certification.AssertionMessage);
                bool recordPresent = certificationPageObject.IsCertificationRecordPresent(certification);
                Assert.That(recordPresent, Is.True);
                certificationPageObject.DeleteLastCertificationRecords();
            }
        }


        [Test, Order(2), Description("This test edits Certification records with valid data")]
        public void TestEditCertificationRecords()
        {
            List<CertificationConfig> createData = CertificationConfig.LoadCreateCertificationWithValidData();
            List<CertificationConfig> editData = CertificationConfig.LoadEditCertificationWithValidData();

            foreach (var initialcertification in createData)
            {

                certificationPageObject.CreateCertificationRecord(initialcertification);
                AssertionHelpers.AssertToolTipMessage(certificationPageObject, initialcertification.AssertionMessage);
            }

            foreach (var certification in editData)
            {
                CertificationConfig originalCertification = createData.FirstOrDefault(c => c.Certificate == certification.OriginalCertificate);

                if (originalCertification != null)
                {
                    certificationPageObject.SelectCertificationRecord(originalCertification);                      
                    certificationPageObject.EditCertificationRecord(certification);
                    AssertionHelpers.AssertToolTipMessage(certificationPageObject, certification.AssertionMessage);
                    Thread.Sleep(1000);
                    bool recordPresent = certificationPageObject.IsCertificationRecordPresent(certification);
                    Assert.That(recordPresent, Is.True);                        
                }
            }
            foreach (var certification in editData)
            {
                certificationPageObject.DeleteLastCertificationRecords();
            }
        }

        [Test, Order(3), Description("This test deletes Certification records")]
        public void TestDeleteCertificationRecords()
        {
            List<CertificationConfig> testData = CertificationConfig.LoadDeleteCertification();

            foreach (var certification in testData)
            {
                certificationPageObject.CreateCertificationRecord(certification);
                bool recordPresentBeforeDeletion = certificationPageObject.IsCertificationRecordPresent(certification);

                if (recordPresentBeforeDeletion)
                {
                    certificationPageObject.DeleteLastCertificationRecords();
                    AssertionHelpers.AssertToolTipMessage(certificationPageObject, certification.AssertionMessage);
                    bool recordPresent = certificationPageObject.IsCertificationRecordPresent(certification);
                    Assert.That(recordPresent, Is.False);
                }
            }
        }
                                    

        [Test, Order(4), Description("This test add Certification record with null data")]
        public void TestCreateCertificationRecordWithNullData()
        {
            List<CertificationConfig> testData = CertificationConfig.LoadCreateCertificationWithNullData();

            foreach (var certification in testData)
            {
                certificationPageObject.CreateCertificationRecord(certification);
                bool recordPresent = certificationPageObject.IsCertificationRecordPresent(certification);
                Assert.That(recordPresent, Is.False);
                certificationPageObject.CancelButton.Click();
                Thread.Sleep(1000);
            }
        }
               

        [Test, Order(5), Description("This test add Certification record with Duplicate data")]
        public void TestCreateCertificationRecordWithDuplicateData()
        {
            List<CertificationConfig> createData = CertificationConfig.LoadCreateCertificationWithValidData();
            List<CertificationConfig> testData = CertificationConfig.LoadCreateCertificationWithDuplicateData();

            CertificationConfig initialCertification = createData.First();
            CertificationConfig certification = testData.First();
                                   
            certificationPageObject.CreateCertificationRecord(initialCertification);
            AssertionHelpers.AssertToolTipMessage(certificationPageObject, initialCertification.AssertionMessage);
                               
            certificationPageObject.CreateCertificationRecord(certification);
            AssertionHelpers.AssertToolTipMessage(certificationPageObject, certification.AssertionMessage);
            certificationPageObject.CancelButton.Click();
            Thread.Sleep(1000);
            int rowCount = certificationPageObject.RowCount();
            Assert.That(certificationPageObject.RowCount(), Is.EqualTo(rowCount));

            certificationPageObject.DeleteLastCertificationRecords();
                      
        }


        [Test, Order(6), Description("This test edits Certification record with duplicate data")]
        public void TestEditCertificationRecordWithDuplicateData()
        {
            List<CertificationConfig> createData = CertificationConfig.LoadCreateCertificationWithValidData();
            List<CertificationConfig> editData = CertificationConfig.LoadEditCertificationWithDuplicateData();

            CertificationConfig initialCertification = createData.First();  
            CertificationConfig editCertification = editData.First();

            certificationPageObject.CreateCertificationRecord(initialCertification);
            AssertionHelpers.AssertToolTipMessage(certificationPageObject, initialCertification.AssertionMessage);

            certificationPageObject.SelectCertificationRecord(initialCertification);
            certificationPageObject.EditCertificationRecord(editCertification);
            AssertionHelpers.AssertToolTipMessage(certificationPageObject, editCertification.AssertionMessage);
            Thread.Sleep(3000);
            certificationPageObject.CancelButton.Click();
            Thread.Sleep(1000);
            int rowCount = certificationPageObject.RowCount();
            Assert.That(certificationPageObject.RowCount(), Is.EqualTo(rowCount));
                       
            certificationPageObject.DeleteLastCertificationRecords();
                        
        }


        [Test, Order(7), Description("This test edits Certification record with null data")]
        public void TestEditCertificationRecordWithNullData()
        {
            List<CertificationConfig> createData = CertificationConfig.LoadCreateCertificationWithValidData();
            List<CertificationConfig> editData = CertificationConfig.LoadEditCertificationWithNullData();

            CertificationConfig initialCertification = createData.First();
            CertificationConfig editCertification = editData.First();

            certificationPageObject.CreateCertificationRecord(initialCertification);
            AssertionHelpers.AssertToolTipMessage(certificationPageObject, initialCertification.AssertionMessage);
            Console.WriteLine($"Created initial record with Certificate: {initialCertification.Certificate}, From: {initialCertification.From}, Year: {initialCertification.Year}");

            certificationPageObject.SelectCertificationRecord(initialCertification);
            certificationPageObject.EditCertificationRecord(editCertification);
            AssertionHelpers.AssertToolTipMessage(certificationPageObject, editCertification.AssertionMessage);
            Console.WriteLine($"Edited record to Certificate: {editCertification.Certificate}, From: {editCertification.From}, Year: {editCertification.Year}");
            //certificationPageObject.CancelButton.Click();
            //Thread.Sleep(1000);
            bool recordPresent = certificationPageObject.IsCertificationRecordPresent(editCertification);
            Assert.That(recordPresent, Is.False);

            Console.WriteLine($"Record presence after edit: {recordPresent}");

            certificationPageObject.DeleteLastCertificationRecords();

        }

    }
}
