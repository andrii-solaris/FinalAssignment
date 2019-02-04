using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace FinalAssignment.Utils
{  
    //This class is used as a controller for Extent Reports functionality.
    class Reporter
    {

        private static ExtentReports Extent;
        private static ExtentTest Test;

        //Sets an instance of Extent Reports and configures it.
        public static void InitializeReporter(string reporterPath)
        {
            Extent = new ExtentReports();
            var htmlReporter = new ExtentHtmlReporter(reporterPath);
            Extent.AddSystemInfo("Environment", "Final Assignment Framework");
            Extent.AddSystemInfo("User Name", "Andrii Stepaniuk");
            Extent.AttachReporter(htmlReporter);

            htmlReporter.Config.DocumentTitle = "Final Automation Course Assignment";
            htmlReporter.Config.ReportName = "UI and API tests";
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;            
        }

        //Get name of the current test and append to report.
        public static void WriteTestName(string testName)
        {
            Test = Extent.CreateTest(testName);
        }

        //Terminate Extent Reports instance.
        public static void TerminateReporter()
        {
            Extent.Flush();
        }

        //Adds a step to report.
        public static void Log(string log)
        {            
                Test.Log(Status.Pass, log);         
        }        

        //Changes test status to fail in HTML report, takes screenshot and appends it to the report, used in UI tests.
        public static void LogFail(string log, string screenshotPath)
        {
            Test.Log(Status.Fail, $"{log} {Test.AddScreenCaptureFromPath(screenshotPath)}");
        }

        //Changes test status to fail in HTML report, used in API tests.
        public static void LogFail(string log)
        {
            Test.Log(Status.Fail, log);
        }

        //Changes test status to skipped in HTML report.
        public static void LogSkip(string log)
        {
            Test.Log(Status.Skip, log);
        }

        //Writes step to logs and HTML reports.
        public static void SetLogs(string message)
        {
            Serilog.Log.Information(message);
            Log(message);
        }
    }
}
