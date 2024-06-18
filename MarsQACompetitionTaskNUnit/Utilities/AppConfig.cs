using System.Text.Json;

namespace MarsQACompetitionTaskNUnit.Utilities
{
    internal class AppConfig
    {
        public string Browser { get; set; }
        public string url { get; set; }

        public static AppConfig LoadConfig()
        {
            // Load JSON data from the file
            string jsonFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Appsetting.json");
            string jsonString = File.ReadAllText(jsonFilePath);

            //deserialization of data
            AppConfig appConfig = JsonSerializer.Deserialize<AppConfig>(jsonString);

            return appConfig;

        }

    }
}
