using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using System.Reflection;

namespace MarsQACompetitionTaskNUnit.Utilities
{
    public static class ExtentManager
    {
        public static ExtentReports extent;
        public static ExtentTest test;

        public static ExtentReports GetExtent()
        {
            if (extent == null)
            {
                var reportPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"..\..\..\..\Reports\");                                     
                Directory.CreateDirectory(reportPath);
                var htmlReporter = new ExtentHtmlReporter(Path.Combine(reportPath, "ExtentReport.html"));
                extent = new ExtentReports();
                extent.AttachReporter(htmlReporter);
            }
            return extent;
        }

        public static void CreateTest(string testName)
        {
            test = GetExtent().CreateTest(testName);
            
        }

        public static void FlushReport()
        {
            if (extent != null)
            {
                extent.Flush();
                extent = null; // Dispose of the extent instance
            }
        }

        public static void LogScreenshot(string message, string image)
        {
            if (string.IsNullOrEmpty(image))
            {
                test.Info("No screenshot available.");
                return;
            }

            test.Info(message, MediaEntityBuilder.CreateScreenCaptureFromBase64String(image).Build());
        }
    }
       
}
