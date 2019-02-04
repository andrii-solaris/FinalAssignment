using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;

namespace FinalAssignment.Utils
{
    //This class serves to create a specific instance of a WebDriver using specified parameters.
    static class DriverFactory
    {
        private static readonly IDictionary<string, IWebDriver> _driverDictionary = new Dictionary<string, IWebDriver>();
        private static IWebDriver _driver;

        public static IWebDriver Driver
        {
            get
            {
                return _driver;
            }
            private set
            {
                _driver = value;
            }
        }

        //Instantiates WebDriver. Either "Chrome" or "Firefox" options are available.
        public static void InstantiateDriver(string driverType)
        {

            var browserOptions = ConfigurationManager.AppSettings["DriverOptions"].Split(',');

            switch (driverType)
            {
                case "Firefox":
                    if (Driver == null)
                    {
                        var options = new FirefoxOptions();

                        if (ConfigurationManager.AppSettings["HeadlessMode"].Equals("True") ||
                            (TestContext.Parameters["HeadlessMode"] != null && TestContext.Parameters["HeadlessMode"].Equals("True")))
                        {
                            options.AddArgument("-headless");
                        }

                        options.Profile = new FirefoxProfile();
                        //Setting custom download directory for "Firefox".
                        options.Profile.SetPreference("browser.download.dir", $@"{Constants.CurrentDirectory}\Downloads");
                        options.Profile.SetPreference("browser.download.folderList", 2);
                        options.Profile.SetPreference("browser.helperApps.neverAsk.saveToDisk", "image/jpeg");

                        _driver = new FirefoxDriver(options);
                        _driverDictionary.Add("Firefox", Driver);

                        _driver.Manage().Window.Maximize();

                    }
                    break;               

                case "Chrome":
                    if (Driver == null)
                    {

                        var options = new ChromeOptions();
                        //This part is used to run "Chrome" in a headless mode if this option was specified in App.config file or via NUnit Console.
                        if (ConfigurationManager.AppSettings["HeadlessMode"].Equals("True") ||
                            (TestContext.Parameters["HeadlessMode"] != null && TestContext.Parameters["HeadlessMode"].Equals("True")))
                        {
                            options.AddArgument("-headless");
                        }

                        foreach (var option in browserOptions)
                        {
                            options.AddArgument(option);
                        }

                        //Setting custom download directory for "Chrome".
                        options.AddUserProfilePreference("download.default_directory", $@"{Constants.CurrentDirectory}\Downloads");
                        options.AddUserProfilePreference("download.prompt_for_download", false);
                        options.AddUserProfilePreference("disable-popup-blocking", "true");

                        _driver = new ChromeDriver(options);
                        _driverDictionary.Add("Chrome", Driver);

                    }
                    break;
            }

            Log.Information($"Initializing {driverType} driver...");
        }

        //Used to take screenshots on test fails.
        public static string TakeScreenshot()
        {
            Screenshot screenshot = ((ITakesScreenshot)_driver).GetScreenshot();

            var currentDirectory = $@"{Constants.CurrentDirectory}\FinalAssignment\Reports";
            var screenshotPath = Path.Combine(currentDirectory, Constants.Directory, $"screenshot_{DateTime.Now:HH_ mm_ ss}.png");

            Log.Debug($"Taking screenshot and saving it to {screenshotPath}");

            screenshot.SaveAsFile(screenshotPath);

            TestContext.AddTestAttachment(screenshotPath);

            return screenshotPath;
        }

        //Navigates to the specified URL.
        public static void GoToPage(string pageUrl)
        {
            Reporter.SetLogs($"Navigating to the page by the following url: {pageUrl}");
            _driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
            _driver.Navigate().GoToUrl(pageUrl);
        }

        //Closing WebDriver instance.
        public static void CloseAllDrivers()
        {
            foreach (var key in _driverDictionary.Keys)
            {
                Log.Information($"Closing driver instance...");
                _driverDictionary[key].Quit();
            }
        }
    }
}