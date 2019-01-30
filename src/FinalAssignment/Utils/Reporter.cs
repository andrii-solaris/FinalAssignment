using System;
using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports.MarkupUtils;

namespace FinalAssignment.Utils
{  

    class Reporter
    {

        private static ExtentReports Extent;
        private static ExtentTest Test;

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

        public static void WriteTestName(string testName)
        {
            Test = Extent.CreateTest(testName);
        }

        public static void TerminateReporter()
        {
            Extent.Flush();
        }

        public static void Log(string log)
        {
            Test.Log(Status.Pass, MarkupHelper.CreateLabel(log,ExtentColor.Green));
        }        

        public static void LogFail(string log, string screenshotPath)
        {
            Test.Log(Status.Fail, MarkupHelper.CreateLabel($"{log} {Test.AddScreenCaptureFromPath(screenshotPath)}", ExtentColor.Red));
        }

        public static void LogSkip(string log)
        {
            Test.Log(Status.Skip, MarkupHelper.CreateLabel(log, ExtentColor.Yellow));
        }
    }
}
