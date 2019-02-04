using FinalAssignment.Utils;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Serilog;

namespace FinalAssignment.Tests
{
    [TestFixture]
    [Description("This class represents test suite for API tests performed under https://jsonplaceholder.typicode.com/")]
    class ApiTestSuite : ApiController
    {

        [SetUp]
        public void SetUp()
        {
            Reporter.WriteTestName(TestContext.CurrentContext.Test.MethodName);
            Initialize("https://jsonplaceholder.typicode.com/");
        }

        [Test]
        [Order(1)]
        [Timeout(5000)]
        [Author("Andrii Stepaniuk", "andrii.stepaniuk@fortegrp.net")]
        [Description("Gets title of the first post and compares it against the expected result via API.")]
        public void ApiGetFirstPostTitle()
        {
            var response = GetRequest("posts/1");
            ValidateAPIResponse(response, "title", "sunt aut facere repellat provident occaecati excepturi optio reprehenderit");
        }

        [Test]
        [Order(2)]
        [Timeout(5000)]
        [Author("Andrii Stepaniuk", "andrii.stepaniuk@fortegrp.net")]
        [Description("Creates a record in a database via API using preset data, validates record was created.")]
        public void ApiCreateNewPhoto()
        {
            var response = PostRequest("photos",
                new
                {
                    albumId = 1,
                    title = "MyPhotoTest",
                    url = "https://via.placeholder.com/600/92c952",
                    thumbnailUrl = "https://via.placeholder.com/150/92c952"
                });

            ValidateAPIResponse(response, "id", "5001");
        }

        [Test]
        [Order(3)]
        [Timeout(5000)]
        [Author("Andrii Stepaniuk", "andrii.stepaniuk@fortegrp.net")]
        [Description("Updates a title field of a specified record via API, validates field was updated.")]
        public void ApiPatchCreatedPhoto()
        {
            var response = PatchRequest("photos/5000",
                new
                {
                    title = "MyPhotoTestUpdated"
                });

            ValidateAPIResponse(response, "title", "MyPhotoTestUpdated");
        }  
        
        [TearDown]
        public void TearDown()
        {

            var message = "";

            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {               
                var stackTrace = TestContext.CurrentContext.Result.StackTrace;
                var errorMessage = TestContext.CurrentContext.Result.Message;
                message = $"Test failed! Stack trace of an error is {stackTrace}{errorMessage}";
                Log.Debug(message);
                Reporter.LogFail(message);

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
