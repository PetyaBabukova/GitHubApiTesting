using RestSharp;
using System.Net;
using System.Text.Json;

namespace GitHubApiTests
{
    public class ApiTests
    {
        
        [Test]
        public void Test_GetSingleIsssue()
        {
            var client = new RestClient("https://api.github.com");
            var request = new RestRequest("repos/petyababukova/postman/issues/41");

            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Http status code property: ");

            var issue = JsonSerializer.Deserialize<TestIssue>(response.Content);

            Assert.That(issue.title, Is.EqualTo("Issue with labels"), "Issue name");
            Assert.That(issue.number, Is.EqualTo(41), "Issue number");
        }


        [Test]
        public void Test_GetSingleIsssueWithLabels()
        {
            var client = new RestClient("https://api.github.com");
            var request = new RestRequest("repos/petyababukova/postman/issues/41");

            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Http status code property: ");

            var issue = JsonSerializer.Deserialize<TestIssue>(response.Content);

            Assert.That(issue.title, Is.EqualTo("Issue with labels"), "Issue name");
            Assert.That(issue.number, Is.EqualTo(41), "Issue number");
        }


        [Test]

        public void Test_GetAllIsssues()
        {
            var client = new RestClient("https://api.github.com");
            var request = new RestRequest("repos/petyababukova/postman/issues");

            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Http status code property: ");

            var issues = JsonSerializer.Deserialize<List<TestIssue>>(response.Content);

            foreach (var issue in issues)
            {
            Assert.That(issue.title, Is.Not.Empty, "Issue name");
            Assert.That(issue.number, Is.GreaterThan(0), "Issue number");
            }

        }
    }
}