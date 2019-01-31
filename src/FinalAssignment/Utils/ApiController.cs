using Newtonsoft.Json.Linq;
using RestSharp;
using System.Net;
using System.Linq;
using FluentAssertions;
using Serilog;

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

            SetLogs($"Base URL for API request is set to {baseUrl}");

            Client = new RestClient
            {
                BaseUrl = new System.Uri(BaseUrl),
                Timeout = System.Convert.ToInt32(System.TimeSpan.FromMinutes(3).TotalMilliseconds)
            };
        }

        public JObject GetRequest(string parameter)
        {
            Request = new RestRequest(parameter, Method.GET);
            SetLogs($"Executing GET API request with the following parameter: {parameter}");
            Response = Client.Get(Request);
            Response.StatusCode.Should().Be(HttpStatusCode.OK);
            SetLogs($"Response code is: {(HttpStatusCode.OK).ToString()}");

            return JObject.Parse(Response.Content);
        }

        public JObject PostRequest(string parameter, object body)
        {
            Request = new RestRequest(parameter, Method.POST);
            SetLogs($"Executing POST API request with the following parameter: {parameter}");
            Request.AddJsonBody(body);
            SetLogs($"Request has been set up with the following body: {body.ToString()}");
            Response = Client.Post(Request);
            Response.StatusCode.Should().Be(HttpStatusCode.Created);
            SetLogs($"Response code is: {(HttpStatusCode.Created).ToString()}");

            return JObject.Parse(Response.Content);
        }

        public JObject PatchRequest(string parameter, object body)
        {
            Request = new RestRequest(parameter, Method.PATCH);
            SetLogs($"Executing PATCH API request with the following parameter: {parameter}");
            Request.AddJsonBody(body);
            SetLogs($"Request has been set up with the following body: {body.ToString()}");
            Response = Client.Patch(Request);
            Response.StatusCode.Should().Be(HttpStatusCode.OK);
            SetLogs($"Response code is: {(HttpStatusCode.OK).ToString()}");

            return JObject.Parse(Response.Content);
        }

        public void ValidateAPIResponse(JObject response, string token, string expectedValue)
        {
            SetLogs($"Validating the following element of the response: {token}");
            string value = (string)response.SelectTokens(token).FirstOrDefault();
            value.Should().BeEquivalentTo(expectedValue);
            SetLogs($"The expected value {expectedValue} matches the actual one: {value}");
        }

        private void SetLogs(string message)
        {
            Log.Information(message);
            Reporter.Log(message);
        }
    }
}
