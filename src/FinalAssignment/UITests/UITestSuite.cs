using System.Configuration;
using FinalAssignment.PageObjectLibrary;
using FinalAssignment.Utils;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Serilog;

namespace FinalAssignment.Tests
{
    [TestFixture]
    [Author("Andrii Stepaniuk", "andrii.stepaniuk@fortegrp.net")]
    class UITestSuite 
    {       

        [OneTimeSetUp]
        public void BeforeTests()
        {
            if (TestContext.Parameters["DriverType"] != null)
            {
                DriverFactory.InstantiateDriver(TestContext.Parameters["DriverType"]);
            }
            else
            {
                DriverFactory.InstantiateDriver(ConfigurationManager.AppSettings["DriverType"]);
            }
        }

        [SetUp]
        public void SetUp()
        {
            Reporter.WriteTestName(TestContext.CurrentContext.Test.MethodName);
            DriverFactory.GoToPage("https://www.phptravels.net/admin");
            new PhpTravelsAdminLoginPage().SubmitSignInForm();
        }

        [Test]
        [Order(1)] 
        [Timeout(80000)]
        [Author("Andrii Stepaniuk", "andrii.stepaniuk@fortegrp.net")]       
        [Description("Creates a set of hotel records using random input data")]
        [TestCaseSource(typeof(HotelDataProvider))]
        public void CreateHotelsRange(string[] testData)
        {
            Reporter.SetLogs("Starting 'Create Hotel range test...'");
            Reporter.SetLogs($"Test data for this test: hotel name - {testData[0]}, hotel description -{testData[1]}, " +
                $"hotel stars - {testData[2]}, hotel type - {testData[3]}, hotel location - {testData[4]}");

            PhpTravelsDashboardPage dashboard = new PhpTravelsDashboardPage().
                ExpandHotelsDropdown().
                ClickHotelsLink().
                Add();

            new HotelEditPage().
                SetHotelName(testData[0]).
                SetHotelDescription(testData[1]).
                SetHotelStars(testData[2]).
                SetHotelType(testData[3]).
                SetDateFrom("23/07/2019").
                SetDateTo("23/07/2019").
                ExpandLocationDropdown().
                SetHotelLocation(testData[4]).                
                ClickSubmit();

            var validations = new SeleniumActions();

           validations.ValidateText("xpath=//table/tbody/tr[1]/td[5]/a", testData[0]);
           validations.ValidateText("xpath=//table/tbody/tr[1]/td[8]", testData[4]);

                dashboard.DeleteFirstHotel();
        }

        [Test, Pairwise]
        [Order(2)]
        [Timeout(80000)]        
        [Author("Andrii Stepaniuk", "andrii.stepaniuk@fortegrp.net")]
        [Description("Changes data entered for a hotel and validates changes were applied.")]
        public void EditHotels([Values("Firstly Edited", "Secondly Edited")] string name, [Values("New York", "Dallas")] string location)
        {
            Reporter.SetLogs("Starting 'Edit Hotels' test.");
            Reporter.SetLogs($"Test data for this test: hotel name - {name}, hotel location - {location}");

            PhpTravelsDashboardPage dashboard = new PhpTravelsDashboardPage().
                ExpandHotelsDropdown().
                ClickHotelsLink().
                EditFirstHotel();

            new HotelEditPage().
                SetHotelName(name).                
                ExpandLocationDropdown().
                SetHotelLocation(location).
                ClickSubmit();

            var validations = new SeleniumActions(); 

            validations.ValidateText("xpath=//table/tbody/tr[1]/td[5]/a", name);
            validations.ValidateText("xpath=//table/tbody/tr[1]/td[8]", location);

        }

        [Test]
        [Order(3)]
        [Timeout(80000)]
        [Author("Andrii Stepaniuk", "andrii.stepaniuk@fortegrp.net")]
        [TestCaseSource(typeof(RoomDataProvider))]
        [Description("Creates a range of rooms in a Rendezvous Hotel")]
        public void CreateRoomRange(string[] testData)
        {
            Reporter.SetLogs("Starting 'Create Rooms test...'");
            Reporter.SetLogs($"Test data for this test: room type - {testData[0]}, room description -{testData[1]}, " +
                $"room price - {testData[2]}, room quantity - {testData[3]}, minimum stay - {testData[4]}, maximum adults - {testData[5]}," +
                $" maximum children - {testData[6]}, extra beds - {testData[7]}, extra bed charges - {testData[8]}");

            PhpTravelsDashboardPage dashboard = new PhpTravelsDashboardPage().
                ExpandHotelsDropdown().
                ClickRoomsLink().
                Add();

            new RoomEditPage().
                ExpandRoomTypeDropdown().
                SetRoomType(testData[0]).
                SetRoomDescription(testData[1]).
                SetRoomPrice(testData[2]).
                SetRoomQuantity(testData[3]).
                SetRoomMinStay(testData[4]).
                SetRoomMaxAdults(testData[5]).
                SetRoomMaxChildren(testData[6]).
                SetRoomExtraBeds(testData[7]).
                SetRoomExtraBedCharges(testData[8]).
                ClickSubmit();

            var validations = new SeleniumActions();

            validations.ValidateText("xpath=//table/tbody/tr[1]/td[3]", testData[0]);            
            validations.ValidateText("xpath=//table/tbody/tr[1]/td[5]", testData[3]);
            validations.ValidateText("xpath=//table/tbody/tr[1]/td[6]", testData[2]);

            dashboard.DeleteFirstRoom();

        }

        [Test]
        [Order(4)]
        [Timeout(80000)]       
        [Author("Andrii Stepaniuk", "andrii.stepaniuk@fortegrp.net")]
        [Description("Changes Rendezvous Hotel to another ones")]
        public void ReassignRoomToHotel([Values("Malmaison Manchester", "Alzer Hotel Istanbul")] string hotelName)
        {

            Reporter.SetLogs("Starting 'Edit Hotels' test.");
            Reporter.SetLogs($"Test data for this test: hotel name - {hotelName}");

            PhpTravelsDashboardPage dashboard = new PhpTravelsDashboardPage().
                ExpandHotelsDropdown().
                ClickRoomsLink().
                EditFirstRoom();

            new RoomEditPage().
                ExpandHotelDropdown().
                SetHotel(hotelName).
                ClickSubmit();

            new SeleniumActions().ValidateText("xpath=//table/tbody/tr[1]/td[4]", hotelName);

        }

        [TearDown]
        public void TearDown()
        {

            new PhpTravelsDashboardPage().LogOut();

            var message = "";            

            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var stackTrace = TestContext.CurrentContext.Result.StackTrace;
                var errorMessage = TestContext.CurrentContext.Result.Message;
                message = $"Test failed! Stack trace of an error is {stackTrace}{errorMessage}";
                Log.Debug(message);
                Reporter.LogFail(message, DriverFactory.TakeScreenshot());

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

        [OneTimeTearDown]
        public void AfterTests()
        {
            DriverFactory.CloseAllDrivers();
        }
    }
}
