using System.Text.Json.Serialization;

namespace GitHubApiTests
{
    internal class Locations
    {
        [JsonPropertyName("country")]
        public string Country { get; set; }


    }
}