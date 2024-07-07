using System.Text.Json;

namespace MarsQACompetitionTaskNUnit.Utilities.JsonReader
{
    public class CertificationConfig
    {
        public string OriginalCertificate { get; set; }
        public string Certificate { get; set; }
        public string From { get; set; }
        public string Year { get; set; }
        public string AssertionMessage { get; set; }
        public bool IsValid { get; set; }

        
        public static List<CertificationConfig> LoadConfig(string fileName)
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "Certification", fileName);
            string jsonString = File.ReadAllText(jsonFilePath);
            return JsonSerializer.Deserialize<List<CertificationConfig>>(jsonString);
        }

        public static List<CertificationConfig> LoadCreateCertificationWithValidData()
        {
            return LoadConfig("CreateCertificationWithValidData.json");
        }

        public static List<CertificationConfig> LoadEditCertificationWithValidData()
        {
            return LoadConfig("EditCertificationWithValidData.json");
        }

        public static List<CertificationConfig> LoadDeleteCertification()
        {
            return LoadConfig("DeleteCertification.json");
        }

        public static List<CertificationConfig> LoadCreateCertificationWithNullData()
        {
            return LoadConfig("CreateCertificationWithNullData.json");
        }

        public static List<CertificationConfig> LoadCreateCertificationWithDuplicateData()
        {
            return LoadConfig("CreateCertificationWithDuplicateData.json");
        }

        public static List<CertificationConfig> LoadEditCertificationWithNullData()
        {
            return LoadConfig("EditCertificationWithNullData.json");
        }

        public static List<CertificationConfig> LoadEditCertificationWithDuplicateData()
        {
            return LoadConfig("EditCertificationWithDuplicateData.json");
        }
    }
}
