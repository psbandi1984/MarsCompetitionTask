using System.Text.Json;

namespace MarsQACompetitionTaskNUnit.Utilities
{
    public class EducationConfig
    {
        public string University { get; set; }
        public string Country { get; set; }
        public string Title { get; set; }
        public string Degree { get; set; }
        public string GraduationYear { get; set; }
        public string AssertionMessage { get; set; }

        public static List<EducationConfig> LoadConfig()
        {
            // Load JSON data from the file
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EducationTestData.json");
            string jsonString = File.ReadAllText(jsonFilePath);

            //deserialization of data
            List<EducationConfig> educationConfig = JsonSerializer.Deserialize<List<EducationConfig>>(jsonString);

            return educationConfig;

        }
    }
}
