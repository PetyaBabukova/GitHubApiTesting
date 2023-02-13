using RestSharp;
using RestSharpDemoProject;
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
            var request = new RestRequest("repos/petyababukova/postman/issues/42");

            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Http status code property: ");

            var issue = JsonSerializer.Deserialize<TestIssue>(response.Content);

            Assert.That(issue.title, Is.EqualTo("Test from RestSharp638116463344059169"), "Issue name");
            Assert.That(issue.number, Is.EqualTo(42), "Issue number");
        }


        [Test]
        public void Test_GetSingleIsssueWithLabels()
        {
            var client = new RestClient("https://api.github.com");
            var request = new RestRequest("repos/petyababukova/postman/issues/42");

            var response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Http status code property: ");

            var issue = JsonSerializer.Deserialize<TestIssue>(response.Content);

            Assert.That(issue.title, Is.EqualTo("Test from RestSharp638116463344059169"), "Issue name");
            Assert.That(issue.number, Is.EqualTo(42), "Issue number");

        
        }


        [Test]
        public void Test_GetIsssueLabels()
        {
            var client = new RestClient("https://api.github.com");
            var request = new RestRequest("repos/petyababukova/postman/issues/43/labels");

            var response = client.Execute(request);

            var labels = JsonSerializer.Deserialize<List<Labels>>(response.Content);



            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Http status code property: ");
            Assert.That(labels.Count, Is.GreaterThan(0), "Availability of labels: ");


            /*foreach (var label in labels)
            {
                Console.WriteLine(label.id);
                Console.WriteLine(label.name);
                Console.WriteLine();
            }*/
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