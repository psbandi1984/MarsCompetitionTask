using System.Text.Json;

namespace MarsQACompetitionTaskNUnit.Utilities.JsonReader
{
    public class EducationConfig
    {
        public string OriginalEducation { get; set; }
        public string University { get; set; }
        public string Country { get; set; }
        public string Title { get; set; }
        public string Degree { get; set; }
        public string GraduationYear { get; set; }
        public string AssertionMessage { get; set; }
        public bool IsValid { get; set; }

        public static List<EducationConfig> LoadConfig(string fileName)
        {
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestData", "Education", fileName);
            string jsonString = File.ReadAllText(jsonFilePath);
            return JsonSerializer.Deserialize<List<EducationConfig>>(jsonString);
        }


        public static List<EducationConfig> LoadCreateEducationWithValidData()
        {
            return LoadConfig("CreateEducationWithValidData.json");
        }

        public static List<EducationConfig> LoadEditEducationWithValidData()
        {
            return LoadConfig("EditEducationWithValidData.json");
        }

        public static List<EducationConfig> LoadDeleteEducation()
        {
            return LoadConfig("DeleteEducation.json");
        }

        public static List<EducationConfig> LoadCreateEducationWithNullData()
        {
            return LoadConfig("CreateEducationWithNullData.json");
        }

        public static List<EducationConfig> LoadCreateEducationWithDuplicateData()
        {
            return LoadConfig("CreateEducationWithDuplicateData.json");
        }

        public static List<EducationConfig> LoadEditEducationWithNullData()
        {
            return LoadConfig("EditEducationWithNullData.json");
        }

        public static List<EducationConfig> LoadEditEducationWithDuplicateData()
        {
            return LoadConfig("EditEducationWithDuplicateData.json");
        }
    }
}
