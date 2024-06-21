namespace MarsQACompetitionTaskNUnit.Utilities
{
    public static class ReportLogger
    {
        public static void LogInfo(string message)
        {
            Console.WriteLine($"INFO: {message}");
            ExtentManager.test.Info(message);
        }

        public static void LogPass(string message)
        {
            Console.WriteLine($"PASS: {message}");
            ExtentManager.test.Pass(message);
        }

        public static void LogFail(string message)
        {
            Console.WriteLine($"FAIL: {message}");
            ExtentManager.test.Fail(message);
        }

        public static void LogSkip(string message)
        {
            Console.WriteLine($"Skip: {message}");
            ExtentManager.test.Skip(message);
        }

        public static void LogWarning(string message)
        {
            Console.WriteLine($"WARNING: {message}");
            ExtentManager.test.Warning(message);
        }
                
    }
}
