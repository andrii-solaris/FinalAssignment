using Newtonsoft.Json.Linq;
using RestSharp;
using FinalAssignment.Utils;
using NUnit.Framework;

namespace FinalAssignment.APITests
{
    [TestFixture]
    class ApiTestSuite : ApiController
    {

        [SetUp]
        public void SetUp()
        {
            Initialize("https://jsonplaceholder.typicode.com/");
        }

        [Test]
        public void ApiGetFirstPostTitle()
        {
            var response = GetRequest("posts/1");
            ValidateAPIResponse(response, "title", "sunt aut facere repellat provident occaecati excepturi optio reprehenderit");
        }

        [Test]
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
        public void ApiPatchCreatedPhoto()
        {
            var response = PatchRequest("photos/5000",
                new
                {
                    title = "MyPhotoTestUpdated"
                });

            ValidateAPIResponse(response, "title", "MyPhotoTestUpdated");
        }        
    }
}
