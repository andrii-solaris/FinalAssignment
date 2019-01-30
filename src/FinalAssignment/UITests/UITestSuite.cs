using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinalAssignment.PageObjectLibrary;
using FinalAssignment.Utils;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Serilog;

namespace FinalAssignment.Tests
{
    [TestFixture]
    [Author("Andrii Stepaniuk", "andrii.stepaniuk@fortegrp.net")]
    class TestSuite : SeleniumActions
    {

        private AtataSampleAppPage _samplePage = new AtataSampleAppPage();

        [SetUp]
        public void SetUp()
        {
            Reporter.WriteTestName(TestContext.CurrentContext.Test.MethodName);
            base.GoToPage("https://atata-framework.github.io/atata-sample-app/#!/");
            new AtataIndexPage().ClickSignInButton();
            new AtataSignInPage().SubmitSignInForm();
        }

        [Test]
        [Order(1)]
        [Timeout(5000)]
        [Author("Andrii Stepaniuk", "andrii.stepaniuk@fortegrp.net")]
        [Description("Validates full name, email, city and gender of registered users")]
        [TestCaseSource(typeof(TestDataProvider))]
        public void ValidateUserDetails(string[] testData)
        {
            Log.Information("Starting 'ValidateUserDetails test...Timeouts set to 5 seconds.'");
            Log.Information($"Test data for this test: user index - {testData[0]}, fullname -{testData[1]}, " +
                $"email - {testData[2]}, city - {testData[3]}, gender - {testData[4]}");
            _samplePage.ViewUserDetails(Convert.ToInt32(testData[0]));
            base.ValidateText("xpath=//h1", testData[1]);
            base.ValidateText("css=div.summary-container>div>dl:nth-child(1)>dd", testData[2]);
            base.ValidateText("css=div.summary-container>div>dl:nth-child(2)>dd", testData[3]);
            base.ValidateText("css=div.summary-container>div>dl:nth-child(3)>dd", testData[4]);
            base.ToPreviousPage();
        }

        [Test, Sequential]
        [Order(2)]
        [Timeout(5000)]
        [Author("Andrii Stepaniuk", "andrii.stepaniuk@fortegrp.net")]
        [Description("Navigates to 'Calculations' tab, enters first and second operand, validates the result of addition")]
        public void CalculateValue([Values(2, 5, 7)] int firstOperand, [Values(2, 3, 2)] int secondOperand, [Values(4, 8, 9)] int result)
        {
            Log.Information("Starting 'Calculate Addition Value' test. Timeouts set to 5 seconds.");
            _samplePage.GoToCalculationsTab();
            Log.Information($"Test data for this test first operand: {firstOperand}, second operand: {secondOperand}, expected result: {result}");
            Assert.That(base.GetCurrentUrl(), Is.EqualTo("https://atata-framework.github.io/atata-sample-app/#!/calculations"));
            Assert.That(_samplePage.ConductCalculations(firstOperand, secondOperand), Is.EqualTo(result.ToString()));
        }

        [Test]
        [Order(3)]
        [Timeout(5000)]
        [Author("Andrii Stepaniuk", "andrii.stepaniuk@fortegrp.net")]
        [Description("Navigates to 'Plans' tab, validates that the price of Basic, Plus and Premium plans displays as expected")]
        public void ValidatePlans()
        {
            Log.Information("Starting 'ValidatePlans test...Timeouts set to 5 seconds.'");
            _samplePage.GoToPlansTab();
            Assert.That(base.GetCurrentUrl(), Is.EqualTo("https://atata-framework.github.io/atata-sample-app/#!/plans"));
            base.ValidateText("xpath=//h3[text()='Basic']/following-sibling::b", "$0");
            base.ValidateText("xpath=//h3[text()='Plus']/following-sibling::b", "$19.99");
            base.ValidateText("xpath=//h3[text()='Premium']/following-sibling::b", "$49.99");
        }

        [Test]
        [Order(4)]
        [Timeout(5000)]       
        [Author("Andrii Stepaniuk", "andrii.stepaniuk@fortegrp.net")]
        [Description("Navigates to 'Products' tab. This test fails.")]
        public void ValidatePricesProductsTab()
        {
            Log.Information("Starting 'ValidateProducts test...Timeouts set to 5 seconds. This test is about to fail.'");
            _samplePage.GoToProductsTab();
            Assert.That(base.GetCurrentUrl(), Is.EqualTo("https://atata-framework.github.io/atata-sample-app/#!/products"));            
            base.ValidateText("xpath=//tbody/tr[1]/td[2]", "$127.00");
        }

        [TearDown]
        public void TearDown()
        {
            var message = "";

            _samplePage.Logout();

            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var stackTrace = TestContext.CurrentContext.Result.StackTrace;
                var errorMessage = TestContext.CurrentContext.Result.Message;
                message = $"Test failed! Stack trace of an error is {stackTrace}{errorMessage}";
                Log.Debug(message);
                Reporter.LogFail(message, base.TakeScreenshot());

            }
            else if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed)
            {
                message = "Test passed!";
                Log.Debug(message);
                Reporter.Log(message);                
            }

            else if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Skipped)
            {
                message = "Test skipped!";
                Log.Debug(message);
                Reporter.Log(message);
            }                
        }
    }
}
