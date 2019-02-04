using FinalAssignment.Utils;
using NUnit.Framework;
using System.IO;
using Serilog;
using Serilog.Events;
using Serilog.Enrichers;

namespace FinalAssignment.Tests
{    

    [SetUpFixture]
    class TestSuiteUtils
    {        

        [OneTimeSetUp]
        //This method initializes and configures both Serilog and Extent Report functionalities. 
        public void RunBeforeTestSuite()

        {            
            var currentDirectory = $@"{Constants.CurrentDirectory}\FinalAssignment\Reports";
            var logFilePath = Path.Combine(currentDirectory, Constants.Directory, "logs.txt");
            var detailedLogFilePath = Path.Combine(currentDirectory, Constants.Directory, "detailedLogs.txt");
            var htmlReportFilePath = Path.Combine(currentDirectory, Constants.Directory, "extentreport.html");
            var template = "[{Timestamp:HH:mm:ss} {Level:u3}] [{ProcessId}] {Message:lj}{NewLine}{Exception}";

            Log.Logger = new LoggerConfiguration().
                MinimumLevel.Debug().
                WriteTo.File(detailedLogFilePath, outputTemplate: template).
                WriteTo.File(logFilePath, outputTemplate: template, restrictedToMinimumLevel: LogEventLevel.Information).
                WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Information).
                Enrich.WithProcessId().
                CreateLogger();

            Log.Debug($"Logger instance created with three sinks. Output files will be placed to {currentDirectory} in {Constants.Directory} folder");

            Reporter.InitializeReporter(htmlReportFilePath);               
        }

        [OneTimeTearDown]
        //This method finalizes and terminates both Serilog and Extent Report functionalities. 
        public void RunAfterTestSuite()
        {
            Reporter.TerminateReporter();
            Log.Debug("Preparing to close logger instance...");
            Log.CloseAndFlush();            
        }
        
    }
}
