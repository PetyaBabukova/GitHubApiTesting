using RestSharp;
using RestSharp.Authenticators;
using RestSharpDemoProject;
using System.Net;
using System.Text.Json;

namespace GitHubApiTests
{
    public class ApiTests
    {

        private RestClient client;
        const string baseUrl = "https://api.github.com";
        const string partialUrl = "repos/petyababukova/postman/issues";
        private const string username = "petyababukova";
        private const string password = "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";
        [SetUp] public void Setup() 
        {
            this.client = new RestClient(baseUrl);
            this.client.Authenticator = new HttpBasicAuthenticator(username, password);
            //this.client.Timeout = 2000; //It doesn`t work here, So, in the test it`s work
        }

        [Test]
        [Timeout(2000)]
        public void Test_GetSingleIsssue()
        {
            
            var request = new RestRequest($"{partialUrl}/42");
            //var request = new RestRequest(partialUrl + "/42");

            var response = this.client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Http status code property: ");

            var issue = JsonSerializer.Deserialize<TestIssue>(response.Content);

            Assert.That(issue.title, Is.EqualTo("Test from RestSharp638116463344059169"), "Issue name");
            Assert.That(issue.number, Is.EqualTo(42), "Issue number");
        }


        [Test]
        public void Test_GetSingleIsssueWithLabels()
        {
            var request = new RestRequest($"{partialUrl}/42");

            var response = this.client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Http status code property: ");

            var issue = JsonSerializer.Deserialize<TestIssue>(response.Content);

            Assert.That(issue.title, Is.EqualTo("Test from RestSharp638116463344059169"), "Issue name");
            Assert.That(issue.number, Is.EqualTo(42), "Issue number");

        
        }


        [Test]
        public void Test_GetIsssueLabels()
        {
            var request = new RestRequest($"{partialUrl}/43/labels");

            var response = this.client.Execute(request);

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
            var request = new RestRequest("repos/petyababukova/postman/issues");

            var response = this.client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Http status code property: ");

            var issues = JsonSerializer.Deserialize<List<TestIssue>>(response.Content);


            foreach (var issue in issues)
            {
            Assert.That(issue.title, Is.Not.Empty, "Issue name");
            Assert.That(issue.number, Is.GreaterThan(0), "Issue number");
            }

        }

        [Test]
        public void Test_CreateNewIssue()
        {

            //Arrange
            
            var issueBody = new
            {
                title = "Test from RestSharp " + DateTime.Now.Ticks,
                body = "Some description on my body issue",
                labels = new string[] { "bug", "13-02", "critical" }
            };


            var issue = CreateIssue(issueBody);
            

            //Assert
            Assert.That(issue.number, Is.GreaterThan(0), "Issue number: ");
            Assert.That(issue.title, Is.EqualTo(issueBody.title), "Issue Title: ");
            Assert.That(issue.body, Is.EqualTo(issueBody.body), "Issue Body");
        }

        [Test] 
        public void Test_EditIssue() 
        {
            var editedBody = new
            {
                title = "Edited Test from RestSharp " + DateTime.Now.Ticks,
                body = "Edited description on my body issue",
                labels = new string[] { "edited", "critical" }

            };

            var issue = CreateIssue(editedBody);

            //Assert
            Assert.That(issue.number, Is.GreaterThan(0), "Issue number: ");
            Assert.That(issue.title, Is.EqualTo(editedBody.title), "Issue Title: ");
            Assert.That(issue.body, Is.EqualTo(editedBody.body + "1"), "Issue Body");

        }

        private Issue CreateIssue(object body)
        {
            //Arrange
            var request = new RestRequest(partialUrl, Method.Post);

            request.AddBody(body);

            //Act
            var response = this.client.Execute(request);
            var issue = JsonSerializer.Deserialize<Issue>(response.Content);

            return issue;
        }

        private Issue EditIssue(object body)
        {
            //Arrange
            var request = new RestRequest($"{partialUrl}/43", Method.Patch);

            request.AddBody(body);

            //Act
            var response = this.client.Execute(request);
            var issue = JsonSerializer.Deserialize<Issue>(response.Content);

            return issue;
        }


    }
}