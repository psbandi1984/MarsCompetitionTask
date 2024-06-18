using System.Text.Json;

namespace MarsQACompetitionTaskNUnit.Utilities
{
    public class CertificationConfig
    {
        public string Certificate { get; set; }
        public string From { get; set; }
        public string Year { get; set; }
        public string AssertionMessage { get; set; }

        public static List<CertificationConfig> LoadConfig()
        {
            // Load JSON data from the file
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "CertificationTestData.json");
            string jsonString = File.ReadAllText(jsonFilePath);

            //deserialization of data
            List<CertificationConfig> certificationConfig = JsonSerializer.Deserialize<List<CertificationConfig>>(jsonString);

            return certificationConfig;

        }
    }
}
