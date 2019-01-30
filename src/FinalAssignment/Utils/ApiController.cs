using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using System.Linq;
using FluentAssertions;

namespace FinalAssignment.Utils
{
    class ApiController
    {
        private string BaseUrl;
        private RestClient Client;
        private RestRequest Request;
        private IRestResponse Response;       
        
        public void Initialize(string baseUrl)
        {
            BaseUrl = baseUrl;
            Client = new RestClient
            {
                BaseUrl = new System.Uri(BaseUrl),
                Timeout = System.Convert.ToInt32(System.TimeSpan.FromMinutes(3).TotalMilliseconds)
            };
        }

        public JObject GetRequest(string parameter)
        {
            Request = new RestRequest(parameter, Method.GET);
            Response = Client.Get(Request);
            Response.StatusCode.Should().Be(HttpStatusCode.OK);

            return JObject.Parse(Response.Content);
        }

        public JObject PostRequest(string parameter, object body)
        {
            Request = new RestRequest(parameter, Method.POST);
            Request.AddJsonBody(body);
            Response = Client.Post(Request);
            Response.StatusCode.Should().Be(HttpStatusCode.Created);

            return JObject.Parse(Response.Content);
        }

        public JObject PatchRequest(string parameter, object body)
        {
            Request = new RestRequest(parameter, Method.PATCH);
            Request.AddJsonBody(body);
            Response = Client.Patch(Request);
            Response.StatusCode.Should().Be(HttpStatusCode.OK);

            return JObject.Parse(Response.Content);
        }

        public void ValidateAPIResponse(JObject response, string token, string expectedValue)
        {
            string value = (string)response.SelectTokens(token).FirstOrDefault();
            value.Should().BeEquivalentTo(expectedValue);
        }
    }
}
